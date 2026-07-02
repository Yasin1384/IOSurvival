using UnityEngine;
using UnityEngine.UI;

public class CardsSoliderData : MonoBehaviour
{
    [SerializeField] private Text lable;
    [SerializeField] private Image iconSprite;
    [SerializeField] private Text price;

    [SerializeField] private Button button;
    private ItemCardsSoliderData_SO currentData;


    public void Setup(ItemCardsSoliderData_SO data)
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
    private void OnCardClicked(ItemCardsSoliderData_SO data)
    {
        SelectedCardsHolder.SelectedPlayer = currentData.soliderType;
        CurrencyManager.Instance.SpendCoins(data.Price);
        PlayerPrefs.SetString("SelectedPlayer", data.soliderType.Name);
        PlayerPrefs.Save();
    }
}
