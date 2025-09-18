using UnityEngine;

public class EnemySpawnerLite : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyPrefab2;
    public Transform[] spawnPoints;
    public float interval = 2f;          // intervalle de départ

    // ---- Accélération ----
    public float accelerateAfter = 10f;  // après combien de secondes on commence à accélérer
    public float accelEvery = 5f;        // tous les combien on accélère ensuite
    public float accelFactor = 0.9f;     // multiplicateur (<1) appliqué à l'intervalle
    public float minInterval = 0.3f;     // limite basse

    float nextAccelTime;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), interval, interval);
        nextAccelTime = Time.time + accelerateAfter;
    }

    void Update()
    {
        if (Time.time >= nextAccelTime)
        {
            interval = Mathf.Max(minInterval, interval * accelFactor);     // réduit l'intervalle
            CancelInvoke(nameof(Spawn));                                    // reprogramme avec le nouvel intervalle
            InvokeRepeating(nameof(Spawn), interval, interval);
            nextAccelTime += accelEvery;                                    // prochaine accélération
        }
    }

    void Spawn()
    {
        if (spawnPoints == null || spawnPoints.Length == 0 || enemyPrefab == null) return;
        int i = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[i].position, Quaternion.identity);
    }
}
