using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    Rigidbody2D RB;

    float Angle;

	// Use this for initialization
	void Start () {
        RB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        
        SetRotation();

    }

    void SetRotation()
    {
        //find Angel

        //Angle = ;

        //Set Rotation

        RB.rotation = Angle;
    }
}
