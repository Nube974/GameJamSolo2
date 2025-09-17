using UnityEngine;
using UnityEngine.UI;

public class XPBarUI : MonoBehaviour
{
    public PlayerXP target;
    public Image fill;

    void Awake()
    {
        if (!target) target = FindObjectOfType<PlayerXP>();
        if (!fill) fill = GetComponentInChildren<Image>(); // si tu mets ce script sur le parent
    }

    void OnEnable()
    {
        if (target) target.onXpChanged.AddListener(Refresh);
        Refresh(); // affiche l’état actuel dès l’ouverture
    }

    void OnDisable()
    {
        if (target) target.onXpChanged.RemoveListener(Refresh);
    }

    void Update()
    {
        // Fallback au cas où l'event n'est pas câblé
        if (target && fill) fill.fillAmount = target.XpRatio;
    }

    public void Refresh()
    {
        if (target && fill) fill.fillAmount = target.XpRatio;
    }
}
