
using System;
using UnityEngine;

public class FirstPuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject neil_ground_1;
    [SerializeField] private GameObject neil_ground_2;
    [SerializeField] private GameObject umpa_ground_1;
    [SerializeField] private GameObject umpa_ground_2;

    public void Update()
    {
        if (Detect_neil.neil_detected && detect_umpa.umpa_detected)
        {
            Destroy(neil_ground_1);
            Destroy(umpa_ground_1);

        }
    }
}