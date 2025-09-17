using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public int max = 3, hp;
    public UnityEvent onDeath;
    void Awake() 
    { 
        hp = max; 
    }
    public float Ratio => (float) hp / max;
    public void TakeHit(int d = 1)
    {
        hp = Mathf.Max(0, hp - d);
        if (hp == 0)
        {
            var defeat = FindObjectOfType<DefeatScreen>(); if (defeat) defeat.Show();
            onDeath?.Invoke();
            Destroy(gameObject,Time.deltaTime);
        }
    }
}
