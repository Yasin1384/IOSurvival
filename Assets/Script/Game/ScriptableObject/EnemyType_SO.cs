using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesData", menuName = "Enemy System/Enemies Data")]

public class EnemyType_SO : ScriptableObject
{
    public string Name;
   
    public GameObject EnemyPrefab;

    public float Speed;

    public int Hp;

    public int KillBonus;
}
