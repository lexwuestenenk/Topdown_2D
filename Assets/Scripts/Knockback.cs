using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float thrust;

    // Function gets called when player's sword hits a collider with the Enemy tag.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {
            Rigidbody2D enemy = collision.GetComponent<Rigidbody2D>();
            if (enemy != null)
            {
                StartCoroutine(KnockCoroutine(enemy));
            }
        }
    }

    // When enemy is hit, this function will add force in the opposite direction of where the hit is coming from,
    // creating a knockback effect
    private IEnumerator KnockCoroutine(Rigidbody2D enemy)
    {
        Vector2 forceDirection = enemy.transform.position - transform.position;
        Vector2 force = forceDirection.normalized * thrust;

        enemy.velocity = force;
        yield return new WaitForSeconds(.3f);
        enemy.velocity = new Vector2();
    }
}
