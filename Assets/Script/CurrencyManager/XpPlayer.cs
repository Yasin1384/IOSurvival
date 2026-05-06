using UnityEngine;

public class XpPlayer : MonoBehaviour
{
    public static XpPlayer Instance;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        LoadProgress();
        DontDestroyOnLoad(gameObject);

    }
    public int currentLevel = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 100;

    public void AddXP(int amount)
    {
        currentXP += amount;

        if (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            LevelUp();

        }
        SaveProgress();
    }

    private void LevelUp()
    {
        currentLevel++;
        xpToNextLevel = CalculateNextXP();
    }

    private int CalculateNextXP()
    {
        return Mathf.RoundToInt(xpToNextLevel * 1.25f);
    }
    private void SaveProgress()
    {
        PlayerPrefs.SetInt("SavedLevel", currentLevel);
        PlayerPrefs.SetInt("SavedXP", currentXP);
        PlayerPrefs.SetInt("SavedXPToNextLevel", xpToNextLevel);
        PlayerPrefs.Save();
        Debug.Log("Progress Saved: Level=" + currentLevel + ", XP=" + currentXP + "/" + xpToNextLevel);
    }

    private void LoadProgress()
    {
        currentLevel = PlayerPrefs.GetInt("SavedLevel", 1);
        currentXP = PlayerPrefs.GetInt("SavedXP", 0);
        xpToNextLevel = PlayerPrefs.GetInt("SavedXPToNextLevel", 100);


        if (currentLevel == 1 && currentXP == 0 && xpToNextLevel == 100)
        {
            if (!PlayerPrefs.HasKey("SavedLevel"))
            {
                currentLevel = 1;
                currentXP = 0;
                xpToNextLevel = 100;
            }
        }

        Debug.Log("Progress Loaded: Level=" + currentLevel + ", XP=" + currentXP + "/" + xpToNextLevel);
    }
}
