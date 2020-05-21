using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScript : MonoBehaviour {

    public float time;
    public int secondsPerSecond;

    public bool doingCycle = false;

    private void Awake() {
        SyncDayNightCycle.dayNightSyncEvent += SyncCycle;
    }

    private void OnDisable() {
        SyncDayNightCycle.dayNightSyncEvent -= SyncCycle;
    }

    private void SyncCycle(float time, float secondsPerSecond) {
        this.time = time;
        this.secondsPerSecond = (int) secondsPerSecond;
    }

    private void Update() {
        if (!doingCycle)
            return;

        time += secondsPerSecond * Time.deltaTime;

        //if time exeeds one day the timer is reset
        //TODO: Sync server on timer reset.
        if (time >= 86400) {
            time -= 86400;
            //You can Sync up time here when it is set to zero
        }
    }

    //sends current time whenever requested
    public float GetTime() {
        return time;
    }
}
