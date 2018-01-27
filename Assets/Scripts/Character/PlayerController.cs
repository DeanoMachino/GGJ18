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

    [HideInInspector]
    private float normalizedHorizontalSpeed = 0;

    private CharacterController2D _controller;
    private Animator _animator;
    private Vector3 _velocity;

    private int _playerID = 1;

    private bool _isChargingAttack = false;
    private float _chargingProgress;
    private float _chargingSpeed = 3;

    #region Event Listeners

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
        _playerID = id;
    }

    private void Update() {
        ProcessAttack();
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
            projectile.Initialise(_playerID, GetAttackDirection(), _chargingProgress * 10);
            Debug.Log("Attack released");
        }
        _chargingProgress = 0;
        _isChargingAttack = false;
    }

    private Vector3 GetAttackDirection() {
        var horizontal = Input.GetAxis(GetControlString(PlayerControls.HorizontalAim));
        var vertical = Input.GetAxis(GetControlString(PlayerControls.VerticalAim));

        if (horizontal == 0 && vertical == 0) {
            horizontal = 1;
        }

        return new Vector2(horizontal, vertical).normalized;
    }

    private void FixedUpdate() {
        UpdatePhysics();
    }

    private void UpdatePhysics() {
        if (_controller.IsGrounded) {
            _velocity.y = 0;
        }

        if (Input.GetAxis(GetControlString(PlayerControls.Movement)) > 0) {
            normalizedHorizontalSpeed = 1;
            if (transform.localScale.x < 0f) {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }

            if (_controller.IsGrounded) {
                //_animator.Play(Animator.StringToHash("Run"));
            }
        } else if (Input.GetAxis(GetControlString(PlayerControls.Movement)) < 0) {
            normalizedHorizontalSpeed = -1;
            if (transform.localScale.x > 0f) {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            if (_controller.IsGrounded) {
                //_animator.Play(Animator.StringToHash("Run"));
            }
        } else {
            normalizedHorizontalSpeed = 0;

            if (_controller.IsGrounded) {
                //_animator.Play(Animator.StringToHash("Idle"));
            }
        }

        // we can only jump whilst grounded
        if (_controller.IsGrounded && Input.GetButtonDown(GetControlString(PlayerControls.Jump))) {
            _velocity.y = Mathf.Sqrt(2f * jumpHeight * -GameManager.GRAVITY);
            //_animator.Play(Animator.StringToHash("Jump"));
        }

        // apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
        var smoothedMovementFactor = _controller.IsGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
        _velocity.x = Mathf.Lerp(_velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor);

        // apply gravity before moving
        _velocity.y += GameManager.GRAVITY * Time.deltaTime;

        // if holding down bump up our movement amount and turn off one way platform detection for a frame.
        // this lets us jump down through one way platforms
        if (_controller.IsGrounded && Input.GetKey(KeyCode.DownArrow)) {
            _velocity.y *= 3f;
            _controller.ignoreOneWayPlatformsThisFrame = true;
        }

        _controller.Move(_velocity * Time.deltaTime);

        // grab our current _velocity to use as a base for all calculations
        _velocity = _controller.velocity;
    }

    private string GetControlString(PlayerControls control) {
        switch (control) {
            case PlayerControls.Movement:
                return string.Format("Player{0}Movement", _playerID);
            case PlayerControls.HorizontalAim:
                return string.Format("Player{0}AimHorizontal", _playerID);
            case PlayerControls.VerticalAim:
                return string.Format("Player{0}AimVertical", _playerID);
            case PlayerControls.Jump:
                return string.Format("Player{0}Jump", _playerID);
            case PlayerControls.Attack:
                return string.Format("Player{0}Attack", _playerID);
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
}
