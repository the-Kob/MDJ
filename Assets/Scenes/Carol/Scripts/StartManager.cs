using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{

    public GameObject startButton;
    void Start() {
        startButton.GetComponent<Button>().onClick.AddListener(StartButton);
    }
    public void StartButton()
    {
        SceneManager.LoadScene("Lobby");
    }
}
