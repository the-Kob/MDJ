using System.Collections;
using System.Collections.Generic;
using System;
using Scenes.Carol.Scripts;
using UnityEngine;

public class Components : MonoBehaviour, ICollectible
{
    public static event HandleAction OnCollect;
    public delegate void HandleAction(ItemData itemData);
    public ItemData componentData;
    
    public void Collect()
    {
        OnCollect?.Invoke(componentData);
        Destroy(gameObject);
    }
}
   

