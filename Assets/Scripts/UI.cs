using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    public GameObject player_01;
    public GameObject player_02;
    public GameObject player_03;
    public GameObject player_04;

    public Sprite radio_active_1;
    public Sprite radio_active_2;
    public Sprite radio_active_3;
    public Sprite radio_active_4;

    private string placeholder_player_parts = "Player-Parts";

    // Use this for initialization
    void Start () {
        // Do nothing if null
        if (GameManager.Instance == null) { return; }

        this.deactivatePlayers();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Do nothing if null
        if (GameManager.Instance == null) { return; }
        // Update each players UI parts
        foreach (Player player in GameManager.Instance.players)
        {
            this.updatePlayerParts(player);
        }
    }

    public void deactivatePlayers()
    {
        // Check if a player needs to be deactivated
        foreach (Player player in GameManager.Instance.players) {
            if (!player.active) {
                this.deactivatePlayer(player);
            }
        }
    }

    public void deactivatePlayer(Player player)
    {
        // TODO GM: Complete if needed
    }

    public void activateRadioByIndex(GameObject[] radios, int index)
    {
        foreach (GameObject radio in radios)
        {
            if (radio.name == "Icon-Radio-0" + index.ToString())
            {
                if (index == 1)
                {
                    radio.GetComponent<Image>().sprite = this.radio_active_1;
                }
                if (index == 2)
                {
                    radio.GetComponent<Image>().sprite = this.radio_active_2;
                }
                if (index == 3)
                {
                    radio.GetComponent<Image>().sprite = this.radio_active_3;
                }
                if (index == 4)
                {
                    radio.GetComponent<Image>().sprite = this.radio_active_4;
                }
            }
        }
    }

    public void updatePlayerParts(Player player)
    {
        // TODO: Refactor
        int index = player.playerID + 1;
        GameObject[] radios = GameObject.FindGameObjectsWithTag("Player-0" + index.ToString());

        if (player.P1)
        {
            this.activateRadioByIndex(radios, 1);
        }
        if (player.P2)
        {
            this.activateRadioByIndex(radios, 2);
        }
        if (player.P3)
        {
            this.activateRadioByIndex(radios, 3);
        }
        if (player.P4)
        {
            this.activateRadioByIndex(radios, 4);
        }
    }
}
