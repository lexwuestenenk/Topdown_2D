using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHit : MonoBehaviour
{

    public int playerDamage; 

    // Function gets called when player's weapon collider trigger hits a collider.
    // It will then compare tags and act according to those. 
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Breakable")) {
            other.GetComponent<Breakable>().Destroy();
        }

        else if(other.CompareTag("Enemy")) {
           other.GetComponent<Log>().TakeDamage(playerDamage);
        }
    }
}
