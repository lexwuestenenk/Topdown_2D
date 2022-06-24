using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHit : MonoBehaviour
{

    public int playerDamage; 

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
