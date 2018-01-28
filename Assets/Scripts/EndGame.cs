using UnityEngine;

public class EndGame : MonoBehaviour {

    public Sprite player_1;
    public Sprite player_2;
    public Sprite player_3;
    public Sprite player_4;

    // Use this for initialization
    void Update () {

        GameManager gm = GameManager.Instance;

        if (gm.GameEnded)
        {
            int looser_index = 1;
            foreach (Player player in gm.players)
            {
                Sprite sprite;
                
                // Winner
                if (gm.Winner == player.Name)
                {
                    GameObject winner = this.transform.Find("Player-Winner").gameObject;
                    sprite = winner.GetComponent<Sprite>();
                } else // loosers
                {
                    GameObject looser = this.transform.Find("Player-Looser_" + looser_index.ToString()).gameObject;
                    sprite = looser.GetComponent<Sprite>();
                    looser_index += 1;
                }

                sprite = this.getSpriteByName(player);
            }

        }
        
	}

    public Sprite getSpriteByName(Player player)
    {
        if (player.playerID == 1)
        {
            return player_1;
        }
        if (player.playerID == 2)
        {
            return player_2;
        }
        if (player.playerID == 3)
        {
            return player_3;
        }
        return player_4;
    }
	
}
