using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates a value that is used when a player enters a building.
// This value is used to spawn the player back into the right location later.

[CreateAssetMenu]
public class VectorValue : ScriptableObject
{
    public Vector2 initialValue;
}
