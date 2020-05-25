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
    }

    private void OnDisable() {
        PlayerMessageHandler.playerMessageEvent -= ShowMessage;
    }

    private void ShowMessage(MessageTypes messageType, string message) {
        displayField.text = message;

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
