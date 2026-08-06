using UnityEngine;
using UnityEngine.UI;

public class UiGamePlay : MonoBehaviour
{
    public static UiGamePlay Instance { get; private set; }


    [SerializeField] private Text xpText;
    int xpIndex = 1;
    [SerializeField] private Text userNameText;
    [SerializeField] private Text opponentNameText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        
        }

        SetNames();
    }

    public void SetNames()
    {
        userNameText.text = NakamaManager.Instance.MyUsername;
        opponentNameText.text = NakamaManager.Instance.OpponentUsername;
    }

    public int AddXpInGame()
    {
        xpText.text = xpIndex.ToString();
        return xpIndex++;

    }
}
