using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroll : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform moveSpot;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    public Enemy enemy;

    [SerializeField] private Animator animator;

    void Start()
    {
        waitTime = startWaitTime;

        minX = transform.position.x - 3f;
        maxX = transform.position.x + 3f;
        minY = transform.position.y - 3f;
        maxY = transform.position.y + 3f;

        //Init the random position of the moveSpot patroll
        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        Flip();
    }
    
    void Update()
    {
        //if seeTarget || follow target
        if (!enemy.seenTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, moveSpot.position) < 0.5f)
            {
                if (waitTime <= 0)
                {
                    moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                    Flip();
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }
    }

    void Flip()
    {
        if (moveSpot.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (moveSpot.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
