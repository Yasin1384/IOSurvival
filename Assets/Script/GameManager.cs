using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }



    public CameraFollow cameraFollow;
    public GameObject playerPrefab;
    public Vector3 spawnPos;
    [SerializeField] private string sceneToLoad;

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
    void Start() 
    {
        Spawn();
    }

    private void Spawn()
    {
        GameObject player = Instantiate(playerPrefab, spawnPos, Quaternion.identity);

        cameraFollow.SetTarget(player.transform);
    }

    private void TimerGame()
    {

    }

    public void GameOver(GameObject gameObject)
    {
        Destroy(gameObject);
        SceneManager.LoadScene(sceneToLoad);
    }

    public void WinGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

}
