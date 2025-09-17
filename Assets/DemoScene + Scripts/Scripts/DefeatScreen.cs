using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatScreen : MonoBehaviour
{
    [Header("UI")]
    public GameObject panel;           // Assigne ton Panel (d�sactiv� par d�faut)
    [Tooltip("Nom de la sc�ne Menu (laisser vide pour recharger la sc�ne courante).")]
    public string mainMenuSceneName = "";

    void Awake()
    {
        if (panel) panel.SetActive(false);
    }

    /// <summary>Affiche l'�cran de d�faite et met la pause.</summary>
    public void Show()
    {
        if (panel) panel.SetActive(true);
        Time.timeScale = 0f;
    }

    /// <summary>Rejoue le niveau courant.</summary>
    public void Retry()
    {
        Time.timeScale = 1f;
        Scene s = SceneManager.GetActiveScene();
        SceneManager.LoadScene(s.buildIndex);
    }

    /// <summary>Retour au menu (si 'mainMenuSceneName' est vide, recharge la sc�ne courante).</summary>
    public void ToMenu()
    {
        Time.timeScale = 1f;
        if (!string.IsNullOrEmpty(mainMenuSceneName))
            SceneManager.LoadScene(mainMenuSceneName);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>Quitte l'application (ne fait rien dans l'Editor).</summary>
    public void QuitGame()
    {
        Time.timeScale = 1f;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
