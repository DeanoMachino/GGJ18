using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int _playerID;

    Rigidbody2D Rigidbody;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Initialise(int playerID, Vector2 initialDirection, float velocity, float lifetime) {
        if (Rigidbody == null) {
            Rigidbody = GetComponent<Rigidbody2D>();
        }
        Rigidbody.AddForce(initialDirection * velocity, ForceMode2D.Impulse);
        StartCoroutine(DestroyProjectile(lifetime));
    }

    void FixedUpdate()
    {
        UpdateRotation();
    }

    private IEnumerator DestroyProjectile(float lifetime) {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
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
