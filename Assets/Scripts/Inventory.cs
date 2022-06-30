using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// THIS SCRIPT IS CURRENTLY UNUSED! //

public class Inventory : ScriptableObject
{
    [SerializeField] List<GameObject> items = new List<GameObject>();

    public List<GameObject> GetItems { get => items; }

    public void AddItem(GameObject itemToAdd) { items.Add(itemToAdd); }
}
