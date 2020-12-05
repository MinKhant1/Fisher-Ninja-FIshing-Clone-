using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;

    public GameObject currentScreen;

    public GameObject gameScreen;
    public GameObject endScreen;
    public GameObject mainScreen;
    public GameObject returnScreen;

    public Button lengthButton;
    public Button strengthButton;
    public Button offlineButton;

    public Text gameScreenMoney;
    public Text lengthCostText;
    public Text lengthValueText;
    public Text strengthCostText;
    public Text strengthValueText;
    public Text offlineCostText;
    public Text offlineValueText;
    public Text endScreenMoney;
    public Text returnScreenMoney;

    public int gameCount;
    private void Awake()
    {
        if (ScreenManager.instance)
        {
            Destroy(base.gameObject);
        }
        else
            ScreenManager.instance = this;
        currentScreen = mainScreen;
    }
    // Start is called before the first frame update
    void Start()
    {
        CheckIdles();
        UpdateTexts();

    }

    public void ChangeScreen(Screens screen)
    {
        currentScreen.SetActive(false);
        switch (screen)
        {
            case Screens.MAIN:
                currentScreen = mainScreen;
                UpdateTexts();
                CheckIdles();
                break;
            case Screens.GAME:
                currentScreen = gameScreen;
                gameCount++;

                break;
            case Screens.END:
                currentScreen = endScreen;

                break;
            case Screens.RETURN:
                break;

        }
        currentScreen.SetActive(true);

    }


    public void SetEndScreenMoney()
    {
        endScreenMoney.text = "$" + IdleManager.instance.totalGain;
    }
    public void SetReturnScreenMoney()
    {
        endScreenMoney.text = "$" + IdleManager.instance.totalGain + " gained while waiting";
    }
    private void UpdateTexts()
    {
        gameScreenMoney.text = "$" + IdleManager.instance.wallet;
        lengthCostText.text = "$" + IdleManager.instance.lengthCost;
        lengthValueText.text = -IdleManager.instance.length + "m";
        strengthCostText.text = "$" + IdleManager.instance.strengthCost;
        strengthValueText.text = IdleManager.instance.strength + " fishes.";
        offlineCostText.text ="$" + IdleManager.instance.offlineEarningsCost;
        offlineValueText.text = "$" + IdleManager.instance.offlineEarnings+"/min";
    }

    private void CheckIdles()
    {

        int lengthCost = IdleManager.instance.lengthCost;
        int strengthCost = IdleManager.instance.strengthCost;
        int offlineEarningsCost = IdleManager.instance.offlineEarningsCost;
        int wallet = IdleManager.instance.wallet;

        if (wallet < lengthCost)
            lengthButton.interactable = false;
        else
            lengthButton.interactable = true;
        if (wallet < strengthCost)
            strengthButton.interactable = false;
        else
            strengthButton.interactable = true;

        if (wallet < offlineEarningsCost)
            offlineButton.interactable = false;
        else
            offlineButton.interactable = true;
    }


}
