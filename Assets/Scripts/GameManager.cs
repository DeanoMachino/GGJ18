using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject CharacterTesty;

    public static GameObject[] SpawnLocations;

    public List<Player> players = new List<Player>();

    public static GameManager Instance;

    public static float GRAVITY = -25f;

    private void Awake() {
        Instance = this;
    }

    public void Start()
    {
        SpawnLocations = GameObject.FindGameObjectsWithTag("Spawns");
        SetUpPlayers(4);
    }

    void SetUpPlayers(int QuantPlayers)
    {
        for (int a = 0; a < QuantPlayers; a++)
        {
            Player PlayerToAdd = new Player();
            PlayerToAdd.Name = "Player" + a + 1;
            PlayerToAdd.playerindex = a;
            players.Add(PlayerToAdd);
            PlayerToAdd.Spawn(CharacterTesty);
        }
    }
}

public class Player
{
    // player objects
    public GameObject PlayerReference;
    public int playerindex;

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

    public void Spawn(GameObject playerPreFab)
    {
        GameObject player = GameManager.Instantiate(playerPreFab) as GameObject;
        player.transform.position = GameManager.SpawnLocations[playerindex].transform.position;
    }
}