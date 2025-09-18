using UnityEngine;

public class OrbitingBin : MonoBehaviour
{
    public Transform player;          // drag & drop (ou tag "Player")
    public float radius = 1.6f;       // distance au joueur
    public float orbitSpeed = 180f;   // °/s

    public float angle;                      // --> angle d’orbite en radians
    public int damage = 1;

    void Start()
    {
        if (!player) player = GameObject.FindWithTag("Player")?.transform;
        if (player)
        {
            // place la poubelle sur le cercle d’orbite et initialise l’angle
            Vector3 offset = Vector3.right * radius;
            transform.position = player.position + offset;
            //angle = 0f;
        }
    }

    void LateUpdate() // après le déplacement du joueur
    {
        if (!player) return;

        // avance l’angle puis recalcule la position autour du joueur
        angle += orbitSpeed * Mathf.Deg2Rad * Time.deltaTime;
        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * radius;
        transform.position = player.position + offset;

        // (optionnel) orienter tangentiellement :
        // transform.up = Vector3.Perpendicular(offset).normalized;

        // (optionnel) spin visuel indépendant :
        // transform.Rotate(0, 0, 360f * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Touch: " + other.name);
        if (other.CompareTag("Enemy"))
        {
            var eh = other.GetComponent<EnemyHealth>();
            if (eh)
            {
                eh.TakeDamage();
            }
            else Destroy(other.gameObject);
            return;
            //Destroy(other.gameObject);
           // <-- important : on sort, pas d'accès à 'other' après Destroy
        }
        // ou : other.GetComponent<EnemyHealth>()?.TakeDamage(999);
    }
}
