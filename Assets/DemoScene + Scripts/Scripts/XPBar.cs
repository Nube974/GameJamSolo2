using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    public PlayerXP target;
    public Image fill; // Remplacer Transform par Image

    void OnEnable() { Refresh(); }
    public void Refresh()
    {
        if (target && fill) fill.fillAmount = target.XpRatio;
    }
    void Update() { Refresh(); }  // simple et suffisant pour la jam
}
