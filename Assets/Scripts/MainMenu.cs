using UnityEngine;
using UnityEditor.UI;
using UnityEditor.SceneManagement;

public class MainMenu : MonoBehaviour {

    /// <summary>
    /// Play the game
    /// </summary>
    public void onPlayEvent()
    {
        Debug.Log("Starting game...");
        Application.LoadLevel("Game");
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
