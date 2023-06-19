
using UnityEngine;
using UnityEngine.UI;

public class AllCollectedPlanet1 : MonoBehaviour
{
    [SerializeField] private GameObject component1;
    [SerializeField] private GameObject component2;
    [SerializeField] private GameObject component3;
    [SerializeField] private GameObject component4;
    [SerializeField] private GameObject component5;
    [SerializeField] private GameObject component6;
    [SerializeField] private GameObject component7;
    
    [SerializeField] private Image UIImage;
    
    private bool WasActivated = false;
    
    //activate image "ALLone" if all components are collected
    private void Update()
    {
        if (!UIImage)
        {
            return;
        }
        
        
        if (!WasActivated && component1 == null && component2 == null && component3 == null && component4 == null && component5 == null && component6 == null && component7 == null)
        {
            UIImage.enabled = true;
            Invoke("DisableImage", 5f);
            WasActivated = true;
            
        }
    }
    
    private void DisableImage()
    {
        UIImage.enabled = false;
    }
  
    
}
