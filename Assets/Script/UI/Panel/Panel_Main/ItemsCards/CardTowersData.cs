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
        if (currentData == null || currentData.TowerBehaviorData == null)
        {
            Debug.LogError("Tower data is missing!");
            return;
        }

        if (SelectedCardsHolder.SelectedTowers.Count >= MAX_TOWERS)
        {
            Debug.Log("Max towers reached!");
            return;
        }
        SelectedCardsHolder.SelectedTowers.Add(currentData.TowerBehaviorData);
    }
}
