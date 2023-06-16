using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Scenes.Carol.Scripts;


public class Component : MonoBehaviour, ICollectible
    {
        public static event CollectibleAction OnCollect;
        public delegate void CollectibleAction(ItemData itemData);
        public ItemData itemData;
        
        public void Collect()
        {
            Destroy(gameObject);
            OnCollect?.Invoke(itemData);
        }
    }
