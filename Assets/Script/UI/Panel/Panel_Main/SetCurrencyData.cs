using System.Xml.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class SetCurrencyData : MonoBehaviour
{
    [SerializeField] private Text _coinText;
    [SerializeField] private Text _xpText;
    [SerializeField] private Text _levelText;

    private int currentCoin;
    //----- LEVEL
    private int currentLevel;
    private int currentXp;
    private int xpToNextLevel;


    public void CurrencySetting()
    {
        CurrencyManager currencyManager = CurrencyManager.Instance;
        currentLevel = currencyManager.currentLevel;
        currentXp = currencyManager.currentXP;
        xpToNextLevel = currencyManager.xpToNextLevel;

        Debug.Log(currentCoin);
        Debug.Log(currentLevel);
        Debug.Log(currentXp);
        Debug.Log(xpToNextLevel);

        _levelText.text = currentLevel.ToString();
        _xpText.text = currentXp.ToString() + "/" + xpToNextLevel.ToString();
        CurrencyManager.Instance.OnCoinsChanged += UpdateUI;
        _coinText.text = CurrencyManager.Instance.coins.ToString();
    }

    void UpdateUI(int amount)
    {
        
        _coinText.text = amount.ToString();
    }
}
