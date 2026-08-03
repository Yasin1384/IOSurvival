using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Panel_Loading : Panel
{

    public override async void Setup()
    {
        Debug.Log("Connecting to Nakama Server...");

        bool isConnected = await NakamaManager.Instance.InitializeAndConnectAsync();

        if (isConnected)
        {
            Debug.Log("Connected! Loading Online Game/Lobby...");
            UiManager.Instance.OpenPanel<Panel_Main>().Setup();
        }
        else
        {
            Debug.Log("Connection Failed! Loading Offline Mode...");

            //TODO : Popup Disconnect
        }
    }
}
