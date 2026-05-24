using UnityEngine;

[CreateAssetMenu(fileName = "UI System", menuName = "UI System /ItemCardsSupportSoliderData_SO /ItemCardsData_SO")]
public class ItemCardsSupportSoliderData_SO : ScriptableObject
{
    public string NameItems;
    public Sprite Sprite;
    public string Price;

    public SoliderSupport_SO SupportSoliderBehaviorData;
}
