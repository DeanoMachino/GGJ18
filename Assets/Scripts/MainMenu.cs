using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    void Start()
    {
        AudioManager.Instance.playBackgroundMusic(AudioManager.AvailableMusicClips.mainMenuMusic);
    }

    /// <summary>
    /// Play the game
    /// </summary>
    public void onPlayEvent()
    {
        Debug.Log("Starting game...");
        SceneManager.LoadScene("Game");
    }

	/// <summary>
    /// Quit function
    /// </summary>
    public void onQuitEvent()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }
}
