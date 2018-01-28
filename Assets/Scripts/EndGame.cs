using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

    public Sprite player_1;
    public Sprite player_1_lose;
    public Sprite player_2;
    public Sprite player_2_lose;
    public Sprite player_3;
    public Sprite player_3_lose;
    public Sprite player_4;
    public Sprite player_4_lose;

    public GameObject replay_button;

    // Use this for initialization
    void Update () {

        GameManager gm = GameManager.Instance;

        if (gm.GameEnded)
        {
            int looser_index = 1;
            foreach (Player player in gm.players)
            {
                Sprite sprite;
                bool is_winner = false;
                
                // Winner
                if (gm.Winner == player.Name)
                {
                    Transform tranny = this.transform.Find("Player-Looser_");
                    if (tranny == null) { continue; }

                    GameObject winner = tranny.gameObject;
                    sprite = winner.GetComponent<Sprite>();
                    is_winner = true;
                } else // loosers
                {
                    Transform tranny = this.transform.Find("Player-Looser_" + looser_index.ToString());
                    if (tranny == null) { continue; }
                    GameObject looser = tranny.gameObject;
                    sprite = looser.GetComponent<Sprite>();
                    looser_index += 1;
                }

                sprite = this.getSpriteByName(player, is_winner);
            }

        }
        
	}

    public Sprite getSpriteByName(Player player, bool win)
    {
        if (player.playerID == 1)
        {
            if (win) { return player_1; } else { return player_1_lose; }
        }
        if (player.playerID == 2)
        {
            if (win) { return player_2; } else { return player_2_lose; }
        }
        if (player.playerID == 3)
        {
            if (win) { return player_3; } else { return player_3_lose; }
        }
        else
        {
            if (win) { return player_4; } else { return player_4_lose; }
        }
    }
	

    public void Replay()
    {
        GameManager gm = GameManager.Instance;
        gm.RestartGame();
    }

    public void goToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
