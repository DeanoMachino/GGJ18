using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectable : MonoBehaviour {

    public GameObject Collectable;
    GameObject SpawnCol = null;

    // Use this for initialization
    void Start () {
        Spawn();
    }
	
	// Update is called once per frame
	void Update () {
        if (SpawnCol == null && !IsInvoking("Spawn"))
        {
            Invoke("Spawn", 2);
        }
    }

    void Spawn()
    {
        SpawnCol = Instantiate(Collectable) as GameObject;
        SpawnCol.transform.position = transform.position;
    }
}
public enum Parts
{
    RadioP1,
    RadioP2,
    RadioP3,
    RadioP4
}