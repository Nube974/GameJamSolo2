using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    public OrbitingBinsController bins;
    public PlayerXP playerXP;

    void Awake() { gameObject.SetActive(false); }

    public void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f; // pause pendant le choix
    }

    public void Choose_AddBin() { bins.Upgrade_AddBin(); Close(); }
    public void Choose_Speed() { bins.Upgrade_Speed(); Close(); }
    public void Choose_Radius() { bins.Upgrade_Radius(); Close(); }

    void Close()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
