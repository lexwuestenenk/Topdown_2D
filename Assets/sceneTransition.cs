using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;

    public GameObject interactPopup;
    private bool inTrigger = false;

    void Update()
    {
        if(Input.GetButtonDown("Interact") && inTrigger) {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger) {
            interactPopup.SetActive(true);
            inTrigger = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            interactPopup.SetActive(false);
            inTrigger = false;
        }
    }
}
