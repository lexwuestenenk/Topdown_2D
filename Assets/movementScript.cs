using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player state handler. Used to check when player is in certain states, 
// And if the player can perform certain actions in those states.
public enum PlayerState
{
    walk, 
    attack,
    interact
}

public class movementScript : MonoBehaviour
{
    // PlayerState and animator controller. 
    public PlayerState currentState;
    private Animator animator;

    // Variables and components used for player movement.
    private Rigidbody2D body;
    private Vector3 change;
    private float currentSpeed = 4f;
    private float walkSpeed = 4f;
    private float runSpeed = 6f;

    void Start()
    {
        // Get all the components used for movement and animation.
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

        // Start the attack coroutine if the player's current state is attack.
        // A coroutine is a function that runs side by side with the other functions. 
        if(Input.GetButtonDown("Attack") && currentState != PlayerState.attack) {
            StartCoroutine(AttackCo());
        } else if(currentState == PlayerState.walk) {
            UpdateAnimationAndMove();
        }
        // Change the player's speed between walking and running when player presses the run button (Left Shift). . 
        if(Input.GetButtonDown("Run")) {
            currentSpeed = runSpeed;
        } else if(Input.GetButtonUp("Run")) {
            currentSpeed = walkSpeed;
        }
    }

    // Animation handler for player's attack. Hitboxes are handled by the animator in the unity editor. 
    private IEnumerator AttackCo()
    {
        animator.SetBool("Attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(0.33f);
        currentState = PlayerState.walk;
    }

    // Animation handler for player movement. 
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

    // Movement for player.
    void FixedUpdate()
    {
        body.MovePosition(
            transform.position + change * currentSpeed * Time.deltaTime
        );
    }
}