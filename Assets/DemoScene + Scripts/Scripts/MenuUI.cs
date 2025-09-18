using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [Tooltip("Nom de la scène de jeu à charger")]
    public string gameSceneName = "Level 1";  // ou laisse vide pour charger index+1

    public void StartGame()
    {
        Time.timeScale = 1f;
        if (!string.IsNullOrEmpty(gameSceneName))
            SceneManager.LoadScene(gameSceneName);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;   // stop en Editor
#else
        Application.Quit();                                // quitte en build
#endif
    }
}
