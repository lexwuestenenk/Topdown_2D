using UnityEngine;
using System.Collections;
 
public class mouseFollow : MonoBehaviour {
 
    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;
 
    // Update is called once per frame
    void Update () {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
    }
}