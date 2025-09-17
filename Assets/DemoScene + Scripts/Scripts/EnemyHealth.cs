using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int hp = 1;
    public int xpValue = 2;

    public void TakeDamage(int d = 1)
    {
        hp -= d;
        if (hp <= 0) Kill();
    }

    public void Kill()
    {
        var pxp = FindFirstObjectByType<PlayerXP>();
        if (pxp) pxp.AddXP(xpValue);
        Destroy(gameObject);
    }
}
