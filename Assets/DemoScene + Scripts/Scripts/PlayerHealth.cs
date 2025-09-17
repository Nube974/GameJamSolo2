using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int max = 3, hp;
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

            Destroy(gameObject,Time.deltaTime);
        }
    }
}
