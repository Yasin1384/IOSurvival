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

    private int currentCoin;

    //----- LEVEL
    private int currentLevel;
    
    private int currentXp;
    private int xpToNextLevel;


    [Header("--- Button ---")]
    public Button startButton;


    public override void Setup()
    {
        CurrencySetting();


        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(StartButton);
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
