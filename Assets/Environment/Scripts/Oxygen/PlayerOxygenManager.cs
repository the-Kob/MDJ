using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerOxygenManager : MonoBehaviour
{
    public static PlayerOxygenManager playerOxygenManager { get; set; }


    /* Needs to use the GameManager either way, the subscribing aspect mentioned
     * When state changes, it needs to do diff things based on state
     * If GameWith/Wo Cat, then respawn
     * Store last o2 dawn used?
     * It's chill to edit Kob's
     */

    public List<Slider> oxygenSliders;
    public TMP_Text oxygenLevelsText;

    public float oxygenLevels;
    public float maxOxygenLevels = 1000f;

    public float oxygenDecreasePerTimeframe = 1f;
    public float decreaseTimeframe = 0.75f;
    public float timeElapsed = 0f;

    public bool stopOxygen = false;
    public bool resetOxygen = false;

    public float oxygenLevelsAtSave;
    public List<GameObject> players;
    public List<Vector3> playersPositionAtSave = new List<Vector3>(2);
    public DomeOxygenManager lastOxygenDome;

    // Start is called before the first frame update
    void Awake()
    {
        if (playerOxygenManager != null && playerOxygenManager != this)
        {
            Debug.Log("Destroying this");
            Destroy(this);
        }
        else
        {
            playerOxygenManager = this;
        }
    }

    void Start()
    {
        SavePlayersPosition();
        ResetOxygen();

        oxygenLevelsAtSave = maxOxygenLevels;
        Debug.Log("Game started.");
    }

    public float getMaxOxygenLevels()
    {
        return maxOxygenLevels;
    }

    // Update is called once per frame
    void Update()
    {
        if (resetOxygen)
        {
            ResetOxygen();
            return;
        }

        if (stopOxygen) return;

        if (oxygenLevels <= 0)
        {
            Debug.Log("O2 asked for gen reset");
            GameManager.Instance.UpdateGameState(GameState.GameOver);
            return;
        }

        UpdateOxygenLevels();

    }

    private void ResetOxygen()
    {
        resetOxygen = false;
        Time.timeScale = 1f;
        oxygenLevels = maxOxygenLevels;

        foreach (Slider oxygenSlider in oxygenSliders)
        {
            oxygenSlider.value = oxygenLevels / maxOxygenLevels;
        }
        timeElapsed = 0f;
    }

    private void UpdateOxygenLevels()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > decreaseTimeframe)
        {
            timeElapsed = 0f;
            float percent = oxygenDecreasePerTimeframe / maxOxygenLevels;
            DecreaseOxygenLevels(percent);
        }

        oxygenLevelsText.text = "" + oxygenLevels;

        foreach (Slider oxygenSlider in oxygenSliders)
        {
            oxygenSlider.value = oxygenLevels / maxOxygenLevels;
        }
    }

    public float DecreaseOxygenLevels(float percent)
    {
        float oldOxygenLevels = oxygenLevels;
        oxygenLevels = Mathf.Max(0f, oxygenLevels - percent * maxOxygenLevels);

        return oldOxygenLevels - oxygenLevels;
    }

    public float IncreaseOxygenLevels(float percent)
    {
        float oldOxygenLevels = oxygenLevels;
        oxygenLevels = Mathf.Min(maxOxygenLevels, oxygenLevels + percent * maxOxygenLevels);

        return oldOxygenLevels - oxygenLevels;
    }

    public void Save(DomeOxygenManager oxygenDome)
    {
        lastOxygenDome = oxygenDome;

        SavePlayersOxygen();
        SavePlayersPosition();
    }

    public void Reset()
    {
        ResetPlayersPosition();
        ResetPlayersOxygen();
        ResetOxygenDome();
    }

    public void SavePlayersPosition()
    {
        Debug.Log("saving");
        Debug.Log(lastOxygenDome.spawnPlayerPositions[0]);
        for (int i = 0; i < players.Count; i++)
        {
            playersPositionAtSave[i] = lastOxygenDome.spawnPlayerPositions[i];
        }
    }

    public void ResetPlayersPosition()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].transform.position = playersPositionAtSave[i];
        }
    }

    public void SavePlayersOxygen() { oxygenLevelsAtSave = oxygenLevels; }

    public void ResetPlayersOxygen() { oxygenLevels = oxygenLevelsAtSave; }

    public void ResetOxygenDome()
    {
        DomeOxygenManager domeOxygenManager = lastOxygenDome;
        domeOxygenManager.ResetDome();
    }
}