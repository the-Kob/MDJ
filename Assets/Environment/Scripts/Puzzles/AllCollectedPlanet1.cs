
using UnityEngine;
using UnityEngine.UI;

public class AllCollectedPlanet1 : MonoBehaviour
{
    [SerializeField] private GameObject component1;
    [SerializeField] private GameObject component2;
    [SerializeField] private GameObject component3;
    [SerializeField] private GameObject component4;
    [SerializeField] private GameObject component5;
    
    [SerializeField] private Image UIImage;
    
    //activate image "ALLone" if all components are collected
    private void FixedUpdate()
    {
        if (!UIImage)
        {
            return;
        }

        if (UIImage.enabled == true)
        {
            Invoke("DisableImage", 4f);
            UIImage.enabled = false;
        }
        
        if (component1 == null && component2 == null && component3 == null && component4 == null && component5 == null)
        {
            UIImage.enabled = true;
            
        }
    }
    
    private void DisableImage()
    {
        UIImage.enabled = false;
    }
  
    
}
