using System;
using UnityEngine;

public class OrbitingBinsController : MonoBehaviour
{
    public Transform player;
    public GameObject binPrefab;
    public int binCount = 1;
    public float radius = 1.6f;
    public float orbitSpeed = 180f;

    GameObject[] bins;
    float angle;
    bool building;               // anti-réentrance
    Transform binParent;         // conteneur propre

    void Start()
    {
        Debug.Log("Hugo is hungry");
        if (!player) player = GameObject.FindWithTag("Player")?.transform;
        binParent = new GameObject("Bins").transform;
        binParent.parent = player.transform;
        binParent.localScale = Vector3.one;
        SetBinCount(1);
        RebuildBins();
    }

    //void LateUpdate()
    //{
    //    if (!player || bins == null) return;
    //    angle += orbitSpeed * Mathf.Deg2Rad * Time.deltaTime;

    //    for (int i = 0; i < bins.Length; i++)
    //    {
    //        var b = bins[i]; if (!b) continue;
    //        float a = angle + (Mathf.PI * 2f) * i / bins.Length;
    //        Vector3 off = new Vector3(Mathf.Cos(a), Mathf.Sin(a), 0f) * radius;
    //        b.transform.position = player.position + off;
    //    }
    //}

    // ---------- Upgrades publics ----------
    public void Upgrade_AddBin(int amount = 1) { SetBinCount(Mathf.Clamp((bins?.Length ?? binCount) + amount, 1, 8)); }
    public void Upgrade_Radius(float add = 1f) { radius += add; }
    public void Upgrade_Speed(float add = 80f) { orbitSpeed += add; }

    public void SetBinCount(int newCount)
    {
        if (bins != null && bins.Length == newCount) return; // idempotent
        binCount = newCount;
        RebuildBins();
    }

    // ---------- Construction contrôlée ----------
    void RebuildBins()
    {
    
        if (building) return; building = true;

        // détruire proprement l'ancien set
        if (bins != null)
            for (int i = 0; i < bins.Length; i++) if (bins[i]) Destroy(bins[i]);
       

        bins = new GameObject[Mathf.Max(1, binCount)];
        for (int i = 0; i < bins.Length; i++)
        {
            float offset = ((float)i / bins.Length) * 360 *Mathf.Deg2Rad;
            //var b = Instantiate(binPrefab, binParent);
         
            GameObject b = Instantiate(binPrefab, binParent);
            b.transform.Rotate(Vector3.forward * offset);
            Debug.Log(b.transform.position);

            b.GetComponent<OrbitingBin>().angle = offset;
            // Assure-toi que binPrefab n'a PAS ce controller dedans !
            bins[i] = b;

        }

        building = false;
    }
}
