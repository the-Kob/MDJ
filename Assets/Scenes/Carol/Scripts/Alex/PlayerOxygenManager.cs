using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerOxygenManager : MonoBehaviour
{
    public static PlayerOxygenManager playerOxygenManager { get; set; }

    public List<Slider> oxygenSliders;
    public TMP_Text oxygenLevelsText;

    public float oxygenLevels;
    public float maxOxygenLevels = 1000f;
    
    public float oxygenDecreasePerTimeframe = 1f;
    public float decreaseTimeframe = 0.75f;
    public float timeElapsed = 0f;

    public bool stopOxygen = false;
    public bool resetOxygen = false;

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

        ResetOxygen();
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
        }

        if (stopOxygen) return;

        if (oxygenLevels <= 0)
        {
            Restart();
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

    public void Restart()
    {
        Time.timeScale = 0f;
        Debug.Log("Out of O2 :/");
    }
}
