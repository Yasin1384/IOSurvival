using UnityEngine;

[CreateAssetMenu(fileName = "UI System", menuName = "UI System /ItemCardsSoliderData_SO /ItemCardsData_SO")]
public class ItemCardsSoliderData_SO : ScriptableObject
{
    public string NameItems;
    public Sprite Sprite;
    public int Price;

    public PlayerType_SO soliderType;
}
