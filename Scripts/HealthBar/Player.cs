using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Stat health;

    [SerializeField]
    GameObject panel;

    public int EarnCoins = 0;
    [SerializeField] public Text moneyText;
    [SerializeField] public Text GlobalGoldInfosText;

    public SaveManager saveManager;

    void Awake()
    {
        health.Initialize(saveManager.Instance.state.PlayerMaxHealth);
    }

    private void Start()
    {
        moneyText = GameObject.Find("CoinInfos").GetComponent<Text>();
        FindObjectOfType<AudioManager>().Play("inGame");
    }
    
    void Update()
    {
        
    }
    public void PlayerTakeDamage(int damage)
    {
        health.CurrentVal -= damage;
        if (health.CurrentVal == 0)
        {
            panel.SetActive(true);
            GlobalGoldInfosText.text = saveManager.Instance.state.CoinNumber.ToString();
        }
    }

    public void PlayerHealing(int healPoint)
    {
        health.CurrentVal += healPoint;
    }

    public void PlayerIncrementCoinsEarn()
    {
        EarnCoins += 1;
        moneyText.text = EarnCoins.ToString();
    }

    public void Revive()
    {
        if(saveManager.Instance.state.CoinNumber >= 10)
        {
            health.CurrentVal = saveManager.Instance.state.PlayerMaxHealth;
            panel.SetActive(false);
            saveManager.Instance.state.CoinNumber -= 10;
        }       
    }
}
