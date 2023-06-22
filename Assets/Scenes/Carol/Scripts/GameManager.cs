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

    DateTime oldDate;
    DateTime currentDate;
    float minutes;
    const float CAT_DIES_THRESHOLD = 20.0f;
    
    public GameObject gameOverScreen;

    public List<LocalPlayer> players;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()

    {
        minutes = 0;
        oldDate = DateTime.Now;
        
        UpdateGameState(GameState.GameWithCat);
    }

    void Update()
    {
        if (CheckIfCatDies())
        {
            UpdateGameState(GameState.GameWithoutCat);
        }
    }

    private bool CheckIfCatDies()
    {
        bool catDies = false;
        currentDate = DateTime.Now;
        minutes = currentDate.Minute - oldDate.Minute;

        // If 20 minutes have passed, kill the cat
        if (minutes >= CAT_DIES_THRESHOLD)
        {
            catDies = true;
        }

        return catDies;
    }

    public GameState state;

    // TODO, we need to subscribe the entities that need to know when the game state changes to this event
    /*
     * Example:
     * 
     * On the OnEnable method of the entities (ie. player, cat) -> GameManager.OnGameStateChanged += MethodResponsibleToHandleTheStateChanged
     * On the OnDisable method of the entities (ie. player, cat) -> GameManager.OnGameStateChanged -= MethodResponsibleToHandleTheStateChanged
     * 
     * Outside, create the new method:
     * 
     * private void MethodResponsibleToHandleTheStateChanged(GameState state) {
     *      if(state == GameState.GameWithoutCat) {
     *          // change cat appearance, disable movement -> for example 
     *      }
     * }
     */
    public static event Action<GameState> OnGameStateChanged;

    public void UpdateGameState(GameState newState)
    {
        GameState oldState = state;
        state = newState;

        switch(newState)
        {
            case GameState.GameWithCat:
                StartCoroutine(HandleGameWithCat());
                break;
            case GameState.GameWithoutCat:
                StartCoroutine(HandleGameWithoutCat(oldState));
                break;
            case GameState.Win:
                break;
            case GameState.GameOver:
                StartCoroutine(HandleGameOver());
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private IEnumerator HandleGameWithCat()
    {
        // This order matters
        PlayerOxygenManager.playerOxygenManager.Reset();
        yield return new WaitForSecondsRealtime(1);
        RopeManager.Instance.Reset();
    }

    private IEnumerator HandleGameWithoutCat(GameState oldState)
    {
        if (oldState != GameState.GameWithCat)
        {
            // This order matters
            PlayerOxygenManager.playerOxygenManager.Reset();
            yield return new WaitForSecondsRealtime(1);
            RopeManager.Instance.Reset();
        }
    }

    private IEnumerator HandleGameOver()
    {
        PlayerOxygenManager.playerOxygenManager.stopOxygen = true;

        foreach(LocalPlayer player in players) {
            player.isGameOver = true;
        }
        
        yield return new WaitForSecondsRealtime(2f);

        gameOverScreen.SetActive(true);

        if (CheckIfCatDies())
        {
            UpdateGameState(GameState.GameWithoutCat);
        }
        else
        {
            UpdateGameState(GameState.GameWithCat);
        }
        
        yield return new WaitForSecondsRealtime(4f);

        gameOverScreen.SetActive(false);

        foreach (LocalPlayer player in players)
        {
            player.isGameOver = false;
        }

        PlayerOxygenManager.playerOxygenManager.stopOxygen = false;
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
    GameWithCat,
    GameWithoutCat,
    Win,
    GameOver
}
