using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour
{
    [SerializeField]  private int HealingPoint;
    private Player player;

    private void Start()
    {
        player = (Player) Object.FindObjectOfType<Player>();
    }
    
    void Update()
    {
        if (player != null)
        {
            incrementLife();
        }
    }

    private void incrementLife()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance <= 0.7f)
        {
            player.PlayerHealing(HealingPoint);
            Destroy(gameObject);
        }
    }
}
