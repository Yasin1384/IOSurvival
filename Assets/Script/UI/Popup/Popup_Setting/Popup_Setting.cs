using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class Popup_Setting : Popup
{
    [SerializeField] private Button closeButton;
    public override void Setup()
    {
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(CloseButton);
    }

    private void CloseButton()
    {
        UiManager.Instance.ClosePopup(this.gameObject);
    }
}
