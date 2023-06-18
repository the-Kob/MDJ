using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Inventory : MonoBehaviour
    {
        public List<InventoryItem> inventory = new List<InventoryItem>();
        private Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();

        public void OnEnable()
        {
            SatelliteDish.OnCollect += Add;
            Components.OnCollect += Add;
        }
        
        public void OnDisable()
        {
            SatelliteDish.OnCollect -= Add;
            Components.OnCollect -= Add;
            
        }

        public void Add(ItemData itemData)
        {
            if(itemDictionary.TryGetValue(itemData , out InventoryItem item))
            {
                item.AddToStack();
                Debug.Log($"{item.itemData.displayName} total stack is now: {item.stackSize}");
            }
            else
            {
                InventoryItem newItem = new InventoryItem(itemData);
                inventory.Add(newItem); //add to list of inventory items
                itemDictionary.Add(itemData, newItem); //add to dictionary
                Debug.Log($"Added {itemData.displayName} to inventory");
            }
        }
        
        public void Remove(ItemData itemData)
        {
            if(itemDictionary.TryGetValue(itemData, out InventoryItem item))
            {
                item.RemoveFromStack();
                if(item.stackSize == 0)
                {
                    inventory.Remove(item);
                    itemDictionary.Remove(itemData);
                }
            }
        }
    }
