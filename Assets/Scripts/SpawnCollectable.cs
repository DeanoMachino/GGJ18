using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectable : MonoBehaviour {

    public GameObject Collectable;
    public GameObject[] CollectableSpawns;
    GameObject SpawnCol = null;

    // Use this for initialization
    void Start () {
        CollectableSpawns = GameObject.FindGameObjectsWithTag("SpawnCollectable");
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
        int index = Random.Range(0, CollectableSpawns.Length);
        GameObject currentPoint = CollectableSpawns[index];
        SpawnCol = Instantiate(Collectable, currentPoint.transform) as GameObject;
    }
}
public enum Parts
{
    RadioP1,
    RadioP2,
    RadioP3,
    RadioP4
}