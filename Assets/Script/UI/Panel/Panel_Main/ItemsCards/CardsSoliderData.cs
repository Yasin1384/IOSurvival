using UnityEngine;
using UnityEngine.UI;

public class CardsSoliderData : MonoBehaviour
{
    [SerializeField] private Text lable;
    [SerializeField] private Image iconSprite;
    [SerializeField] private Text price;

    [SerializeField] private Button button;
    private ItemCardsSoliderData_SO currentData;

    private void Awake()
    {
        button.onClick.AddListener(OnCardClicked);
    }
    public void Setup(ItemCardsSoliderData_SO data)
    {
        currentData = data;

        lable.text = data.NameItems;
        iconSprite.sprite = data.Sprite;
        price.text = data.Price;

    }
    private void OnCardClicked()
    {
        Debug.Log("Selected: " + currentData.NameItems);

        SelectedCardsHolder.SelectedPlayer = currentData.soliderType;
    }
}
