using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomMenu : MonoBehaviour
{
    [SerializeField] public Text moneyText;

    public SaveManager saveManager;

    void Start()
    {
        moneyText.text = saveManager.Instance.state.CoinNumber.ToString();
    }

    public void IncreaseHealth()
    {
        if(saveManager.Instance.state.CoinNumber >= 10)
        {
            saveManager.Instance.state.CoinNumber -= 10;
            moneyText.text = saveManager.Instance.state.CoinNumber.ToString();
            saveManager.Instance.state.PlayerMaxHealth += 10;
        }
    }
}
