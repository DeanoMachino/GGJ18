using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] SpawnLocations;
    public List<Player> players = new List<Player>();
    public static GameManager Instance;
    public static float GRAVITY = -25f;
    public bool GameEnded = false;
    public GameObject playerPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        SetUpPlayers(4);
    }

    // Select what you want to update, index of player that needs to be updated, index of player of killed
    public void UpdateScore(Score WhatToUpdate, int IndexPlayer, int IndexKilled)
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
        Debug.Log("No one Won!");
        return null;
    }

    void Won(Player P)
    {
        if(P != null)
        {
            //Stop Game
            GameEnded = true;

            //Open End Game UI and close all other UI
            Debug.Log(P.Name + "Won");
        }
    }

    //Setup Players
    void SetUpPlayers(int QuantPlayers)
    {
        for (int a = 0; a < QuantPlayers; a++)
        {
            Player PlayerToAdd = new Player(a);
            PlayerToAdd.Name = "Player" + a + 1;
            players.Add(PlayerToAdd);
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
    public int playerID;

    // Player name
    public string Name;

    // Number of kills and deaths
    public int Kills;
    public int Deaths;

    public int AvatarIndex = 0;

    // is the player active/playing?
    public bool active;

    // Parts per-player
    public int TotalAmountOfParts()
    {
        int TempAmount = 0;
        bool[] GotParts = { P1, P2, P3, P4 };
        foreach (bool B in GotParts)
        {
            TempAmount++;
        }
        return TempAmount;
    }
    public bool P1;
    public bool P2;
    public bool P3;
    public bool P4;

    public Player(int playerID) {
        this.playerID = playerID;
        Spawn();
    }

    public void Spawn()
    {
        GameObject player = GameManager.Instantiate(GameManager.Instance.playerPrefab) as GameObject;
        player.transform.position = GameManager.Instance.SpawnLocations[playerID].transform.position;
        player.GetComponent<PlayerController>().Initialise(playerID);
        PlayerReference = player;
    }
    public int Rank = 0;
}