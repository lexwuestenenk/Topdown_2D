using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Make variables that enemies can inherit. This can be reused for every single enemy in the game. 

    public int health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
}
