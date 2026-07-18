using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardTowersData : MonoBehaviour
{
    [SerializeField] private Text lable;
    [SerializeField] private Image iconSprite;
    [SerializeField] private Text price;

    [SerializeField] private Button button;

    public void Setup(ItemCardsTowerData_SO data)
    {
        lable.text = data.NameItems;
        iconSprite.sprite = data.Sprite;
        price.text = data.Price.ToString();



        button.onClick.AddListener(() =>
        {
            UiManager.Instance.OpenPopups<Popup_Upgrade>().Setup();
        });
    }

    

}
