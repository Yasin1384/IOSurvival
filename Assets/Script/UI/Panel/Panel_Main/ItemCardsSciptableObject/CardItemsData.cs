using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CardItemsData : MonoBehaviour
{
    [SerializeField] private Text lable;
    [SerializeField] private Image iconSprite;
    [SerializeField] private Text price;

    [SerializeField] private Button button;
    private ItemCardsTowerData_SO currentData;

    private void Awake()
    {
        button.onClick.AddListener(OnCardClicked);
    }
    public void Setup(ItemCardsTowerData_SO data)
    {
        currentData = data;

        lable.text = data.NameItems;
        iconSprite.sprite = data.Sprite;
        price.text = data.Price;

    }
    private void OnCardClicked()
    {
        Debug.Log("Selected: " + currentData.NameItems);

        SelectedTowerHolder.SelectedTower = currentData.TowerBehaviorData;
    }
}
