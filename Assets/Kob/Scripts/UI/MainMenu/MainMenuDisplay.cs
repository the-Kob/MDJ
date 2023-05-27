using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image foreground;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject optionsPanel;

    [Header("Settings")]
    [SerializeField] private float fadeSpeed = 0.5f; 
    [SerializeField] private float fadeTime = 1f;
    [SerializeField] private string roomMenuSceneName = "RoomMenu";

    bool showingOptions = false;

    void Awake()
    {
        StartCoroutine(FadeFromBlack());

        menuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public IEnumerator FadeFromBlack()
    {
        yield return new WaitForSeconds(fadeTime);

        Color color = foreground.color;
        float fade = color.a;

        while(fade > 0)
        {
            fade -= fadeSpeed * Time.deltaTime;

            foreground.color = new Color(color.r, color.g, color.b, fade);

            yield return null;
        }

        foreground.gameObject.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(roomMenuSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Options()
    {
        menuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        menuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }
}
