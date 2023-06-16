using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Scenes.Carol.Scripts;

public class SatelliteDish : MonoBehaviour, ICollectible
{
    
    [SerializeField]
    private GameObject umpa_ground;
    
    [SerializeField]
    private GameObject neil_ground;
    
    public static event CollectibleAction OnCollect;
    public delegate void CollectibleAction(ItemData itemData);
    public ItemData itemData;
    public void Collect()
    {
        if (neil_ground == null && umpa_ground == null)
        {
            Destroy(gameObject);
            OnCollect?.Invoke(itemData);
        }
      
    }
}
