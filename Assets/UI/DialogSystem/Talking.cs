using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Talking : MonoBehaviour
{
   //if neil or umpa collide with this object the GameObject "DialogueSystem" will be activated
   //this will cause the dialogue to start
   //this is the same for the other triggers, but with different dialogues
   public GameObject dialogBox;
   void OnTriggerEnter(Collider other)
   {
       if (other.gameObject.tag == "Neil" || other.gameObject.tag == "Umpa")
       {
           Debug.Log("Triggered");
           dialogBox.SetActive(true);
       }
   }
   
   void OnTriggerExit(Collider other)
   {
       if (other.gameObject.tag == "Neil" || other.gameObject.tag == "Umpa")
       {

           
           dialogBox.SetActive(false);
           
         
       }
   }

  
   
  /* void OnTriggerStay(Collider other)
   {
       if (other.gameObject.tag == "Neil" || other.gameObject.tag == "Umpa")
       {
           Debug.Log("Triggered");
           dialogBox.SetActive(true);
       }
   }*/
   
}

