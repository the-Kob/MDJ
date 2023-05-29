using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oxygen_player : MonoBehaviour
{
    // Start is called before the first frame update

    public float initial_oxygen_level = 200f;
    public float oxygen_level;
    
    private float decreaseAmount = 25f;
    private float decreaseInterval = 5f;
    private float timer = 0f;

    public void Start()
    {
        oxygen_level = initial_oxygen_level;
    }

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
        oxygen_level -= decreaseAmount;
        Debug.Log("OXYGEN PLAYER: " + oxygen_level);

        if (oxygen_level <= 0f)
        {
            // Perform actions when oxygen level reaches 0 or below
            //Debug.Log("Out of Oxygen!");
        }
    }

     void OnTriggerEnter(Collider other){
        if(gameObject.tag == "Player" && other.gameObject.tag == "oxygen"){
            oxygen_level += 25;
            Debug.Log("OXYGEEEEEEEEEEEEEEEEEEEEEEEEEN");
           
            
            //Debug.Log("oxygen capacity: " + oxygenLevel);
        }
}


}
