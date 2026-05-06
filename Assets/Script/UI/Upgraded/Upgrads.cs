using UnityEngine;
using UnityEngine.UI;

public class Upgrads : MonoBehaviour
{
    [SerializeField] private Button hpButton;
    [SerializeField] private Button speedButton;
    [SerializeField] private Button gunButton;

    private void Start()
    {
        
        HpUpgrad();
    }

    private void HpUpgrad()
    {

        speedButton.onClick.RemoveAllListeners();
        speedButton.onClick.AddListener(() =>
        {
            var playerType = GameManager.Instance.PlayerType;

            float speed = playerType.Speed + 2;

            playerType.Speed = speed;
        });

        hpButton.onClick.RemoveAllListeners();
        hpButton.onClick.AddListener(() =>
        {
            var playerType = GameManager.Instance.PlayerType;

            int hp = playerType.Hp + 2;

            playerType.Hp = hp;
        });

        gunButton.onClick.RemoveAllListeners();
        gunButton.onClick.AddListener(() =>
        {
            var playerType = GameManager.Instance.PlayerType;

            float speedSpawnBullet = playerType.SpeedSpawnBullet + 2;
            float bulletSpeed = playerType.BulletSpeed + 2;

            playerType.SpeedSpawnBullet = speedSpawnBullet;
            playerType.BulletSpeed = bulletSpeed;
        });
    }
}
