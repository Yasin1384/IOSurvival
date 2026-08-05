using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Popup_CheckInternet : Popup
{
    [SerializeField] private Button checkInternetButton;


    public override void Setup()
    {
        checkInternetButton.onClick.AddListener(async () =>
        {
            UiManager.Instance.ClosePopup(gameObject);
            await ClickButton();
        });
    }

    private async Task ClickButton()
    {
        bool isSucces = await NakamaManager.Instance.InitializeAndConnectAsync();
        if (isSucces)
        {
            Debug.Log("Connected! Loading Online Game/Lobby...");
            UiManager.Instance.OpenPanel<Panel_Main>().Setup();
        }
        else
        {
            Debug.Log("Connection Failed! Loading Offline Mode...");

            UiManager.Instance.OpenPopups<Popup_CheckInternet>().Setup();
        }
    }
}
