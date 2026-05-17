using UnityEngine;


[CreateAssetMenu(fileName = "PlayersData", menuName = "Player System/Players Data")]
public class PlayerType_SO : ScriptableObject
{
    public string Name;

    public GameObject PlayerPrefab;

    public float Speed;

    public int Hp;
}
