using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Transform _safeArea;

    private readonly List<GameObject> _panels = new List<GameObject>();
    private readonly List<GameObject> _popups = new List<GameObject>();

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

        OpenPanelMain();
    }

    private void OpenPanelMain()
    {
        OpenPanel<Panel_Main>().Setup();
    }


    #region ---- Panel ----
    public T OpenPanel<T>() where T : Panel
    {
        GameObject panel = Addressables.InstantiateAsync(typeof(T).Name, _safeArea).WaitForCompletion();

        _panels.Add(panel);

        return panel.GetComponent<T>();
    }


    public void ClosePanel(GameObject panel)
    {
        var panelToClose = _panels.Find(p => p == panel);
        if (panelToClose != null)
        {
            Addressables.ReleaseInstance(panelToClose);
            _panels.Remove(panelToClose);
        }
    }


    public void CloseAllPanel()
    {
        foreach (var panel in _panels)
        {
            Addressables.ReleaseInstance(panel);
        }
        _panels.Clear();

    }
    #endregion

    #region ---- Popup ----
    public T OpenPopups<T>() where T : Popup
    {
        GameObject popup = Addressables.InstantiateAsync(typeof(T).Name, _safeArea).WaitForCompletion();

        _popups.Add(popup);

        return popup.GetComponent<T>();
    }


    public void ClosePopup(GameObject popup)
    {
        var popupToClose = _popups.Find(p => p == popup);
        if (popupToClose != null)
        {
            Addressables.ReleaseInstance(popupToClose);
            _popups.Remove(popupToClose);
        }
    }


    public void CloseAllPopups()
    {
        foreach (var popup in _popups)
        {
            Addressables.ReleaseInstance(popup);
        }
        _popups.Clear();
    }
    #endregion

}
