using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ServerMessageDisplayer : MonoBehaviour {

    [SerializeField] private float messageDurationInSeconds = 2f;
    [SerializeField] private string defaultText = "";
    [SerializeField] private TextMeshProUGUI displayField;
    [SerializeField] private AudioSource audioSource;

    // Start is called before the first frame update
    private void Awake() {
        PlayerMessageHandler.playerMessageEvent += ShowMessage;
        IsRenderingScript.isVisibleEvent += SeeingSeeker;
        PlayerDeath.playerDeathEvent += DeathEvent;
    }

    private void OnDisable() {
        PlayerMessageHandler.playerMessageEvent -= ShowMessage;
        IsRenderingScript.isVisibleEvent -= SeeingSeeker;
        PlayerDeath.playerDeathEvent -= DeathEvent;
    }

    private void DeathEvent(Player player) {
        if(player.IsMainPlayer) {
            audioSource.clip = PlayerAudioType.GetClip(PlayerAudioType.DEAD.Sounds[Random.Range(0, PlayerAudioType.DEAD.Sounds.Length)]);
            audioSource.Play();
        }
    }

    private void SeeingSeeker(bool isSeeing) {
        if(isSeeing) {
            //Give the player a hands up..
            if(!audioSource.isPlaying) {
                audioSource.clip = PlayerAudioType.GetClip(PlayerAudioType.HIDER_SEES_SEEKER.Sounds[Random.Range(0, PlayerAudioType.HIDER_SEES_SEEKER.Sounds.Length)]);
                audioSource.Play();
            }
        }
    }

    private void ShowMessage(MessageTypes messageType, string message) {
        displayField.text = message;
        StartCoroutine("StopShowingMessage");

        //Also play the appropriate audio :)
        PlayerAudioType type = PlayerAudioType.GetByMessageType(messageType);
        audioSource.clip = PlayerAudioType.GetClip(type.Sounds[Random.Range(0, type.Sounds.Length)]);
        audioSource.Play();
    }

    private IEnumerator StopShowingMessage() {
        yield return new WaitForSeconds(messageDurationInSeconds);

        displayField.text = defaultText;
        StopCoroutine("StopShowingMessage");
    }
}
