using UnityEngine;
using UnityEngine.Events;

public class VictoryConditions : MonoBehaviour
{
    [Header("Victoire par quota (ex: 100 recyclés / kills)")]
    public bool useQuota = true;
    public int targetCount = 50;
    public int currentCount = 0;

    [Header("Victoire par temps (ex: survivre 300s)")]
    public bool useTimer = false;
    public float surviveSeconds = 300f;
    float t;

    [Header("Événements")]
    public UnityEvent onVictory;  // brancher l'écran de victoire

    bool won;

    void Update()
    {
        if (won) return;

        if (useTimer)
        {
            t += Time.deltaTime;
            if (t >= surviveSeconds) Win();
        }
        if (useQuota && currentCount >= targetCount) Win();
    }

    public void AddProgress(int amount = 1)
    {
        if (won) return;
        currentCount += amount;
        if (useQuota && currentCount >= targetCount) Win();
    }

    public void Win()
    {
        if (won) return;
        won = true;
        onVictory?.Invoke();
    }
}
