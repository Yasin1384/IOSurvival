using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Panel_Main : Panel
{
    [Header("--- Text & String ---")]
    public string SceneToLoadString;
    [SerializeField] private Text userName;


    [Header("--- Button ---")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button cardsButton;
    [SerializeField] private Button settingButton;

    [Header("--- Panels ---")]
    [SerializeField] private List<GameObject> panelsGameObjectList;
    [SerializeField] private Panel_MilitaryCards militaryCards;

    [Header("--- Currency ---")]
    [SerializeField] private SetCurrencyData data;





    public override void Setup()
    {
        string savedName = PlayerPrefs.GetString("PlayerUsername");        
        Debug.Log(savedName);
        userName.text = savedName;

        data.CurrencySetting();

        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(StartButton);

        settingButton.onClick.RemoveAllListeners();
        settingButton.onClick.AddListener(OpenSetting);


        homeButton.onClick.RemoveAllListeners();
        homeButton.onClick.AddListener(() =>
        {
            SetupButton(TabButtonType.Home);
        });

        shopButton.onClick.RemoveAllListeners();
        shopButton.onClick.AddListener(() =>
        {
            SetupButton(TabButtonType.Shop);
        });

        cardsButton.onClick.RemoveAllListeners();
        cardsButton.onClick.AddListener(() =>
        {
            SetupButton(TabButtonType.Cards); 
        });

        militaryCards.Setup();
    }




    private void SetupButton(TabButtonType tabButtonType)
    {
        switch (tabButtonType)
        {
            case TabButtonType.Home:
                {
                    panelsGameObjectList[0].SetActive(true);
                    panelsGameObjectList[1].SetActive(false);
                    panelsGameObjectList[2].SetActive(false);
                    break;
                }
            case TabButtonType.Shop:
                {
                    panelsGameObjectList[0].SetActive(false);
                    panelsGameObjectList[1].SetActive(true);
                    panelsGameObjectList[2].SetActive(false);
                    break;
                }
            case TabButtonType.Cards:
                {
                    panelsGameObjectList[0].SetActive(false);
                    panelsGameObjectList[1].SetActive(false);
                    panelsGameObjectList[2].SetActive(true);
                    break;
                }
        }
        
    }


    private void StartButton()
    {
        NakamaManager.Instance.FindMatch(SceneToLoadString);
    }



    private void OpenSetting()
    {
        UiManager.Instance.OpenPopups<Popup_Setting>().Setup();
    }
}
