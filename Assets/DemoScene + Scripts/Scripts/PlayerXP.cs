using UnityEngine;
using UnityEngine.Events;

public class PlayerXP : MonoBehaviour
{
    public int level = 1;
    public int xp = 0;
    public int xpToNext = 10;                  // palier courant
    public float growth = 1.5f;                // multiplicateur du prochain palier

    public UnityEvent onLevelUp;               // brancher UI LevelUp
    public UnityEvent onXpChanged;             // brancher la barre d’XP

    public void AddXP(int amount)
    {
        xp += amount; onXpChanged?.Invoke();
        while (xp >= xpToNext)                 // gère multi-level en un kill
        {
            xp -= xpToNext; level++;
            xpToNext = Mathf.CeilToInt(xpToNext * growth);
            onLevelUp?.Invoke();
        }
    }

    public float XpRatio => Mathf.Clamp01((float)xp / xpToNext);
}
