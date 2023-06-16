using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Scenes.Carol.Scripts;

public class SatelliteDish : MonoBehaviour, ICollectible
{
    [SerializeField]
    private GameObject neil_ground;
    [SerializeField]
    private GameObject umpa_ground;
    public static event CollectibleAction OnCollect;
    public delegate void CollectibleAction(ItemData itemData);
    public ItemData dishData;
  
    public void Collect()
    {
        if (neil_ground == null && umpa_ground == null)
        {
            OnCollect?.Invoke(dishData);
            Debug.Log("Satellite Dish Collected");
            Destroy(gameObject);
        }
    }
}