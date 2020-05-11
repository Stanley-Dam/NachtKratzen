using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunScript : MonoBehaviour
{
    [SerializeField]
    private float second, time;
    [SerializeField]
    private int secondsPerSecond;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            time += secondsPerSecond * Time.deltaTime;
        if (time >= 86400)
        {
            time -= 86400;
        }
        transform.rotation = Quaternion.Euler(360f / 86400f * time, 0,0);
    }
    //3600 * 24 = 86400
}
