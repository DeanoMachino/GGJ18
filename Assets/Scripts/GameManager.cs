using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

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
