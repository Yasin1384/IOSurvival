using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    public static UiManager Instance { get; private set; }
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

    public PlayerType_SO playerType_SO;

    [SerializeField] private Button button;
    [SerializeField] private string sceneToLoad;

    [SerializeField] private Text text;
    [SerializeField] private Text textXP;
    [SerializeField] private Text textEndXP;
    void Start()
    {
        text.text = CurrencyManager.Instance.coins.ToString();
        textXP.text = CurrencyManager.Instance.currentXP.ToString();
        textEndXP.text = CurrencyManager.Instance.xpToNextLevel.ToString();
        button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(sceneToLoad);
        });
    }
}
