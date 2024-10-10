using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    [SerializeField] public Text moneyText;
    [SerializeField] public Text HealthText;

    public SaveManager saveManager;

    void Start()
    {
        moneyText.text = saveManager.Instance.state.CoinNumber.ToString();
        HealthText.text = saveManager.Instance.state.PlayerMaxHealth.ToString();
    }

    public void Exit()
    {
        saveManager.Instance.Save();       
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
