using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // movement config
    public float runSpeed = 8f;
    public float groundDamping = 20f; // how fast do we change direction? higher means faster
    public float inAirDamping = 5f;
    public float jumpHeight = 3f;

    public GameObject projectilePrefab;

    private CharacterController2D _controller;
    private Animator _animator;
    private Vector3 _velocity;

    public int playerID = 1;

    private bool _isChargingAttack = false;
    private float _chargingProgress;
    private float _chargingSpeed = 3;
    public int AvatarIndex = 0;

    #region Event Listeners

    void Start()
    {
    }

    void onControllerCollider(RaycastHit2D hit) {
        // bail out on plain old ground hits cause they arent very interesting
        if (hit.normal.y == 1f) {
            return;
        }
        // logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
        //Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
    }

    void onTriggerEnterEvent(Collider2D col) {
        Debug.Log("onTriggerEnterEvent: " + col.gameObject.name);
        if (col.gameObject.tag == "Collectable")
        {
            switch (col.gameObject.GetComponent<Collectable>().Part)
            {
                case Parts.RadioP1:
                    GameManager.Instance.UpdateScore(Score.GotPart1, playerID, 0);
                    break;
                case Parts.RadioP2:
                    GameManager.Instance.UpdateScore(Score.GotPart2, playerID, 0);
                    break;
                case Parts.RadioP3:
                    GameManager.Instance.UpdateScore(Score.GotPart3, playerID, 0);
                    break;
                case Parts.RadioP4:
                    GameManager.Instance.UpdateScore(Score.GotPart4, playerID, 0);
                    break;
            }
            Destroy(col.gameObject);
        }
    }

    void onTriggerExitEvent(Collider2D col) {
        Debug.Log("onTriggerExitEvent: " + col.gameObject.name);
    }

    #endregion

    private void Awake() {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController2D>();

        // listen to some events for illustration purposes
        _controller.OnControllerCollidedEvent += onControllerCollider;
        _controller.OnTriggerEnterEvent += onTriggerEnterEvent;
        _controller.OnTriggerExitEvent += onTriggerExitEvent;
    }

    public void Initialise(int id) {
        playerID = id;

        SetUpVisuals();
    }

    private void SetUpVisuals() {
        _animator.runtimeAnimatorController = CharacterSpriteManager.Instance.characterAnimators[playerID];
    }

    private void Update() {
        if (!GameManager.Instance.IsCountingDown()) {
            ProcessAttack();
        }
    }

    private void ProcessAttack() {
        float attackInput = Input.GetAxis(GetControlString(PlayerControls.Attack));
        if (attackInput > 0) {
            // Charge attack
            Debug.Log("Charging: " + _chargingProgress);
            _chargingProgress = Mathf.Clamp01(_chargingProgress + Time.deltaTime / _chargingSpeed);
            _isChargingAttack = true;
        } else if (_isChargingAttack) {
            ReleaseAttack();
        }
    }

    private void ReleaseAttack() {
        // Release an attack based on how long the player has charged their attack.
        if (_chargingProgress < 0.25f) {
            // do nothing, not charged enough
            Debug.Log("Attack failed");
        } else {
            GameObject projectileGO = Instantiate(projectilePrefab) as GameObject;
            projectileGO.transform.position = transform.position;
            Projectile projectile = projectileGO.GetComponent<Projectile>();
            float velocity = _chargingProgress * 20;
            float lifetime = 3;
            projectile.Initialise(playerID, GetAttackDirection(), velocity, lifetime);
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), projectileGO.GetComponent<Collider2D>());
            Debug.Log("Attack released");
            AudioManager.Instance.playAudioClip(AudioManager.AvailableAudioClips.releaseAttack);
        }
        _chargingProgress = 0;
        _isChargingAttack = false;
    }

    public float GetChargeProgress() {
        return _chargingProgress;
    }

    private Vector3 GetAttackDirection() {
        var horizontal = Input.GetAxis(GetControlString(PlayerControls.HorizontalAim));
        var vertical = Input.GetAxis(GetControlString(PlayerControls.VerticalAim));

        if (horizontal > -0.2f && horizontal < 0.2f && vertical > -0.2f && vertical < 0.2f) {
            horizontal = IsFacingLeft() ? -1 : 1;
        }

        return new Vector2(horizontal, -vertical).normalized;
    }

    private void FixedUpdate() {
        if (!GameManager.Instance.IsCountingDown()) {
            UpdatePhysics();
        }
    }

    private void UpdatePhysics() {
        if (_controller.IsGrounded) {
            _velocity.y = 0;
        }

        bool moving = ProcessMove();
        bool jumping = ProcessJump();

        // apply gravity before moving
        if (!_controller.IsGrounded) {
            _velocity.y += GameManager.GRAVITY * Time.deltaTime;
        }

        // if holding down bump up our movement amount and turn off one way platform detection for a frame.
        // this lets us jump down through one way platforms
        if (_controller.IsGrounded && Input.GetKey(KeyCode.DownArrow)) {
            _velocity.y *= 3f;
            _controller.ignoreOneWayPlatformsThisFrame = true;
        }

        _controller.Move(_velocity * Time.deltaTime);

        // grab our current _velocity to use as a base for all calculations
        _velocity = _controller.velocity;

        UpdateAnimation(moving, jumping);
    }

    private bool ProcessMove() {
        float movementInput = Input.GetAxis(GetControlString(PlayerControls.Movement));
        float targetSpeed = 0;

        if (movementInput > 0) {
            targetSpeed = runSpeed;
        } else if (movementInput < 0) {
            targetSpeed = -runSpeed;
        }

        _velocity.x = movementInput * runSpeed;

        // Move the player
        if (movementInput > 0 && transform.localScale.x < 0 || movementInput < 0 && transform.localScale.x > 0) {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        
        // Apply horizontal smoothing.
        float smoothedMovementFactor = _controller.IsGrounded ? groundDamping : inAirDamping;
        _velocity.x = Mathf.SmoothDamp(_velocity.x, targetSpeed, ref _velocity.x, Time.deltaTime * smoothedMovementFactor);

        return movementInput != 0;
    }

    private bool ProcessJump() {
        bool jumpInput = Input.GetButtonDown(GetControlString(PlayerControls.Jump));

        // Jump if the player is on the ground.
        if ((_controller.IsGrounded || _velocity.y == 0) && jumpInput) {
            _velocity.y = Mathf.Sqrt(2f * jumpHeight * -GameManager.GRAVITY);
            AudioManager.Instance.playAudioClip(AudioManager.AvailableAudioClips.jump);
        }

        return jumpInput;
    }

    private void UpdateAnimation(bool moving, bool jumping) {
        if (_controller.IsGrounded) {
            if (jumping) {
                //_animator.Play(Animator.StringToHash("Jump"));
            } else if (moving) {
                _animator.Play(Animator.StringToHash("Run"));
            } else {
                _animator.Play(Animator.StringToHash("Idle"));
            }
        }
    }

    private string GetControlString(PlayerControls control) {
        switch (control) {
            case PlayerControls.Movement:
                return string.Format("Player{0}Movement", playerID + 1);
            case PlayerControls.HorizontalAim:
                return string.Format("Player{0}AimHorizontal", playerID + 1);
            case PlayerControls.VerticalAim:
                return string.Format("Player{0}AimVertical", playerID + 1);
            case PlayerControls.Jump:
                return string.Format("Player{0}Jump", playerID + 1);
            case PlayerControls.Attack:
                return string.Format("Player{0}Attack", playerID + 1);
        }
        return "";
    }

    public enum PlayerControls {
        Movement,
        HorizontalAim,
        VerticalAim,
        Jump,
        Attack
    }

    private bool IsFacingLeft() {
        return transform.localScale.x < 0;
    }
}
