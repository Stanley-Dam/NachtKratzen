using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsRenderingScript : MonoBehaviour
{
    private GameObject test;

    private void OnBecameVisible()
    {
        print("I am visible");
    }

    private void OnBecameInvisible()
    {
        print("I am invisible");
    }
}
