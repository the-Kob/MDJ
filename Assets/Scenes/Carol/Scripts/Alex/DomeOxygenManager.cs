using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomeOxygenManager : MonoBehaviour
{
    public List<Vector3> spawnPlayerPositions;
   
    public float maxOxygen;
    public float currentOxygen;

    private bool isSaved = false;
    private float oxygenAtSave;
    private bool activeAtSave;

    private bool neilDetected;
    private bool umpaDetected;

    public float oxygenPercentPerTimeframe = 0.01f;
    public float increaseTimeframe = 0.1f;

    public float timeElapsed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (maxOxygen == 0)
        {
            maxOxygen = PlayerOxygenManager.playerOxygenManager.maxOxygenLevels;
        }
        
        ResetDome();
    }

    // Update is called once per frame
    void Update()
    {
       if (currentOxygen <= 0f)
       {
            gameObject.SetActive(false);
            return;
       }

       CheckForPlayers();
    }

    private void CheckForPlayers()
    {
        if (neilDetected || umpaDetected)
        {
            Debug.Log("Player detected!");
            if (!isSaved)
                Save();

            timeElapsed += Time.deltaTime;
            if (timeElapsed > increaseTimeframe)
            {
                //Debug.Log("timeElapsed = " + timeElapsed + "  |  increaseTimeframe = " + increaseTimeframe);
                GiveOxygen(Mathf.FloorToInt(timeElapsed / increaseTimeframe));
                timeElapsed = 0f;
            }
        }
    }

    public void Save()
    {
        isSaved = true;
        activeAtSave = gameObject.activeInHierarchy; // ??
        oxygenAtSave = currentOxygen;
        PlayerOxygenManager.playerOxygenManager.Save(this);
    }

    public void ResetDome()
    {
        isSaved = false;

        gameObject.SetActive(true);
        activeAtSave = true;
        
        currentOxygen = maxOxygen;
        oxygenAtSave = maxOxygen;

        neilDetected = false;
        umpaDetected = false;
    }

    public void ResetDomeSave()
    {
        gameObject.SetActive(activeAtSave);
        currentOxygen = oxygenAtSave;
    }

    public void GiveOxygen(int nrTimesteps = 1)
    {
        //Debug.Log("Will give O2. #Timesteps = " + nrTimesteps);

        float oxygenPercentToGive = nrTimesteps * oxygenPercentPerTimeframe;

        //Debug.Log("Percent to give = " + oxygenPercentToGive);

        oxygenPercentToGive = oxygenPercentToGive * maxOxygen <= currentOxygen ? 
                              oxygenPercentToGive : currentOxygen / maxOxygen;

        //Debug.Log("Will give " + oxygenPercentToGive + "%");

        float oxygenGiven = PlayerOxygenManager.playerOxygenManager.IncreaseOxygenLevels(oxygenPercentToGive);

        //Debug.Log("Gave " + oxygenGiven + " total");

        currentOxygen = Mathf.Max(0, currentOxygen + oxygenGiven);
    }


    private void OnTriggerStay(Collider other)
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
            //Debug.Log("neil NOT detected");
        }
        if (other.gameObject.tag == "Umpa")
        {
            umpaDetected = false;
            //Debug.Log("umpa NOT detected");
        }
    }
    /*
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Neil")
        {
            neilDetected = true;
            //Debug.Log("neil detected - STAY");
        }
        if (other.gameObject.tag == "Umpa")
        {
            umpaDetected = true;
            //Debug.Log("umpa detected - STAY");
        }

    }*/
}
