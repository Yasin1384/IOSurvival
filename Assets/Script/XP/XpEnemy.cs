using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class XpEnemy : MonoBehaviour
{
    public int killBonus;

    List<EnemyType_SO> enemyList;
    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);

            AddXp();
        }
    }


    private void AddXp()
    {
        foreach (var item in enemyList)
        {
            killBonus += item.KillBonus;
            Debug.Log(killBonus);
        }

    }
}
