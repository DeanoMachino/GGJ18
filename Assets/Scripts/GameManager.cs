using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] SpawnLocations;
    public List<Player> players = new List<Player>();
    public static GameManager Instance;
    public static float GRAVITY = -25f;
    public bool GameEnded = false;
    public GameObject playerPrefab;
    public GameObject EndGameUI;
    public string Winner;

    public Transform MapParent;
    public GameObject blockPrefab;
    public GameObject blockTopPrefab;
    public GameObject platformPrefab;

    public Text countdownTextUI;

    private bool _countingDown;
    private float _countdownValue;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        CreateMap();
        AudioManager.Instance.playBackgroundMusic(AudioManager.AvailableMusicClips.ingameMusic);
        SetUpPlayers(4);

        _countdownValue = 3.9f;
        _countingDown = true;
        countdownTextUI.gameObject.SetActive(true);
    }

    public bool IsCountingDown()
    {
        return _countingDown;
    }

    private void Update()
    {
        if (_countingDown && _countdownValue > 1)
        {
            _countdownValue -= Time.deltaTime;

            countdownTextUI.text = string.Format("{0}", (int)_countdownValue);

            if (_countdownValue <= 1)
            {
                _countingDown = false;
                countdownTextUI.gameObject.SetActive(false);
                AudioManager.Instance.playAudioClip(AudioManager.AvailableAudioClips.gameStart);
            }
        }
    }

    // Select what you want to update, index of player that needs to be updated, index of player of killed
    public void UpdateScore(Score WhatToUpdate, int IndexPlayer, int IndexKilled)
    {
        Debug.Log("Index: " + IndexPlayer);
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
        if (P != null)
        {
            if (!GameEnded)
            {
                //Stop Game
                GameEnded = true;

                //Open End Game UI and close all other UI
                GameObject[] UIs = GameObject.FindGameObjectsWithTag("UI");

                foreach (GameObject GO in UIs)
                {
                    Debug.Log(GO);
                    Destroy(GO);
                }

                GameObject SpawnEndGameUI = Instantiate(EndGameUI, transform.position, transform.rotation) as GameObject;
                Debug.Log(SpawnEndGameUI);
                Winner = P.Name;
            }

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
    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void KillPlayer(int id)
    {
        Destroy(players[id].PlayerReference);
        players[id].Spawn();
    }

    private void CreateMap()
    {
        int[,] rawGrid = new int[16, 30] {
            {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 3, 3, 3, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 3, 3, 3, 0, 0, 0, 0, 0, 0},
            {2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 1, 1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 2, 2},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 1, 1, 0, 0, 0, 0, 0, 0},
            {2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2},
            {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
            {1, 1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 1, 1},
            {1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1},
            {1, 1, 1, 2, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 2, 2, 2, 2, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1}
        };

        for (int y = 0; y < 16; y++)
        {
            for (int x = 0; x < 30; x++)
            {
                GameObject prefab = null;

                switch ((MapTiles)rawGrid[y, x])
                {
                    case MapTiles.Block:
                        prefab = blockPrefab;
                        break;
                    case MapTiles.Block_Top:
                        prefab = blockTopPrefab;
                        break;
                    case MapTiles.Platform:
                        prefab = platformPrefab;
                        break;
                    default:
                        break;
                }

                if (prefab != null)
                {
                    GameObject tile = Instantiate(prefab);
                    tile.transform.SetParent(MapParent);
                    tile.transform.localPosition = new Vector3(-14.5f + x, 7.5f - y, 0);
                }
            }
        }
    }
}

public enum MapTiles
{
    Empty = 0,
    Block = 1,
    Block_Top = 2,
    Platform = 3
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

    public Player(int playerID)
    {
        this.playerID = playerID;
        Spawn();
    }

    public float GetChargeAmount()
    {
        return PlayerReference.GetComponent<PlayerController>().GetChargeProgress();
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