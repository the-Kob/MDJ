using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject cablePartPrefab, parentObject;

    [SerializeField]
    [Range(1, 1000)]
    int length = 8;

    [SerializeField]
    [Range(0, 2)]
    // Distance between cable parts
    float cablePartDistance = 0.5f; 

    [SerializeField]
    bool reset, spawn, snapFirst, snapLast;


    // Update is called once per frame
    void Update()
    {
        if (reset)
        {
            foreach (GameObject cablePart in GameObject.FindGameObjectsWithTag("CablePart"))
            {
                Destroy(cablePart);
            }

            reset = false;
        }

        if (spawn)
        {
            Spawn();
            spawn = false;
        }
    }


    public void Spawn()
    {
        int count = (int)(length / cablePartDistance);

        for (int i = 0; i < count; i++)
        {
            GameObject cablePart;

            cablePart = Instantiate(cablePartPrefab,
                                    new Vector3(
                                        transform.position.x,
                                        transform.position.y + cablePartDistance * (i + 1),
                                        transform.position.z
                                    ),
                                    Quaternion.identity,
                                    parentObject.transform
                        );

            cablePart.transform.eulerAngles = new Vector3(180, 0, 0);

            cablePart.name = parentObject.transform.childCount.ToString();

            if (i == 0)
            {
                Destroy(cablePart.GetComponent<CharacterJoint>());

                if (snapFirst)
                {
                    cablePart.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                }
            }
            else
            {
                cablePart.GetComponent<CharacterJoint>().connectedBody =
                    parentObject.transform
                    .Find((parentObject.transform.childCount - 1).ToString())
                    .GetComponent<Rigidbody>();
            }
        }

        if (snapLast)
        {
            parentObject.transform.Find((parentObject.transform.childCount).ToString()).GetComponent<Rigidbody>().constraints
                = RigidbodyConstraints.FreezePosition;
        }
    }
}
