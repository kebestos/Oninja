using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBall : MonoBehaviour
{
    private  Vector3 direction;

    [SerializeField] private float speed;

    [SerializeField] Weapon weapon;

    [SerializeField] GameObject hiteffect;

    public float distance;
    public LayerMask whatIsSolid;

    public int damage;
    public int enemyDamage;

    void Update()
    {
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "enemy")
        {
            Debug.Log("Enemy take damage");
            collision.collider.GetComponent<Enemy>().TakeDamage(enemyDamage);
        }
        else if(collision.collider.gameObject.tag == "Player")
        {
            Debug.Log("Player take damage");
            collision.collider.GetComponent<Player>().PlayerTakeDamage(enemyDamage);
        }
        else if(collision.collider.gameObject.tag == "Chest")
        {
            collision.collider.GetComponent<Chest>().TakeDamage(enemyDamage);
        }
        GameObject effect = Instantiate(hiteffect, transform.position, Quaternion.identity);
        Destroy(effect, .3f);
        Destroy(gameObject);
    }    
}
