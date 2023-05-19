using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject startButton;
    public GameObject quitButton;
    public GameObject blackScreen;
   // public GameObject imageOn;
    const float fadeSpeed = 0.5f;
    const int upFadeSpeed = 1;

    void Start() {
        StartCoroutine(FadeFromBlack());

    }

    public IEnumerator FadeFromBlack() {

        yield return new WaitForSeconds(1);
        Color color = blackScreen.GetComponent<Image>().color;
        float fadeAmount = color.a;
        //Debug.Log(fadeAmount);

            while (fadeAmount > 0) {
                Debug.Log(fadeAmount);
                fadeAmount -= fadeSpeed * Time.deltaTime;

                Color newColor = new Color(color.r, color.g, color.b, fadeAmount);
                blackScreen.GetComponent<Image>().color = newColor;
                yield return null;

                }   

        blackScreen.SetActive(false);
    
    }


        
    public void StartButton()
    {
        SceneManager.LoadScene("LobbyCarol");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
