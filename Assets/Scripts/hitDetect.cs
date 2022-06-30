using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// THIS SCRIPT IS CURRENTLY UNUSED! //

public class hitDetect : MonoBehaviour
{

    private Animator animator;
    public float HP;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Public function. This can be called from anywhere,
    // and will reduce the enemies HP by the player damage variable set in the playerHit script
    public void takeDamage(float playerDamage)
    {
        HP = HP - playerDamage;
        StartCoroutine(DamageCo());

        if(HP <= 0) {
            StartCoroutine(disableEnemy());
        }
    }

    // Changes animation to the damage animation when the enemy is hit.
    private IEnumerator DamageCo()
    {
        animator.SetBool("Damage", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Damage", false);
    }
    
    // Disables the enemy object when it's HP drops below 0.
    private IEnumerator disableEnemy()
    {
        animator.SetBool("Destroy", true);
        yield return new WaitForSeconds(2f);
        enemy.SetActive(false);
    }
}
