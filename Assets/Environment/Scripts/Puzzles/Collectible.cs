using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Scenes.Carol.Scripts;

public class Collectible : MonoBehaviour, ICollectible
{
    
    public static event CollectibleAction OnCollect;
    public delegate void CollectibleAction(ItemData itemData);
    public ItemData collectibleData;
  
    public void Collect()
    {
            OnCollect?.Invoke(collectibleData);
            Destroy(gameObject);
    }
}