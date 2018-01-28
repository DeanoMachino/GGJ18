using UnityEngine;
using UnityEditor.UI;

public class MainMenu : MonoBehaviour {

    /// <summary>
    /// Play the game
    /// </summary>
    public void onPlayEvent()
    {
        Debug.Log("Starting game...");
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
