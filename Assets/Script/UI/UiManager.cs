using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private string sceneToLoad;

    [SerializeField] private Text text;
    [SerializeField] private Text textXP;
    [SerializeField] private Text textEndXP;
    void Start()
    {
        text.text = CurrencyManager.Instance.coins.ToString();

        textXP.text = XpPlayer.Instance.currentXP.ToString();
        textEndXP.text = XpPlayer.Instance.xpToNextLevel.ToString();
        button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(sceneToLoad);
        });


    }
}
