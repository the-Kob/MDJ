using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class oxygen_player : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider slider;
    public float oxygen_level;    
    private float decreaseAmount = 25f;
    private float decreaseInterval = 5f;
    private float timer = 0f;
    public float sum = 0f;

    public void Start()
    {
        oxygen_level = 500;
        slider.value = oxygen_level;
        slider.maxValue = oxygen_level;
        slider.value = oxygen_level;
    }

   


    private void Update()
    {
        slider.value = oxygen_level;
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

    private void increaseOxygen(){
        oxygen_level += 25f;
        
        sum += 25f;
    }
     void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "oxygen" && gameObject.tag == "Player"){
             increaseOxygen();
             if (sum >= 200f){
                 Destroy(other.gameObject);
             }
             
         
        }
 }
}



