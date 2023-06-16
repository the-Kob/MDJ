using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Scenes.Carol.Scripts;

public class SatelliteDish : MonoBehaviour, ICollectible
{
    
    public static event CollectibleAction OnCollect;
    public delegate void CollectibleAction(ItemData itemData);
    public ItemData dishData;
  
    public void Collect()
    {
        OnCollect?.Invoke(dishData);
        Debug.Log("Satellite Dish Collected");
        Destroy(gameObject);
    }
}