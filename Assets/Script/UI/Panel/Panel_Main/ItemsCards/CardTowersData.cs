using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CardTowersData : MonoBehaviour
{
    [SerializeField] private Text lable;
    [SerializeField] private Image iconSprite;
    [SerializeField] private Text price;

    [SerializeField] private Button button;
    private ItemCardsTowerData_SO currentData;


    private const int MAX_TOWERS = 5;

    private void Awake()
    {

    }
    public void Setup(ItemCardsTowerData_SO data)
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

    private void OnCardClicked(ItemCardsTowerData_SO data)
    {
        currentData = data;

        if (currentData == null || currentData.TowerBehaviorData == null)
        {
            return;
        }

        if (SelectedCardsHolder.SelectedTowers.Count >= MAX_TOWERS)
        {
            return;
        }

        SelectedCardsHolder.SelectedTowers.Add(currentData.TowerBehaviorData);
        CurrencyManager.Instance.SpendCoins(data.Price);
    }
}
