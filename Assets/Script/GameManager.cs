using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    public TimerGame timerGame;

    public List<LevelTypes_SO> LevelTypes;
    public List<EnemyType_SO> EnemyTypes;

    public Vector3 spawnPos;
    [SerializeField] private string sceneToLoad;

    int countWave;
    int countLevel;

    private void OnEnable()
    {

        if (timerGame != null)
        {
            timerGame.Finish += HandleFinishWave;
            Debug.Log(timerGame);
        }
    }

    private void OnDisable()
    {

        if (timerGame != null)
        {
            timerGame.Finish -= HandleFinishWave;
            Debug.Log(timerGame);

        }
    }

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
        CurrencyManager currencyManager = CurrencyManager.Instance;
        SceneManager.LoadScene(sceneToLoad);
        currencyManager.AddXP(50);
        currencyManager.AddCoin(50);    
    }


    private void HandleFinishWave()
    {
        Debug.Log(timerGame);
        foreach (var item in LevelTypes)
        {
            countLevel = item.Level;
            
            countWave = item.Waves - 1;

            if (countWave > 0)
            {
                Debug.Log(countWave);

                timerGame.RestartTimer();
            }
            else if (countWave == 0)
            {
                countLevel++;
                WinGame();
            }
        }
    }

}
