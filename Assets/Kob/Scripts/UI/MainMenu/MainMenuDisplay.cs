using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image foreground;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject optionsPanel;
    //[SerializeField] private GameObject bindingsPanel;
    [SerializeField] private GameObject spaceshipImage;
    [SerializeField] private GameObject smokeEffect;


    [Header("Settings")]
    [SerializeField] private float fadeSpeed = 0.5f; 
    [SerializeField] private float fadeTime = 1f;
    [SerializeField] private string nextSceneName = "RoomMenu";


    public GameObject firstOptionsButton;
    public GameObject firstMenuButton;


    void Awake()
    {

        StartCoroutine(FadeFromBlack());

        Menu();
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

    public IEnumerator WaitForSmoke()
    {
        yield return new WaitForSeconds(fadeTime);
        smokeEffect.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        spaceshipImage.SetActive(true);
        StartCoroutine(WaitForSmoke());

        menuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        //bindingsPanel.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstMenuButton);
    }

    public void Options()
    {

        spaceshipImage.SetActive(false);
        smokeEffect.SetActive(false);

        menuPanel.SetActive(false);
        optionsPanel.SetActive(true);
        //bindingsPanel.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstOptionsButton);

    }

    public void Binds()
    {
        spaceshipImage.SetActive(false);
        smokeEffect.SetActive(false);

        menuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        //bindingsPanel.SetActive(true);
    }
}
