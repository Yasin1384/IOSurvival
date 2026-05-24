using UnityEngine;


[CreateAssetMenu(fileName = "Support Solider Data", menuName = "Support Solider System/Support Solider Data")]
public class SoliderSupport_SO: ScriptableObject
{
    public string Name;

    public GameObject SoliderSupportPrefab;

    public float Speed;

    public int Hp;
}
