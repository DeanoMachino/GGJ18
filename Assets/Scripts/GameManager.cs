using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Player[] players;

    public static GameManager Instance;

    public static float GRAVITY = -25f;

    private List<PlayerController> _players;

    private void Awake() {
        Instance = this;
    }

    // Use this for initialization
    private void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
		
	}

    public void StartGame() {
        InitialiseLevel();
    }

    private void InitialiseLevel() {

    }

    void Players(int QuantPlayers)
    {
        
    }
}

public class Player
{
    public GameObject PlayerReference;
    // Player name
    public string Name;

    // Number of kills and deaths
    public int Kills;
    public int Deaths;

    // is the player active/playing?
    public bool active;

    // Parts per-player
    public bool P1;
    public bool P2;
    public bool P3;
    public bool P4;
}