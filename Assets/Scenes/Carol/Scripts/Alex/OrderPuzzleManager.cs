using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderPuzzleManager : MonoBehaviour
{
    public List<GameObject> targets;
    public List<Material> materials;
    
    public int roundId = 0;
    public bool isDone = false;

    // Start is called before the first frame update
    void Start()
    {
        roundId = 0;
        isDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDone) return;

        for (int i = 0; i < targets.Count; i++)
        {
            if (!targets[i].GetComponent<TargetManager>().isHit)
            {
                return;
            }
        }

        roundId++;
        for (int i = 0; i < targets.Count; i++)
        {
            TargetManager manager = targets[i].GetComponent<TargetManager>();
            manager.UpdateInnerMaterial(materials[roundId]);
            manager.UpdateOuterMaterial(manager.nokColor);
        }
        if (roundId == 2)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].GetComponent<TargetManager>().isDone = true;
            }
            isDone = true;
        }
    }
}
