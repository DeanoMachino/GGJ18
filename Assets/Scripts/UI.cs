using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    public GameObject player_01;
    public GameObject player_02;
    public GameObject player_03;
    public GameObject player_04;

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
        // Fix off by one
        index = index + 1;
        foreach (GameObject radio in radios)
        {
            if (radio.name == "Icon-Radio-0" + index.ToString())
            {
                string path_to_sprite = "Sprites/Item-RadioComponant/radiopart-" + index.ToString();
                Sprite image = Resources.Load(path_to_sprite) as Sprite;
                radio.GetComponent<SpriteRenderer>().sprite = image;
            }
        }
    }

    public void updatePlayerParts(Player player)
    {
        // TODO: Refactor
        GameObject[] radios = GameObject.FindGameObjectsWithTag("Player-0" + player.playerindex.ToString());

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
