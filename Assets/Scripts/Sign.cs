using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{

    // Gets the audioSource the click needs to be played from.
    public AudioSource audioObject;

    // Gets the dialogBox and the text that should be in it, for later use. 
    public GameObject dialogBox;
    public GameObject interactPopup;
    public GameObject nextArrow;
    public Text dialogText;
    public string dialog;

    // bool playerInRange to check if player is close enough to the sign to interact with it.
    public bool playerInRange;

    // Update is called once per frame.
    void Update()
    {
        // Get input from Interact button (F) and check if player is in range of the sign,
        // then enable and disable text bubble with specified text .
        if(Input.GetButtonDown("Interact") && playerInRange)
        {
            if(dialogBox.activeInHierarchy) {
                dialogBox.SetActive(false);
                audioObject.Play();
                nextArrow.SetActive(false);
            } else {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
                audioObject.Play();
            }
        }
    }

    // function gets called when player enters trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) {
            interactPopup.SetActive(true);
            playerInRange = true;
        }
    }

    // function gets called when player exits trigger.
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player")) {
            playerInRange = false;
            interactPopup.SetActive(false);

            if(dialogBox.activeInHierarchy) {
            dialogBox.SetActive(false);
            audioObject.Play();
            nextArrow.SetActive(false);
            }
        }
    }
}
