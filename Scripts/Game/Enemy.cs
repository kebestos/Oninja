using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float distance;
    public Transform target;

    private Vector2 targetDirection;
    private float timeBtwShot;
    public float StartTimeBtwShot;

    public bool seenTarget = false;
    [SerializeField] private FireBall fireball;
    [SerializeField] private Transform firePoint;
    private float fireballForce = 20f;

    public Transform enemyGfx;

    [SerializeField] private Animator animator;

    private Text txtInfos;

    [SerializeField] private Stat healthStatEnemy;

    private Canvas healthCanvas;
    public float disappearTimeHealthBar;
    private float StartDisappearHealth;

    [SerializeField] private Coin coin;
    [SerializeField] private HealingItem healingItem;

    public int Health;

    private void Start()
    {
        StartDisappearHealth = disappearTimeHealthBar;
        //init the enemy healthS
        healthStatEnemy.Initialize(Health);

        Physics2D.queriesStartInColliders = false;
        timeBtwShot = StartTimeBtwShot;
        txtInfos = GameObject.Find("TxtInfos").GetComponent<Text>();

        healthCanvas = transform.GetComponentInChildren<Canvas>();
        healthCanvas.enabled = false;
    }

    private void Update()
    {
        if(target != null)
        {
            targetDirection = (target.position - transform.position).normalized;

            if (target.position.x < enemyGfx.position.x && seenTarget)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (target.position.x > enemyGfx.position.x && seenTarget)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }

            Shoot();
        }

        if (healthCanvas.enabled)
        {
            disappearTimeHealthBar -= Time.deltaTime;
            if (disappearTimeHealthBar <= 0)
                healthCanvas.enabled = false;
        }
        else
            disappearTimeHealthBar = StartDisappearHealth;  

        

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, targetDirection, distance);
        if (hitInfo.collider != null)
        {
            if(hitInfo.collider.gameObject.tag == "Player")
            {
                //Debug.DrawLine(transform.position, hitInfo.point, Color.green);
                seenTarget = true;
            }
            else
            {
                //Debug.DrawLine(transform.position, hitInfo.point, Color.red);
                seenTarget = false;
            }      
        }
    }

    public void TakeDamage(int damage)
    {
        if (!healthCanvas.isActiveAndEnabled)
        {
            healthCanvas.enabled = true;
        }
        healthStatEnemy.CurrentVal -= damage;
        
        if (healthStatEnemy.CurrentVal <= 0)
        {
            animator.SetBool("death", true);
            healthCanvas.enabled = false;            
            var HealingItem = GameObject.Instantiate(healingItem, this.transform.position, healingItem.transform.rotation);
            var newCoin = GameObject.Instantiate(coin, new Vector3(this.transform.position.x +1, this.transform.position.y, this.transform.position.z), this.transform.rotation);
            Destroy(gameObject);

        }
    }

    public void Shoot()
    {
        if (seenTarget == true)
        {
            if (timeBtwShot <= 0)
            {
                var newWeapon = GameObject.Instantiate(fireball, firePoint.position, firePoint.rotation);
                newWeapon.SetDirection(targetDirection);
                Rigidbody2D rb = newWeapon.GetComponent<Rigidbody2D>();
                rb.AddForce(targetDirection * fireballForce, ForceMode2D.Impulse);
                timeBtwShot = StartTimeBtwShot;
            }
            else
            {
                timeBtwShot -= Time.deltaTime;
            }

        }
    }
}
