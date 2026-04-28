using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private string sceneToLoad;

    void Start()
    {
        button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(sceneToLoad);
        });
    }
}
