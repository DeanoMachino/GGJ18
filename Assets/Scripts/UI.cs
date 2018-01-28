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
        Component comp = this.getPlayerComp(player.playerID);
        // TODO GM: Complete if needed
    }

    public void updatePlayerParts(Player player)
    {
        GameObject[] player_parts = this.getPlayerComponants(player.playerID);

        // Foreach image object
        foreach (GameObject comp in player_parts)
        {   
            if (comp.name.StartsWith("Icon-Radio-"))
            {
                Debug.Log(">>> " + comp.name);
            }
        }
        
    }

    public Component getPlayerComp(int player_id)
    {
        Component comp = new Component();
        // TODO GM: refactor
        if (player_id == 1)
        {
            comp = player_01.GetComponent(placeholder_player_parts);
        }
        else if (player_id == 2)
        {
            comp = player_02.GetComponent(placeholder_player_parts);
        }
        else if (player_id == 3)
        {
            comp = player_03.GetComponent(placeholder_player_parts);
        }
        else if (player_id == 4)
        {
            comp = player_04.GetComponent(placeholder_player_parts);
        }
        return comp;
    }

    public GameObject[] getPlayerComponants(int player_id)
    {
        Component comp = this.getPlayerComp(player_id);
        return comp.GetComponents<GameObject>();
    }
}
