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
        var playerType = GameManager.Instance.PlayerType;

        speedButton.onClick.RemoveAllListeners();
        speedButton.onClick.AddListener(() =>
        {
            float speed = playerType.Speed + 2;

            playerType.Speed = speed;
        });

        hpButton.onClick.RemoveAllListeners();
        hpButton.onClick.AddListener(() =>
        {
            int hp = playerType.Hp + 2;

            playerType.Hp = hp;
        });

        gunButton.onClick.RemoveAllListeners();
        gunButton.onClick.AddListener(() =>
        {
            float speedSpawnBullet = playerType.SpeedSpawnBullet + 2;
            float bulletSpeed = playerType.BulletSpeed + 2;

            playerType.SpeedSpawnBullet = speedSpawnBullet;
            playerType.BulletSpeed = bulletSpeed;
        });
    }
}
