using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Players[] players;

    public static GameManager Instance;

    public static float GRAVITY = -25f;

    private void Awake() {
        Instance = this;
    }

    // Use this for initialization
    private void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
		
	}
}
public class Players
{
    public GameObject PlayerReference;
    public string Name;
    public int Kills;
    public int Deaths;
    public bool P1;
    public bool P2;
    public bool P3;
    public bool P4;
}