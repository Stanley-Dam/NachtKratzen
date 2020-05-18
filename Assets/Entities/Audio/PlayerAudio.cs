using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour {

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float stepDelay = 1f;

    private float stepCooldown = 0;

    public void MakeNoise(PlayerAudioType type) {
        audioSource.clip = PlayerAudioType.GetClip(type.Sounds[Random.Range(0, type.Sounds.Length)]);
        audioSource.Play();
    }

    /// <summary>
    /// Play walking sound.
    /// </summary>
    /// <param name="type">type of walking, ex: sprinting or walking. </param>
    public void Walk(PlayerAudioType type) {
        if(!audioSource.isPlaying && stepCooldown <= 0) {
            audioSource.clip = PlayerAudioType.GetClip(type.Sounds[Random.Range(0, type.Sounds.Length)]);
            audioSource.Play();

            stepCooldown = stepDelay;
            StartCoroutine(DoStepCooldown());
        }
    }

    /// <summary>
    /// A cooldown so the footstep audio doesn't get spammed.
    /// </summary>
    private IEnumerator DoStepCooldown() {
        while(stepCooldown > 0) {
            stepCooldown -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        StopCoroutine(DoStepCooldown());
        yield return null;
    }

}
