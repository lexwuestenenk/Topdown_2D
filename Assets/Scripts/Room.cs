using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{

    // gets gameobject so it can change to the right camera depending on what room the player is in
    public GameObject virtualCamera;

    // bools to check if area needs new title card or music
    public bool needText;
    public bool needAudio;

    // gets music and audioSource it needs to be played from
    public AudioClip nextMusic;
    public AudioSource audioObject;

    // time background music should take to fade out/in
    private float fadeTime = 1f;

    // variables
    public string placeName;
    public GameObject text;
    public Text placeText;

    // function gets called when player enters trigger,
    // enables camera of room player entered
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger) {
            virtualCamera.SetActive(true);
            
            if(needText) {
                StartCoroutine(placeNameCo());
            }

            if(needAudio && nextMusic != audioObject.clip) {
                StartCoroutine(AudioFadeOut.musicFadeOut(audioObject, fadeTime, nextMusic));
            }
        }
    }

    // function gets called when player exits trigger,
    // disables virtualcamera of room player left
    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger) {
            virtualCamera.SetActive(false);
        }
    }

    // audiofadeout class, called when music needs to switch 
    public static class AudioFadeOut {

        public static IEnumerator musicFadeOut (AudioSource audioObject, float fadeTime, AudioClip nextMusic)
            {
                float startVolume = audioObject.volume;

                // fade out
                while(audioObject.volume > 0) {
                    audioObject.volume -= startVolume * Time.deltaTime / fadeTime;

                    yield return null;
                }

                audioObject.Stop();
                audioObject.clip = nextMusic;
                audioObject.Play();
                
                // fade in
                while(audioObject.volume < startVolume) {
                    audioObject.volume += startVolume * Time.deltaTime / fadeTime;

                    yield return null;
                }

                audioObject.volume = 0.1f;
            }
    }

    // displays placeText on screen for 4 seconds
    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}
