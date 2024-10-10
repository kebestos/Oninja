using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoovingCharacter : MonoBehaviour
{

    [Header("Player")]
    [SerializeField] private GameObject characterGameObject;
    [SerializeField] private Rigidbody2D rb;

    public bool cliked = false;
    [SerializeField] private float speed = 1f;

    [SerializeField] private Animator animator;

    [SerializeField]
    private FireBall weapon;
    private float spawnTimeWeapon = 1f;

    private Vector2 clickposition;
    public bool facingRight = true;
    public Vector2 moveDirection;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if(clickposition.x == 0 && clickposition.y == 0)
            {
                cliked = true;
                animator.SetFloat("Speed", 1.0f);
                clickposition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }            
            MouseDragSart();             
        }
        else if (!Input.GetMouseButton(0) && cliked == true)
        {
            cliked = false;
            clickposition = new Vector2(0, 0);
            animator.SetFloat("Speed", 0.0f);
        }
    }

    private void MouseDragSart()
    {
        Vector2 currentMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        moveDirection = (currentMousePos - clickposition).normalized;

        if (characterGameObject != null)
        {               
            rb.MovePosition(rb.position +   moveDirection * speed * Time.deltaTime);
            
            if(currentMousePos.x > clickposition.x && !facingRight)
            {
                Flip();
            }
            else if(currentMousePos.x < clickposition.x && facingRight)
            {
                Flip();
            }
        }              
    }    
    
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = characterGameObject.transform.localScale;
        theScale.x *= -1;
        characterGameObject.transform.localScale = theScale;
    }
}
