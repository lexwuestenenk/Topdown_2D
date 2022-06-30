using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{

    // Gets camera GameObject so it can change to the right camera depending on what room the player is in
    public GameObject virtualCamera;

    // Bools to check if area needs new title card or music
    public bool needText;
    public bool needAudio;

    // Gets music and audioSource it needs to be played from
    public AudioClip nextMusic;
    public AudioSource audioObject;

    // Time background music should take to fade out/in
    private float fadeTime = 1f;

    // Variables for location popup
    public string placeName;
    public GameObject text;
    public Text placeText;

    // Function gets called when player enters trigger,
    // Enables camera of room player entered.
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

    // Function gets called when player exits trigger,
    // Disables virtualcamera of room player left
    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger) {
            virtualCamera.SetActive(false);
        }
    }

    // Audiofadeout class, called when needsAudio is true.
    public static class AudioFadeOut {

        public static IEnumerator musicFadeOut (AudioSource audioObject, float fadeTime, AudioClip nextMusic)
            {
                float startVolume = audioObject.volume;

                // Audio fade out
                while(audioObject.volume > 0) {
                    audioObject.volume -= startVolume * Time.deltaTime / fadeTime;

                    yield return null;
                }

                audioObject.Stop();
                audioObject.clip = nextMusic;
                audioObject.Play();
                
                // Audio fade in
                while(audioObject.volume < startVolume) {
                    audioObject.volume += startVolume * Time.deltaTime / fadeTime;

                    yield return null;
                }

                audioObject.volume = 0.1f;
            }
    }

    // Display placeText on screen for 4 seconds.
    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}
