using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject CharacterTesty;
    public static GameObject[] SpawnLocations;
    public List<Player> players = new List<Player>();
    public static GameManager Instance;
    public static float GRAVITY = -25f;
    private List<PlayerController> _players;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        SpawnLocations = GameObject.FindGameObjectsWithTag("Spawns");
        SetUpPlayers(4);
    }

    // Select what you want to update, index of player that needs to be updated, index of player of killed
    void UpdateScore(Score WhatToUpdate, int IndexPlayer, int IndexKilled)
    {
        switch (WhatToUpdate)
        {
            case Score.GotPart1:
                players[IndexPlayer].P1 = true;
                break;
            case Score.GotPart2:
                players[IndexPlayer].P2 = true;
                break;
            case Score.GotPart3:
                players[IndexPlayer].P3 = true;
                break;
            case Score.GotPart4:
                players[IndexPlayer].P4 = true;
                break;
            case Score.Kill:
                players[IndexPlayer].Kills++;
                players[IndexKilled].Deaths++;
                break;
        }
        Won(CheckIfWon());
    }

    Player CheckIfWon()
    {
        foreach (Player play in players)
        {
            if (play.P1 && play.P2 && play.P3 && play.P4)
            {
                return play;
            }
        }
        return null;
    }

    void Won(Player WhoWon)
    {
        if(WhoWon != null)
        {
            Debug.Log(WhoWon.Name + " Won!!");
        }
    }

    //Setup Players
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

public enum Score
{
    GotPart1,
    GotPart2,
    GotPart3,
    GotPart4,
    Kill
};

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
        PlayerReference = player;
    }
}