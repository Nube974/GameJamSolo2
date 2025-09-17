using UnityEngine;

public class EnemySpawnerLite : MonoBehaviour
{
    public GameObject enemyPrefab;      // ici tu mets ton prefab MovingObject1
    public Transform[] spawnPoints;     // les endroits d’apparition
    public float interval = 2f;         // temps entre spawns

    void Start()
    {
        InvokeRepeating(nameof(Spawn), interval, interval);
    }

    void Spawn()
    {
        if (spawnPoints.Length == 0) return;
        int i = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[i].position, Quaternion.identity);
    }
}
