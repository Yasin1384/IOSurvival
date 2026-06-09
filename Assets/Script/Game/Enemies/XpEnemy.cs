using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class XpEnemy : MonoBehaviour
{
    private void Start()
    {
        List<EnemyType_SO> enemyList;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);

            UiGamePlay.Instance.AddXpInGame();

        }
    }


    private void AddXp()
    {
        int xpAmount = 2;
    }


}
