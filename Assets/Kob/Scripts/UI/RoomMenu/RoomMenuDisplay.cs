using System;
using System.Runtime.ConstrainedExecution;
using TMPro;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.UI;

public class RoomMenuDisplay : MonoBehaviour
{
    private static int JOIN_CODE_LENGTH = 6;

    [Header("References")]
    [SerializeField] private GameObject connectingPanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private TMP_InputField joinCodeInputField;
    [SerializeField] private Button joinButton;

    private async void Start()
    {
        joinCodeInputField.onValidateInput += delegate (string s, int i, char c)
        {
            if (s.Length >= JOIN_CODE_LENGTH) { return '\0'; }
            return char.ToUpper(c);
        };

        joinCodeInputField.onValueChanged .AddListener(ValidateInput);

        try
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log($"Player Id: {AuthenticationService.Instance.PlayerId}");
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return;
        }

        connectingPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    private void OnEnable()
    {
        ValidateInput(joinCodeInputField.text);
    }

    public void StartHost()
    {
        Debug.Log("Click");
        HostManager.Instance.StartHost();
    }

    public void StartClient()
    {
        ClientManager.Instance.StartClient(joinCodeInputField.text);
    }

    private void ValidateInput(string code)
    {
        if (code.Length < 6) { joinButton.interactable = false; }
        else { joinButton.interactable = true; }
    }
}
