using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomeOxygenManager : MonoBehaviour
{
    public GameObject oxygenDome;

    public float maxOxygen;
    public float currentOxygen;

    private bool isSaved = false;
    private float oxygenAtSave;
    private bool activeAtSave;

    private bool neilDetected;
    private bool umpaDetected;

    public float oxygenPercentPerHell = 0.01f;
    public float increaseHell = 0.2f;

    public float timeElapsed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        maxOxygen = PlayerOxygenManager.playerOxygenManager.maxOxygenLevels;
        ResetDome();
    }

    // Update is called once per frame
    void Update()
    {
       if (currentOxygen <= 0f)
       {
            oxygenDome.SetActive(false);
            return;
       }

       CheckForPlayers();
    }

    private void CheckForPlayers()
    {
        if (neilDetected || umpaDetected)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > increaseHell)
            {
                Debug.Log("timeElapsed = " + timeElapsed + "  |  increaseHell = " + increaseHell);
                GiveOxygen(Mathf.FloorToInt(timeElapsed / increaseHell));
                timeElapsed = 0f;
            }
        }
    }

    public void Save()
    {
        isSaved = true;
        activeAtSave = oxygenDome.activeInHierarchy; // ??
        oxygenAtSave = currentOxygen;
    }

    public void ResetDome()
    {
        isSaved = false;

        oxygenDome.SetActive(true);
        activeAtSave = true;
        
        currentOxygen = maxOxygen;
        oxygenAtSave = maxOxygen;

        neilDetected = false;
        umpaDetected = false;
    }

    public void ResetDomeSave()
    {
        oxygenDome.SetActive(activeAtSave);
        currentOxygen = oxygenAtSave;
    }

    public void GiveOxygen(int nrTimesteps = 1)
    {
        Debug.Log("Will give O2. #Timesteps = " + nrTimesteps);

        float oxygenPercentToGive = nrTimesteps * oxygenPercentPerHell;

        Debug.Log("Percent to give = " + oxygenPercentToGive);

        oxygenPercentToGive = oxygenPercentToGive * maxOxygen <= currentOxygen ? 
                              oxygenPercentToGive : currentOxygen / maxOxygen;

        Debug.Log("Will give " + oxygenPercentToGive + "%");

        float oxygenGiven = PlayerOxygenManager.playerOxygenManager.IncreaseOxygenLevels(oxygenPercentToGive);

        Debug.Log("Gave " + oxygenGiven + " total");

        currentOxygen = Mathf.Max(0, currentOxygen + oxygenGiven);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Neil")
        {
            neilDetected = true;
            Debug.Log("neil detected");
        }
        if (other.gameObject.tag == "Umpa")
        {
            umpaDetected = true;
            Debug.Log("umpa detected");
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Neil")
        {
            neilDetected = false;
            Debug.Log("neil NOT detected");
        }
        if (other.gameObject.tag == "Umpa")
        {
            umpaDetected = false;
            Debug.Log("umpa NOT detected");
        }
    }
    /*
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Neil")
        {
            neilDetected = true;
            Debug.Log("neil detected - STAY");
        }
        if (other.gameObject.tag == "Umpa")
        {
            umpaDetected = true;
            Debug.Log("umpa detected - STAY");
        }

    }*/
}
