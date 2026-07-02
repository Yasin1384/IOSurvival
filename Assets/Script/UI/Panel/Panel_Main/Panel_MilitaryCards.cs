using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_MilitaryCards : MonoBehaviour
{
    [Header("--- Header Buttons ---")]
    [SerializeField] private Button agressiveButton;
    [SerializeField] private Button defeansiveButton;

    [Header("--- Aggressive ---")]
    [SerializeField] private Button soliderButton;
    [SerializeField] private Button supportSoliderButton;
    [SerializeField] private Button tankButton;
    [SerializeField] private Button robootButton;

    [Header("--- Defeansive ---")]
    [SerializeField] private Button towerButton;
    [SerializeField] private Button troopButton;
    [SerializeField] private Button machingGunButton;
    [SerializeField] private Button luncherButton;

    [Header("--- Sprites ---")]
    [SerializeField] private List<Sprite> headerButtonSpriteList;
    [SerializeField] private List<Sprite> spriteList;

    [Header("--- GameObject ---")]
    [SerializeField] private List<GameObject> buttonGameObject;

    [Header("--- ListItems ---")]
    [SerializeField] private CardsData cardsData;
    [SerializeField] private CardTowersData towerCardsData;
    [SerializeField] private CardsSoliderData soliderCardsData;
    [SerializeField] private CardsSupportSoliderData supportSoliderCardsData;
    [SerializeField] private RectTransform parent;

    public void Setup()
    {
        SetupModeButton(TabMilitaryButtonType.Aggressive);
        SetupModeAgressiveButtons(TabAggressiveButtonType.Solider);

        agressiveButton.onClick.RemoveAllListeners();
        agressiveButton.onClick.AddListener(() =>
        {
            SetupModeButton(TabMilitaryButtonType.Aggressive);
            SetupModeAgressiveButtons(TabAggressiveButtonType.Solider);
        });

        defeansiveButton.onClick.RemoveAllListeners();
        defeansiveButton.onClick.AddListener(() =>
        {
            SetupModeButton(TabMilitaryButtonType.Defeansive);
            SetupModeDefeansiveButtons(TabDefeansiveButtonType.Tower);
        });

        // ----------------------------------------------------------------

        soliderButton.onClick.RemoveAllListeners();
        soliderButton.onClick.AddListener(() =>
        {
            SetupModeAgressiveButtons(TabAggressiveButtonType.Solider);
        });

        supportSoliderButton.onClick.RemoveAllListeners();
        supportSoliderButton.onClick.AddListener(() =>
        {
            SetupModeAgressiveButtons(TabAggressiveButtonType.SupportSolider);
        });

        tankButton.onClick.RemoveAllListeners();
        tankButton.onClick.AddListener(() =>
        {
            SetupModeAgressiveButtons(TabAggressiveButtonType.Tank);
        });

        robootButton.onClick.RemoveAllListeners();
        robootButton.onClick.AddListener(() =>
        {
            SetupModeAgressiveButtons(TabAggressiveButtonType.Roboot);
        });

        // ----------------------------------------------------------------

        towerButton.onClick.RemoveAllListeners();
        towerButton.onClick.AddListener(() =>
        {
            SetupModeDefeansiveButtons(TabDefeansiveButtonType.Tower);
        });

        troopButton.onClick.RemoveAllListeners();
        troopButton.onClick.AddListener(() =>
        {
            SetupModeDefeansiveButtons(TabDefeansiveButtonType.Troops);
        });

        machingGunButton.onClick.RemoveAllListeners();
        machingGunButton.onClick.AddListener(() =>
        {
            SetupModeDefeansiveButtons(TabDefeansiveButtonType.MachingGun);
        });

        luncherButton.onClick.RemoveAllListeners();
        luncherButton.onClick.AddListener(() =>
        {
            SetupModeDefeansiveButtons(TabDefeansiveButtonType.Luncher);
        });

    }

    private void SetupModeButton(TabMilitaryButtonType tabMilitaryButtonType)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }

        switch (tabMilitaryButtonType)
        {
            case TabMilitaryButtonType.None:
                {
                    break;
                }
            case TabMilitaryButtonType.Aggressive:
                {
                    agressiveButton.GetComponent<Image>().sprite = headerButtonSpriteList[0];
                    defeansiveButton.GetComponent<Image>().sprite = headerButtonSpriteList[1];
                    buttonGameObject[0].SetActive(true);
                    buttonGameObject[1].SetActive(false);
                    break;
                }
            case TabMilitaryButtonType.Defeansive:
                {
                    agressiveButton.GetComponent<Image>().sprite = headerButtonSpriteList[1];
                    defeansiveButton.GetComponent<Image>().sprite = headerButtonSpriteList[0];
                    buttonGameObject[1].SetActive(true);
                    buttonGameObject[0].SetActive(false);
                    break;
                }
        }
    }

    private void SetupModeAgressiveButtons(TabAggressiveButtonType tabAggressiveButtonType)
    {
        var cardData = cardsData;


        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
        switch (tabAggressiveButtonType)
        {
            case TabAggressiveButtonType.None:
                {
                    break;
                }
            case TabAggressiveButtonType.Solider:
                {

                    soliderButton.GetComponent<Image>().sprite = spriteList[0];
                    supportSoliderButton.GetComponent<Image>().sprite = spriteList[1];
                    tankButton.GetComponent<Image>().sprite = spriteList[1];
                    robootButton.GetComponent<Image>().sprite = spriteList[1];

                    foreach (var item in cardData.itemCardsSoliderDataList)
                    {
                        Instantiate(soliderCardsData, parent).Setup(item);
                    }
                    break;
                }
            case TabAggressiveButtonType.SupportSolider:
                {

                    soliderButton.GetComponent<Image>().sprite = spriteList[1];
                    supportSoliderButton.GetComponent<Image>().sprite = spriteList[0];
                    tankButton.GetComponent<Image>().sprite = spriteList[1];
                    robootButton.GetComponent<Image>().sprite = spriteList[1];

                    foreach (var item in cardData.itemCardsSupportSoliderDataList)
                    {
                        Instantiate(supportSoliderCardsData, parent).Setup(item);
                    }
                    break;
                }
            case TabAggressiveButtonType.Tank:
                {
                    soliderButton.GetComponent<Image>().sprite = spriteList[1];
                    supportSoliderButton.GetComponent<Image>().sprite = spriteList[1];
                    tankButton.GetComponent<Image>().sprite = spriteList[0];
                    robootButton.GetComponent<Image>().sprite = spriteList[1];
                    break;
                }
            case TabAggressiveButtonType.Roboot:
                {
                    soliderButton.GetComponent<Image>().sprite = spriteList[1];
                    supportSoliderButton.GetComponent<Image>().sprite = spriteList[1];
                    tankButton.GetComponent<Image>().sprite = spriteList[1];
                    robootButton.GetComponent<Image>().sprite = spriteList[0];
                    break;
                }
        }
    }

    private void SetupModeDefeansiveButtons(TabDefeansiveButtonType tabDefeansiveButtonType)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
        var cardData = cardsData.itemCardsTowerDataList;
        switch (tabDefeansiveButtonType)
        {
            case TabDefeansiveButtonType.None:
                {
                    break;
                }
            case TabDefeansiveButtonType.Tower:
                {

                    towerButton.GetComponent<Image>().sprite = spriteList[0];
                    troopButton.GetComponent<Image>().sprite = spriteList[1];
                    machingGunButton.GetComponent<Image>().sprite = spriteList[1];
                    luncherButton.GetComponent<Image>().sprite = spriteList[1];

                    foreach (var item in cardData)
                    {
                        Instantiate(towerCardsData, parent).Setup(item);
                    }
                    break;
                }
            case TabDefeansiveButtonType.Troops:
                {
                    towerButton.GetComponent<Image>().sprite = spriteList[1];
                    troopButton.GetComponent<Image>().sprite = spriteList[0];
                    machingGunButton.GetComponent<Image>().sprite = spriteList[1];
                    luncherButton.GetComponent<Image>().sprite = spriteList[1];
                    break;
                }
            case TabDefeansiveButtonType.MachingGun:
                {
                    towerButton.GetComponent<Image>().sprite = spriteList[1];
                    troopButton.GetComponent<Image>().sprite = spriteList[1];
                    machingGunButton.GetComponent<Image>().sprite = spriteList[0];
                    luncherButton.GetComponent<Image>().sprite = spriteList[1];
                    break;
                }
            case TabDefeansiveButtonType.Luncher:
                {

                    towerButton.GetComponent<Image>().sprite = spriteList[1];
                    troopButton.GetComponent<Image>().sprite = spriteList[1];
                    machingGunButton.GetComponent<Image>().sprite = spriteList[1];
                    luncherButton.GetComponent<Image>().sprite = spriteList[0];
                    break;
                }
        }
    }


}
