using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesData", menuName = "Enemy System/Enemies Data")]

public class EnemyType_SO : ScriptableObject
{
   
    public GameObject EnemyPrefab;

    public int EnemyDamage = 0;

    public float Speed = 0;

    public int MaxHp = 0;
}
