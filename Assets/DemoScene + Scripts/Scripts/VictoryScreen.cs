using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public GameObject panel;  // ton Panel UI (désactivé par défaut)

    void Awake() { if (panel) panel.SetActive(false); }

    public void Show()
    {
        if (panel) panel.SetActive(true);
        Time.timeScale = 0f; // pause pendant l'écran de victoire
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        int count = SceneManager.sceneCountInBuildSettings;
        int i = SceneManager.GetActiveScene().buildIndex;
        int next = (i + 1) % count; // boucle vers 0 si dernier niveau
        SceneManager.LoadScene(next);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        Scene s = SceneManager.GetActiveScene();
        SceneManager.LoadScene(s.buildIndex);
    }

    public void ReturnMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");

    }
}
