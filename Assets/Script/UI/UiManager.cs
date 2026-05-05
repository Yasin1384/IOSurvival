using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private string sceneToLoad;

    [SerializeField] private Text text;
    void Start()
    {
        text.text = CurrencyManager.Instance.coins.ToString();
        button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(sceneToLoad);
        });
    }
}
