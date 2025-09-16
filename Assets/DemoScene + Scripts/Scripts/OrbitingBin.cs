using UnityEngine;

public class OrbitingBin : MonoBehaviour
{
    public Transform player;          // drag & drop (ou tag "Player")
    public float radius = 1.6f;       // distance au joueur
    public float orbitSpeed = 180f;   // °/s

    void Start()
    {
        if (!player) player = GameObject.FindWithTag("Player")?.transform;
        if (player) transform.position = player.position + Vector3.right * radius;
    }

    void Update()
    {
        if (!player) return;
        transform.RotateAround(player.position, Vector3.forward, orbitSpeed * Time.deltaTime);
        var dir = (transform.position - player.position).normalized;
        transform.position = player.position + dir * radius;       // garde le rayon constant
        transform.Rotate(0, 0, 360f * Time.deltaTime);             // (optionnel) spin visuel propre à ta prefab
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) Destroy(other.gameObject);
        // ou : other.GetComponent<EnemyHealth>()?.TakeDamage(999);
    }
}
