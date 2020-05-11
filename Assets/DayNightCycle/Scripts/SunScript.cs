using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunScript : MonoBehaviour
{
    [SerializeField]
    TimeScript timeScript;
    void Update()
    {
        transform.rotation = Quaternion.Euler(360f / 86400f * timeScript.GetTime(), 0,0);

        print(timeScript.GetTime());
    }
}
