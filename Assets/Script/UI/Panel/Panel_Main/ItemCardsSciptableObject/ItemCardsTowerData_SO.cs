using UnityEngine;

[CreateAssetMenu(fileName = "UI System", menuName = "UI System /ItemCardsTowerData_SO /ItemCardsData_SO")]
public class ItemCardsTowerData_SO : ScriptableObject
{
    public string NameItems;
    public Sprite Sprite;
    public string Price;

    public GameObject TowerGameObject;
    public TowerDataTypes_SO TowerBehaviorData;
}
