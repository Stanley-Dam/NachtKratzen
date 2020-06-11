using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStatsDisplayer : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI playersLeftDisplay;
    [SerializeField] private TextMeshProUGUI timeLeftDisplay;

    private int time = 0;

    private void Awake() {
        TimeLeftSync.timeSyncEvent += SyncTime;
        HiderCountSync.hiderCountSyncEvent += SyncHiderCount;
    }

    private void OnDisable() {
        TimeLeftSync.timeSyncEvent -= SyncTime;
        HiderCountSync.hiderCountSyncEvent -= SyncHiderCount;
    }

    private void SyncHiderCount(int hidersLeft) {
        playersLeftDisplay.text = hidersLeft.ToString();
    }

    private void SyncTime(int time) {
        this.time = time;
        StopCoroutine("CountDown");
        StartCoroutine("CountDown");
    }

    private IEnumerator CountDown() {
        while(this.time > 0) {
            this.time--;
            timeLeftDisplay.text = TimeInSecondsToTimeString(this.time);
            yield return new WaitForSeconds(1f);
        }

        StopCoroutine("CountDown");
        yield return null;
    }

    private string TimeInSecondsToTimeString(int seconds) {
        int minutes = Mathf.FloorToInt(seconds / 60);
        seconds -= minutes * 60;

        string minutesText = "" + minutes;
        string secondsText = "" + seconds;

        if (minutes < 10)
            minutesText = "0" + minutesText;
        if (seconds < 10)
            secondsText = "0" + secondsText;

        return minutesText + ":" + secondsText;
    }
}
