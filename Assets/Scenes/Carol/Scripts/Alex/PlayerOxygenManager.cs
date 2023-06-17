using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerOxygenManager : MonoBehaviour
{
    public List<Slider> oxygenSliders;
    public TMP_Text oxygenLevelsText;

    public float oxygenLevels;
    public float maxOxygenLevels = 1000f;
    
    public float oxygenDecrease = 1f;
    public float decreaseTimer = 0.75f;
    public float timeElapsed = 0f;

    public bool stopOxygen = false;
    public bool resetOxygen = false;

    // Start is called before the first frame update
    void Start()
    {
        ResetOxygen();
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

        if (timeElapsed > decreaseTimer)
        {
            timeElapsed = 0f;
            float percent = oxygenDecrease / maxOxygenLevels;
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
