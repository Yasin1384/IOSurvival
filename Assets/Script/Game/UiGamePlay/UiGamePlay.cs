using UnityEngine;
using UnityEngine.UI;

public class UiGamePlay : MonoBehaviour
{
    public static UiGamePlay Instance { get; private set; }


    [SerializeField] private Text xpText;
    int xpIndex = 1;


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

    public int AddXpInGame()
    {
        xpText.text = xpIndex.ToString();
        return xpIndex++;

    }
}
