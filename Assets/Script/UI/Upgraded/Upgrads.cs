using UnityEngine;
using UnityEngine.UI;

public class Upgrads : MonoBehaviour
{

    [SerializeField] private Button hpButton;
    [SerializeField] private Button speedButton;
    [SerializeField] private Button gunButton;

    private void Start()
    {
        //var playerType = GameManager.Instance.PlayerTypes;
        //var gunType = GameManager.Instance.GunTypes;

        //foreach (var item in gunType)
        //{
            
        //}
        //HpUpgrad(playerType, gunType);
    }

    private void HpUpgrad(PlayerType_SO playerType, GunTypes_SO gunTypes_SO)
    {

        speedButton.onClick.RemoveAllListeners();
        speedButton.onClick.AddListener(() =>
        {

            float speed = playerType.Speed + 2;

            playerType.Speed = speed;

            Debug.Log(playerType.Speed);
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
            float speedSpawnBullet = gunTypes_SO.SpeedSpawnBullet + 2;
            float bulletSpeed = gunTypes_SO.BulletSpeed + 2;

            gunTypes_SO.SpeedSpawnBullet = speedSpawnBullet;
            gunTypes_SO.BulletSpeed = bulletSpeed;
        });
    }
}
