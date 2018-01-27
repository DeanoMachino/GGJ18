using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int _playerID;

    Rigidbody2D Rigidbody;
    public float TimeAfterDestroy;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Initialise(int playerID, Vector2 initialDirection, float velocity) {
        if (Rigidbody == null) {
            Rigidbody = GetComponent<Rigidbody2D>();
        }
        Rigidbody.AddForce(initialDirection * velocity, ForceMode2D.Impulse);
    }

    void Update()
    {
        UpdateRotation();

        Destroy(gameObject, TimeAfterDestroy);
    }

    void UpdateRotation()
    {
        //find Angle
        float angle = Mathf.Atan2(Rigidbody.velocity.y, Rigidbody.velocity.x) * Mathf.Rad2Deg;

        //Set Rotation
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Hit = " + collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("I hit a player!");
        }
    }
}
