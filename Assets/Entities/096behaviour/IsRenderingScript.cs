using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsRenderingScript : MonoBehaviour
{
    public delegate void IsVisibleEvent(bool isNachtKrabVisible);
    public static event IsVisibleEvent isVisibleEvent;

    private bool isVisible;

    private void OnBecameVisible()
    {
        isVisible = true;
        isVisibleEvent(isVisible);
    }

    private void OnBecameInvisible()
    {
        isVisible = false;
        isVisibleEvent(isVisible);
    }
}
