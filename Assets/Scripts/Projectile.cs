using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D RB;
    float Angle;
    public float TimeAfterDestroy;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        SetRotation();

        Destroy(gameObject, TimeAfterDestroy);
    }

    void SetRotation()
    {
        //find Angle
        Angle = Mathf.Atan2(RB.velocity.y, RB.velocity.x) * Mathf.Rad2Deg;

        //Set Rotation
        transform.rotation = Quaternion.AngleAxis(Angle, Vector3.forward);
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
