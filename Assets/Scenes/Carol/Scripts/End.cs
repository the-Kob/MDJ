
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    [SerializeField] private GameObject component1;
    [SerializeField] private GameObject component2;
    [SerializeField] private GameObject component3;
    [SerializeField] private GameObject component4;
    [SerializeField] private GameObject component5;
    [SerializeField] private GameObject component6;
    [SerializeField] private GameObject component7;

    private void OnTriggerEnter(Collider other)
    {
        if (component1 == null && component2 == null && component3 == null && component4 == null && component5 == null && component6 == null && component7 == null)
        {
            if (other.gameObject.tag == "Neil" || other.gameObject.tag == "Umpa")
            {
                Debug.Log("End of game");
                // load scene with index + 1
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
