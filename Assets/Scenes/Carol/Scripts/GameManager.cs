using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;

            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static GameManager instance;
    #endregion

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        UpdateGameState(GameState.Menu);
    }

    public GameState state;

    public static event Action<GameState> OnGameStateChanged;

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch(newState)
        {
            case GameState.Menu:
                break;
            case GameState.GameWithCat:
                break;
            case GameState.GameWithoutCat:
                break;
            case GameState.Win:
                break;
            case GameState.GameOver:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    #region Inventory

    public List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();

    public void OnEnable()
    {
        SatelliteDish.OnCollect += AddItemToInventory;
    }

    public void OnDisable()
    {
        SatelliteDish.OnCollect -= RemoveItemFromInventory;

    }

    public void AddItemToInventory(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
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

    public void RemoveItemFromInventory(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.RemoveFromStack();
            if (item.stackSize == 0)
            {
                inventory.Remove(item);
                itemDictionary.Remove(itemData);
            }
        }
    }

    #endregion
}

public enum GameState
{
    Menu,
    GameWithCat,
    GameWithoutCat,
    Win,
    GameOver
}
