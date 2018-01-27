using UnityEngine;
using UnityEditor.UI;

public class MainMenu : MonoBehaviour {

    public void onStartEvent()
    {

    }

	// Quits game when called
    public void onQuitEvent()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }
}
