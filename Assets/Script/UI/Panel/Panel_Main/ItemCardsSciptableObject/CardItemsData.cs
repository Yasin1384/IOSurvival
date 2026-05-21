using UnityEngine;
using UnityEngine.UI;

public class CardItemsData : MonoBehaviour
{
    [SerializeField] private Text lable;
    [SerializeField] private Image iconSprite;
    [SerializeField] private Text price;


    public void Setup(string lable, Sprite sprite, string price)
    {
        this.lable.text = lable;
        iconSprite.sprite = sprite;
        this.price.text = price;
    }

}
