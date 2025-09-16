using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    int health = 3;

   public void  TakeHit(int dmg = 1)
    {
        health -= dmg;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
