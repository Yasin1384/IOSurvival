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
        button.onClick.AddListener(OnCardClicked);
    }
    public void Setup(ItemCardsSupportSoliderData_SO data)
    {
        currentData = data;

        lable.text = data.NameItems;
        iconSprite.sprite = data.Sprite;
        price.text = data.Price;

    }
    private void OnCardClicked()
    {
        Debug.Log("Selected: " + currentData.NameItems);

        SelectedCardsHolder.SelectedSupportSolider = currentData.SupportSoliderBehaviorData;
    }
}
