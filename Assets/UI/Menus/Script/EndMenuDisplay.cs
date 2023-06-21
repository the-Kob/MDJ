using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenuDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image foreground;

    [Header("Settings")]
    [SerializeField] private float fadeSpeed = 0.5f;
    [SerializeField] private float fadeTime = 1f;


    void Awake()
    {

        StartCoroutine(FadeFromBlack());
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

    public void LoadMainMenu()
    {

        Debug.Log("yes");
        SceneManager.LoadScene("MainMenu");
    }
}
