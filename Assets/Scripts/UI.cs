using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public GameManager game_manager;

    public GameObject player_01;
    public GameObject player_02;
    public GameObject player_03;
    public GameObject player_04;



    // Use this for initialization
    void Start () {
        // Check if a player needs to be deactivated
        foreach (Player player in game_manager.players) {
            if (!player.active) {
                this.deactivatePlayer(player);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        // Update each players UI parts
        int counter = 1;
        foreach (Player player in game_manager.players)
        {
            this.updatePlayerParts(player, counter);
            counter += 1;
        }
    }

    public void deactivatePlayer(Player player)
    {
        // TODO: disable player if not active
    }

    public void updatePlayerParts(Player player, int player_id)
    {
        string placeholder_player_parts = "Player-Parts";
        Component player_parts = this.player_01.GetComponent(placeholder_player_parts);
        
        // Foreach image object
        foreach (GameObject comp in player_parts.GetComponents<GameObject>())
        {   
            if (comp.name.StartsWith("Icon-Radio-"))
            {
                Debug.Log(">>> " + comp.name);
            }
        }
        
    }
}
