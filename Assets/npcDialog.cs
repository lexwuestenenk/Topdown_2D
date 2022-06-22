using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcDialog : MonoBehaviour
{

    // Gets the audioSource the click needs to be played from.
    public AudioSource audioObject;
    public AudioClip interactClick;
    public AudioClip talkingSound;

    // Gets the dialogBox and the text that should be in it, for later use. 
    public GameObject dialogBox;
    public Text dialogText;
    public GameObject nextArrow;

    private string dialog;
    private float textDelay = 0.1f;
    private bool _pause = false;
    private bool _interactionActive = false;

    public List<string> characterDialog = new List<string>();
    
    // bool playerInRange to check if player is close enough to the sign to interact with it.
    public bool playerInRange;

    // Update is called once per frame.
    void Update()
    {        
        
        if(_pause) {
            nextArrow.SetActive(true);
        } else {
            nextArrow.SetActive(false);
        }

        if(Input.GetButtonDown("Interact") && _interactionActive) {
            _pause = !_pause;
        }

        // Dialog speedup when pressing Run button (shift)
        if(Input.GetButtonDown("Run")) {
            textDelay = textDelay / 4;
        } else if(Input.GetButtonUp("Run")) {
            textDelay = textDelay * 4;
        }

        // Get input from Interact button (F) and check if player is in range of the sign,
        // then enable and disable text bubble with specified text.
        if(Input.GetButtonDown("Interact") && playerInRange)
        {
            if(dialogBox.activeInHierarchy && !_interactionActive)
            {
                dialogBox.SetActive(false);
                audioObject.PlayOneShot(interactClick);
                _pause = false;
                StopCoroutine("PlayText");
                dialogText.text = "";
            }
            else if(!dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(true);
                audioObject.PlayOneShot(interactClick);
                StartCoroutine("PlayText");
            }
        }
    }

    // function gets called when player enters trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) {
            playerInRange = true;
        }
    }

    // function gets called when player exits trigger.
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player")) {
            playerInRange = false;

            if(dialogBox.activeInHierarchy) {
                dialogBox.SetActive(false);
                StopCoroutine("PlayText");
                audioObject.PlayOneShot(interactClick);
                dialogText.text = "";
            }
        }
    }

    IEnumerator PlayText()
    {
        foreach (var item in characterDialog) {
            Debug.Log(_pause);
            _interactionActive = true;

            while(_pause) {
                yield return null;
            }

            dialogText.text = "";
            dialog = item.ToString();

            foreach (char c in dialog) {
                dialogText.text += c;
                yield return new WaitForSeconds(textDelay);
                audioObject.PlayOneShot(talkingSound, 3f);
            }

            _pause = true;
        }

        Debug.Log("Foreach loop ended");
        _interactionActive = false;
    }
}
