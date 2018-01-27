using UnityEngine;
using UnityEditor.UI;

public class MainMenu : MonoBehaviour {


    public void Start()
    {
        // TODO: Randomly pick a dog to display
    }

	// Quits game when called
    public void onQuitEvent()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }
}
