using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerDamage : MonoBehaviour
{
    public int playerIndex;


    private IDamageStratgy _damageStratgy;
    private int _damage;
    private PlayerType_SO playerTypes;
    private void Start()
    {
        var playerData = SelectedCardsHolder.SelectedPlayer;

        if (playerData == null)
        {
            Debug.Log("No player selected, using default.");

            playerData = PlayerManager.Instance != null
                ? PlayerManager.Instance.GetDefaultPlayer()
                : null;
        }

        _damage = playerData.Hp;


        SetDamage(_damageStratgy);
    }

    private void SetDamage(IDamageStratgy damageStratgy)
    {
        _damageStratgy = new NormalDamageStratgy();
    }

    public void TakeDamage(int baseDamage)
    {
        if (_damageStratgy == null)
        {
            return;
        }

        int finalDamage = _damageStratgy.Damage(baseDamage);
        _damage -= finalDamage;


        if (_damage <= 0)
        {
            //TODO : panel Game Over 
            GameManager.Instance.GameOver(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BulletEnemy"))
        {
            Debug.Log("AAYYYYYYYYYYY");
            TakeDamage(5);
            Debug.Log("AAYYYYYYYYYYY");

        }
    }
}
