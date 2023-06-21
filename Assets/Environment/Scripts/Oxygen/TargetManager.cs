using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public Material currMaterial;
    public GameObject target;
    public List<GameObject> outerRings;
    public List<GameObject> innerRings;

    public float timeSinceHit = 30f;
    public float timeBetweenHits = 5f;

    public bool isHit = false;
    public bool isDone = false;
    public Material okColor;
    public Material nokColor;

    void Start()
    {
        currMaterial = innerRings[0].GetComponent<Material>();
        UpdateOuterMaterial(nokColor);
    }

    void Update()
    {
        if (isDone) return;

        timeSinceHit += Time.deltaTime;
        if (timeSinceHit >= timeBetweenHits)
        {
            isHit = false;
            UpdateOuterMaterial(nokColor);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isHit) return;

        if (other.gameObject.GetComponent<Material>() == currMaterial)
        {
            Debug.Log("HIT");
            isHit = true;
            timeSinceHit = 0f;
            UpdateOuterMaterial(okColor);
        }
    }

    public void UpdateOuterMaterial(Material material)
    {
        for (int i = 0; i < outerRings.Count; i++)
        {
            outerRings[i].GetComponent<Renderer>().material = material;
        }
    }

    public void UpdateInnerMaterial(Material material)
    {
        for (int i = 0; i < innerRings.Count; i++)
        {
            innerRings[i].GetComponent<Renderer>().material = material;
        }
    }
}