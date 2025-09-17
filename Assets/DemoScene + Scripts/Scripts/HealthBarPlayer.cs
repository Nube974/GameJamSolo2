using UnityEngine;

public class HealthBarPlayer : MonoBehaviour
{
    public PlayerHealth target;
    public Transform fill;

    void LateUpdate()
    {
        if (!target || !fill) return;
        fill.localScale = new Vector3(Mathf.Max(0f, target.Ratio), 1f, 1f);
        // Option: toujours face caméra 2D
        transform.rotation = Quaternion.identity;
    }
}
