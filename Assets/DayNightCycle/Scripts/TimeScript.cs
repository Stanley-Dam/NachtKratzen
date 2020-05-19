using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScript : MonoBehaviour
{
    [SerializeField]
    public float time;
    [SerializeField]
    public int secondsPerSecond;

    void Update()
    {
        time += secondsPerSecond * Time.deltaTime;

        //if time exeeds one day the timer is reset
        //TODO: Sync server on timer reset.
        if (time >= 86400)
        {
            time -= 86400;
            //You can Sync up time here when it is set to zero
        }
    }

    //sends current time whenever requested
    public float GetTime()
    {
        return time;
    }
}
