using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    Vector3 MoveDirection;
    [SerializeField]
    MoovingCharacter player;

    [SerializeField] private FireBall fireball;

    private float fireballForce = 20f;
    private float spawnTimeWeapon = .25f;

    [SerializeField] private Transform firePoint;

    public FieldOfView fow;

    private void Start()
    {
        InvokeRepeating("SpawnWeapon", spawnTimeWeapon, spawnTimeWeapon);
    }
    
    void Update()
    {
        MoveDirection = player.moveDirection;
        if (MoveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(MoveDirection.y, MoveDirection.x) * Mathf.Rad2Deg - 90.0f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void SpawnWeapon()
    {
        if (player.cliked)
        {
            if (!fow.visibleTargets.Any())
            {
                var newWeapon = GameObject.Instantiate(fireball, firePoint.position, firePoint.rotation);
                newWeapon.SetDirection(MoveDirection);
                Rigidbody2D rb = newWeapon.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.up * fireballForce, ForceMode2D.Impulse);
            }
            else
            {
                if(fow.visibleTargets[0] != null && player.transform != null)
                {
                    Vector3 directionFireToNearestEnemy = (fow.visibleTargets[0].position - player.transform.position).normalized;
                    float distanceShorter = Vector3.Distance(player.transform.position, fow.visibleTargets[0].position);
                    foreach (Transform target in fow.visibleTargets)
                    {
                        float distanceTemp = Vector3.Distance(player.transform.position, target.position);
                        if (distanceTemp < distanceShorter)
                        {
                            distanceShorter = distanceTemp;
                            directionFireToNearestEnemy = (target.position - player.transform.position).normalized;
                        }
                    }
                    var newWeapon = GameObject.Instantiate(fireball, firePoint.position, firePoint.rotation);
                    newWeapon.SetDirection(directionFireToNearestEnemy);
                    Rigidbody2D rb = newWeapon.GetComponent<Rigidbody2D>();
                    rb.AddForce(directionFireToNearestEnemy * fireballForce, ForceMode2D.Impulse);
                }
                
            }
        }
    }
}
