using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Panel_Main : Panel
{
    [Header("--- Text & String ---")]
    public string SceneToLoadString;
    [SerializeField] private Text _coinText;
    [SerializeField] private Text _xpText;
    [SerializeField] private Text _levelText;

    [Header("--- Button ---")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button cardsButton;

    [Header("--- Panels ---")]
    [SerializeField] private List<GameObject> panelsGameObjectList;
    [SerializeField] private Panel_MilitaryCards militaryCards;

    private int currentCoin;
    //----- LEVEL
    private int currentLevel;
    private int currentXp;
    private int xpToNextLevel;





    public override void Setup()
    {

        CurrencySetting();

        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(StartButton);

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
        SceneManager.LoadScene(SceneToLoadString);
    }

    private void CurrencySetting()
    {
        CurrencyManager currencyManager = CurrencyManager.Instance;
        currentCoin = currencyManager.coins;

        currentLevel = currencyManager.currentLevel;
        currentXp = currencyManager.currentXP;
        xpToNextLevel = currencyManager.xpToNextLevel;

        _coinText.text = currentCoin.ToString();
        _levelText.text = currentLevel.ToString();
        _xpText.text = currentXp.ToString() + "/" + xpToNextLevel.ToString();


    }
}
