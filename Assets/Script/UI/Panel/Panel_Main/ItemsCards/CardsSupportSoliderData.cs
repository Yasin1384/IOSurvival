using UnityEngine;
using UnityEngine.UI;

public class CardsSupportSoliderData : MonoBehaviour
{
    [SerializeField] private Text lable;
    [SerializeField] private Image iconSprite;
    [SerializeField] private Text price;

    [SerializeField] private Button button;
    private ItemCardsSupportSoliderData_SO currentData;

    private void Awake()
    {
    }
    public void Setup(ItemCardsSupportSoliderData_SO data)
    {
        currentData = data;

        lable.text = data.NameItems;
        iconSprite.sprite = data.Sprite;
        price.text = data.Price.ToString();

        if (CurrencyManager.Instance.coins <= data.Price)
        {
            button.enabled = false;
        }
        else
        {
            button.enabled = true;
        }
        button.onClick.AddListener(() =>
        {   
            OnCardClicked(currentData);
        });
    }
    private void OnCardClicked(ItemCardsSupportSoliderData_SO data)
    {
        SelectedCardsHolder.SelectedSupportSolider.Add(currentData.SupportSoliderBehaviorData); 
        CurrencyManager.Instance.SpendCoins(data.Price);

    }
}
