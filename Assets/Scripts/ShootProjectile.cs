using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public float CoolDownTime, SpellCost;
    float CoolDown;
    public GameObject Projectile;
    Vector3 SpawnLocation;
    Vector3 SpawnVelocity;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CoolDown == CoolDownTime)
            {
                Shoot();
            }
        }
    }
    void Shoot()
    {
        SetProjectileValues();
        GameObject SpawnProjectile = Instantiate(Projectile) as GameObject;
        SpawnProjectile.transform.position = SpawnLocation;
        Rigidbody2D SpawnedRigidbody = SpawnProjectile.GetComponent<Rigidbody2D>();
        SpawnedRigidbody.velocity = SpawnVelocity * 40;
        CoolDown = CoolDown + SpellCost;
    }
    void SetProjectileValues()
    {
        var horiz = Input.GetAxis("Horizontal");
        var vert = Input.GetAxis("Vertical");
        SpawnVelocity = new Vector3(horiz, vert, 0);
        SpawnLocation = this.transform.position;
    }
}
