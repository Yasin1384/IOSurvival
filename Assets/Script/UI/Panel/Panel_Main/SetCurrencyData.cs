using System.Xml.Linq;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
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
        currentCoin = currencyManager.coins;
        currentLevel = currencyManager.currentLevel;
        currentXp = currencyManager.currentXP;
        xpToNextLevel = currencyManager.xpToNextLevel;

        Debug.Log(currentCoin);
        Debug.Log(currentLevel);
        Debug.Log(currentXp);
        Debug.Log(xpToNextLevel);

        _coinText.text = currentCoin.ToString();
        _levelText.text = currentLevel.ToString();
        _xpText.text = currentXp.ToString() + "/" + xpToNextLevel.ToString();


    }
}
