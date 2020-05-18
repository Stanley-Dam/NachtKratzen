using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour {

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float stepDelay = 0.5f;

    private bool canStep = true;

    public void Jump(PlayerAudioType type) {
        audioSource.clip = PlayerAudioType.GetClip(type.Sounds[Random.Range(0, type.Sounds.Length)]);
        audioSource.Play();

        StopCoroutine("DoStepCooldown");

        canStep = false;
        StartCoroutine("DoStepCooldown");
    }

    /// <summary>
    /// Play walking sound.
    /// </summary>
    /// <param name="type">type of walking, ex: sprinting or walking. </param>
    public void Walk(PlayerAudioType type) {
        if(canStep) {
            audioSource.clip = PlayerAudioType.GetClip(type.Sounds[Random.Range(0, type.Sounds.Length)]);
            audioSource.Play();

            canStep = false;
            StartCoroutine("DoStepCooldown");
        }
    }

    /// <summary>
    /// A cooldown so the footstep audio doesn't get spammed.
    /// </summary>
    private IEnumerator DoStepCooldown() {
        bool cooldownDone = false;

        while (cooldownDone == false) {
            yield return new WaitForSeconds(stepDelay);
            cooldownDone = true;
        }

        canStep = true;
        StopCoroutine("DoStepCooldown");
        yield return null;
    }

}
