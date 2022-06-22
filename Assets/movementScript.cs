using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk, 
    attack,
    interact
}

public class movementScript : MonoBehaviour
{
    public PlayerState currentState;
    private Rigidbody2D body;
    private Animator animator;
    private Vector3 change;
    private Vector2 DashForce;


    // Player movement variables
    private float currentSpeed = 4f;
    private float walkSpeed = 4f;
    private float runSpeed = 6f;

    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        currentState = PlayerState.walk;
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;

        change.x = Input.GetAxisRaw("Horizontal");
        change.y  = Input.GetAxisRaw("Vertical");

        if(Input.GetButtonDown("Attack") && currentState != PlayerState.attack) {
            StartCoroutine(AttackCo());
        } else if(currentState == PlayerState.walk) {
            UpdateAnimationAndMove();
        }
        // run button input (left shift)
        if(Input.GetButtonDown("Run")) {
            currentSpeed = runSpeed;
        } else if(Input.GetButtonUp("Run")) {
            currentSpeed = walkSpeed;
        }
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("Attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(0.33f);
        currentState = PlayerState.walk;
    }

    void UpdateAnimationAndMove()
    {
        if(change != Vector3.zero) {
            animator.SetFloat("MoveX", change.x);
            animator.SetFloat("MoveY", change.y);
            animator.SetBool("Moving", true);
        } else {
            animator.SetBool("Moving", false);
        }
    }

    void FixedUpdate()
    {
        body.MovePosition(
            transform.position + change * currentSpeed * Time.deltaTime
        );
    }
}
