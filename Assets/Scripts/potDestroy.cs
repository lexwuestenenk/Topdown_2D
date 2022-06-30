using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Function can be called from anywhere. This gets called when the an object is hit by the player's sword, 
    // and has a "Breakable" tag. This can be used for anything that should be breakable. 
    public void Destroy()
    {
        animator.SetBool("Destroy", true);
        StartCoroutine(breakCo());
    }

    IEnumerator breakCo()
    {
        yield return new WaitForSeconds(0.3f);
        this.gameObject.SetActive(false);
    }
}
