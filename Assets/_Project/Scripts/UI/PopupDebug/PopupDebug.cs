using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupDebug : Popup
{
    public TMP_InputField SetLevel;
    public TMP_InputField SetCoin;
    public Toggle ToggleTesting;
    public Toggle ToggleIsOffInterAds;

    public void OnEnable()
    {
        ToggleTesting.isOn = UserData.IsTesting;
        ToggleIsOffInterAds.isOn = UserData.IsOffInterAds;
    }

    public void OnClickAccept()
    {
        if (SetLevel.text != null && SetLevel.text != "")
        {
            UserData.CurrentLevel = int.Parse(SetLevel.text);
            GameManager.Instance.PrepareLevel();
            GameManager.Instance.StartGame();
        }

        if (SetCoin.text != null && SetCoin.text != "")
        {
            UserData.CurrencyTotal = int.Parse(SetCoin.text);
        }

        SetCoin.text = string.Empty;
        SetLevel.text = string.Empty;
        gameObject.SetActive(false);
    }

    public void ChangeTestingState()
    {
        UserData.IsTesting = ToggleTesting.isOn;
    }

    public void OnClickFPSBtn()
    {
        GameManager.Instance.ChangeAFpsState();
    }

    public void OnClickUnlockAllSkin()
    {
        ConfigController.ItemConfig.UnlockAllSkins();
        Observer.PurchaseSucceed?.Invoke();
    }

    public void OnClickIsOffInterAds()
    {
        UserData.IsOffInterAds = ToggleIsOffInterAds.isOn;
    }
}