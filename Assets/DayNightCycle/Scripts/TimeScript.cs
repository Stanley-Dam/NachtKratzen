using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScript : MonoBehaviour
{
    [SerializeField]
    private float time;
    [SerializeField]
    private int secondsPerSecond;

    void Update()
    {
        time += secondsPerSecond * Time.deltaTime;

        //if time exeeds one day the timer is reset
        //TODO: Sync server on timer reset.
        if (time >= 86400)
        {
            time -= 86400;
        }
    }

    //sends current time whenever requested
    public float GetTime()
    {
        return time;
    }
}
