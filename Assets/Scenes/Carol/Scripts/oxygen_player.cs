using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oxygen_player : MonoBehaviour
{
    // Start is called before the first frame update

    private float oxygenLevel = 200f;
    private float decreaseAmount = 25f;
    private float decreaseInterval = 5f;
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= decreaseInterval)
        {
            DecreaseOxygenLevel();
            timer = 0f;
        }

        //Debug.Log("Oxygen Level: " + oxygenLevel);
    }

    private void DecreaseOxygenLevel()
    {
        oxygenLevel -= decreaseAmount;
        //Debug.Log("Oxygen Level: " + oxygenLevel);

        if (oxygenLevel <= 0f)
        {
            // Perform actions when oxygen level reaches 0 or below
            //Debug.Log("Out of Oxygen!");
        }
    }

     void OnTriggerEnter(Collider other){
        if(gameObject.tag == "Player" && other.gameObject.tag == "oxygen"){
            Debug.Log("OXYGEEEEEEEEEEEEEEEEEEEEEEEEEN");
            IncreaseOxygenLevel();
            //Debug.Log("oxygen capacity: " + oxygenLevel);
        }
}

    private void IncreaseOxygenLevel(){

         if(oxygenLevel > 0)
        {
            oxygenLevel += 25;
        }

        if(oxygenLevel == 0)
        {
            //Debug.Log("oxygen capacity is 0");
        }
    }


}
