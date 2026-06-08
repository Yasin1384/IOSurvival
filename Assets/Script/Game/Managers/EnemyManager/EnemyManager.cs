using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    public float Speed;
    public int CurrentHp;
    public int KillBonus;

    public GameObject xpPrefab;
    public int xpAmount = 1;
    public Transform xpPrefabTrans;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void DropCoin(Vector3 dropPosition)
    {
        for (int i = 0; i < xpAmount; i++)
        {
            Instantiate(xpPrefab, dropPosition, Quaternion.identity);
        }
    }
}
