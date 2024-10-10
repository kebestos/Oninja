using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{  

    [SerializeField] Player player;

    void Start()
    {
        player = (Player)Object.FindObjectOfType<Player>();
    }
    void Update()
    {
        if (player!= null)
        {
            incrementedCoin();
        }        
    }

    void incrementedCoin()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if(distance <= 1f)
        {
            player.saveManager.Instance.state.CoinNumber += 1;
            player.PlayerIncrementCoinsEarn();
            Destroy(gameObject);
        }
    }
}
