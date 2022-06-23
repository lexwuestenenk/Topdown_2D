using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void takeDamage(float playerDamage)
    {
        HP = HP - playerDamage;
        StartCoroutine(DamageCo());

        if(HP <= 0) {
            StartCoroutine(disableEnemy());
        }
    }

    private IEnumerator DamageCo()
    {
        animator.SetBool("Damage", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Damage", false);
    }
    
    private IEnumerator disableEnemy()
    {
        animator.SetBool("Destroy", true);
        yield return new WaitForSeconds(2f);
        enemy.SetActive(false);
    }
}
