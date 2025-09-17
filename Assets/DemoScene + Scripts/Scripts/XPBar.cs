using UnityEngine;
using UnityEngine.UI;

public class XPBarUI : MonoBehaviour
{
    public PlayerXP target;
    public Image fill;

    [Header("Animation")]
    public bool animateLevelUp = true;
    public float flashTime = 0.15f;   // temps pour aller à 100%
    bool animating;

    void Awake()
    {
        if (!target) target = FindObjectOfType<PlayerXP>();
        if (!fill) fill = GetComponentInChildren<Image>(true);
        if (fill && fill.type != Image.Type.Filled) fill.type = Image.Type.Filled;
        if (fill) { fill.fillMethod = Image.FillMethod.Horizontal; fill.fillOrigin = 0; }
    }

    void OnEnable()
    {
        if (target)
        {
            target.onXpChanged.AddListener(Refresh);
            target.onLevelUp.AddListener(FlashLevelUp);
        }
        Refresh();
    }

    void OnDisable()
    {
        if (target)
        {
            target.onXpChanged.RemoveListener(Refresh);
            target.onLevelUp.RemoveListener(FlashLevelUp);
        }
    }

    public void Refresh()
    {
        if (!fill || !target || animating) return;
        fill.fillAmount = target.XpRatio;
    }

    void FlashLevelUp()
    {
        if (!animateLevelUp || !gameObject.activeInHierarchy || !fill) { Refresh(); return; }
        StopAllCoroutines();
        StartCoroutine(CoFlashToFullThenReset());
    }

    System.Collections.IEnumerator CoFlashToFullThenReset()
    {
        animating = true;

        // 1) Anime jusqu’à 100%
        float start = fill.fillAmount;
        float t = 0f;
        while (t < flashTime)
        {
            t += Time.unscaledDeltaTime;               // insensible à la pause éventuelle
            float k = Mathf.Clamp01(t / flashTime);
            fill.fillAmount = Mathf.Lerp(start, 1f, k);
            yield return null;
        }
        fill.fillAmount = 1f;
        yield return null;                              // laisse 1 frame

        // 2) Réapplique le nouveau ratio (xp/xpToNext après level up)
        animating = false;
        Refresh();
    }
}
