using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popup_EnterName : Popup
{

    [SerializeField] private InputField nameInputField;
    [SerializeField] private Button submitButton;
    [SerializeField] private GameObject panelGameObject;

    public override void Setup()
    {
        submitButton.onClick.AddListener(OnSubmitClicked);

        if (PlayerPrefs.HasKey("PlayerUsername"))
        {
            panelGameObject.SetActive(false);
            string savedName = PlayerPrefs.GetString("PlayerUsername");
            _ = NakamaManager.Instance.SetPlayerUsernameAsync(savedName);
        }
        else
        {
            panelGameObject.SetActive(true);
        }
    }

    private async void OnSubmitClicked()
    {
        string enteredName = nameInputField.text.Trim();

        if (string.IsNullOrEmpty(enteredName) || enteredName.Length < 3)
        {
            Debug.LogWarning("3 charecter");
            return;
        }

        submitButton.interactable = false;

        bool success = await NakamaManager.Instance.SetPlayerUsernameAsync(enteredName);

        if (success)
        {
            panelGameObject.SetActive(false);
        }
        else
        {
            submitButton.interactable = true;
        }
    }
}
