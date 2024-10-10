using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Animator animator;

    [SerializeField]
    private float life;

    [SerializeField] private Coin coin;
    [SerializeField] private HealingItem healingItem;


    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        life -= damage;

        if (life == -27)
        {
            animator.SetBool("Alive", false);
            var HealingItem = GameObject.Instantiate(healingItem, new Vector3(this.transform.position.x + 1, this.transform.position.y - 2, this.transform.position.z), healingItem.transform.rotation);
            var newCoin = GameObject.Instantiate(coin, new Vector3(this.transform.position.x, this.transform.position.y - 3, this.transform.position.z), this.transform.rotation);
            var newCoin2 = GameObject.Instantiate(coin, new Vector3(this.transform.position.x -1, this.transform.position.y - 2, this.transform.position.z), this.transform.rotation);
        }
    }
}
