using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeClock : MonoBehaviour
{
    [SerializeField]
    private float clockHour, clockMin, clockSec;

    [SerializeField] TimeScript timeScript;

    //[SerializeField] Text rawClockTXT, digitalClockTXT;

    private void Update()
    {
        clockHour = Mathf.Floor(timeScript.GetTime() / 3600);
        clockMin = Mathf.Floor(timeScript.GetTime() / 60 - clockHour * 60);
        clockSec = Mathf.Floor(timeScript.GetTime() - clockMin * 60 - clockHour * 3600);

        //rawClockTXT.text = "Raw time: " + Mathf.Floor(timeScript.GetTime());
        //digitalClockTXT.text = "Digital Time: " + clockHour + ":" + clockMin + ":" + clockSec;
    }
}
