using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();

    public MoovingCharacter player;

    public bool enemyTargeted = false;

    private void Start()
    {
        StartCoroutine("FindTargetWithDelay", .2f);
    }

    IEnumerator FindTargetWithDelay(float delay)
    { 
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTarget();
        }
    }

    void FindVisibleTarget()
    {
        visibleTargets.Clear();
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        foreach(Collider2D target in targetsInViewRadius)
        {
            Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
            if (player.facingRight)
            {
                if (Vector3.Angle(transform.right, dirToTarget) < viewAngle / 2)
                {

                    float dstToTarget = Vector3.Distance(transform.position, target.transform.position);

                    RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, dirToTarget, dstToTarget);
                    if (hitInfo.collider.gameObject.tag == "enemy")
                    {
                        //Debug.Log("see Baka ");
                        Transform t = target.transform;
                        visibleTargets.Add(t);
                    }
                }
            }
            else
            {
                if (Vector3.Angle(transform.right * -1, dirToTarget) < viewAngle / 2)
                {

                    float dstToTarget = Vector3.Distance(transform.position, target.transform.position);

                    RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, dirToTarget, dstToTarget);
                    if (hitInfo.collider.gameObject.tag == "enemy")
                    {
                        Debug.Log("see Baka ");
                        Transform t = target.transform;
                        enemyTargeted = true;
                        visibleTargets.Add(t);
                    }
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        if (player.facingRight)
        {
            return new Vector3(Mathf.Sin((angleInDegrees + 90) * Mathf.Deg2Rad), Mathf.Cos((angleInDegrees + 90) * Mathf.Deg2Rad), 0);
        }
        else
            return new Vector3(Mathf.Sin((angleInDegrees - 90) * Mathf.Deg2Rad), Mathf.Cos((angleInDegrees - 90) * Mathf.Deg2Rad), 0);

    }
}
