using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField]
    private Button
        createGameButton,
        joinGameButton;

    [SerializeField] 
    private TMP_InputField joinGameInputField;

    [SerializeField] 
    private TMP_InputField playerNameInputField;
    
    [SerializeField] 
    private LobbyCreateUI lobbyCreateUI;
    
    [SerializeField] 
    private Transform lobbyContainer;
    
    [SerializeField] 
    private Transform lobbyTemplate;

    private void Awake()
    {
        createGameButton.onClick.AddListener(() =>
        {
            lobbyCreateUI.Show();
        });

        joinGameButton.onClick.AddListener(() =>
        {
            GameLobby.Instance.JoinWithCode(joinGameInputField.text);
        });

        lobbyTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        playerNameInputField.text = GameMultiplayer.Instance.GetPlayerName();
        playerNameInputField.onValueChanged.AddListener((string newText) => {
            GameMultiplayer.Instance.SetPlayerName(newText);
        });

        GameLobby.Instance.OnLobbyListChanged += KitchenGameLobby_OnLobbyListChanged;
        UpdateLobbyList(new List<Lobby>());
    }

    private void KitchenGameLobby_OnLobbyListChanged(object sender, GameLobby.OnLobbyListChangedEventArgs e)
    {
        UpdateLobbyList(e.lobbyList);
    }

    private void UpdateLobbyList(List<Lobby> lobbyList)
    {
        foreach (Transform child in lobbyContainer)
        {
            if (child == lobbyTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (Lobby lobby in lobbyList)
        {
            Transform lobbyTransform = Instantiate(lobbyTemplate, lobbyContainer);
            lobbyTransform.gameObject.SetActive(true);
            lobbyTransform.GetComponent<LobbyListSingleUI>().SetLobby(lobby);
        }
    }

    private void OnDestroy()
    {
        GameLobby.Instance.OnLobbyListChanged -= KitchenGameLobby_OnLobbyListChanged;
    }
}
