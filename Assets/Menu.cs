using UnityEngine;
using UnityEngine.SceneManagement; // Required for loading and unloading scenes

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;

    // Method to switch to the next scene
    public void StartGame()
    {
        // Assuming you want to load the next scene by index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Method to open settings menu
    public void Settings()
    {
        if (mainMenu != null)
            mainMenu.SetActive(false);

        if (settingsMenu != null)
            settingsMenu.SetActive(true);
    }

    // Method to exit the game
    public void Exit()
    {
        // This will only work in a build, not in the Unity editor
        Application.Quit();

        // If you want to be able to stop play mode in the Unity editor, use this:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
