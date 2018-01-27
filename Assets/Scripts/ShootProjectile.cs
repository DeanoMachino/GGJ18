using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public float CoolDownTime, SpellCost, CoolDownRechargeSpeed, DelayBeforeStartChargeCooldown;
    float CoolDown, StartCharge;
    public GameObject Projectile;
    Vector3 SpawnLocation;
    Vector3 SpawnVelocity;

    void Start()
    {
        CoolDown = CoolDownTime;
        InvokeRepeating("ChangeCoolDown", 0f, 0.1f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CoolDown > SpellCost)
            {
                Shoot();
            }
        }
    }

    void ChangeCoolDown()
    {
        //Debug.Log("StartCharge = " + StartCharge + "  CoolDown = " + CoolDown);
        if (StartCharge >= 0)
        {
            StartCharge = StartCharge - 1f;
        }
        else
        {
            if (CoolDown >= CoolDownTime)
            {
            }
            else
            {
                CoolDown = CoolDown + CoolDownRechargeSpeed;
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
        CoolDown = CoolDown - SpellCost;
        StartCharge = DelayBeforeStartChargeCooldown;
    }

    void SetProjectileValues()
    {
        var horiz = Input.GetAxis("Horizontal");
        var vert = Input.GetAxis("Vertical");
        SpawnLocation = this.transform.position;
        SpawnVelocity = new Vector3(horiz, vert, 0);
    }

}
