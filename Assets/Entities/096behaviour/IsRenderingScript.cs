using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class IsRenderingScript : MonoBehaviour {

    public delegate void IsVisibleEvent(bool isNachtKrabVisible);
    public static event IsVisibleEvent isVisibleEvent;

    private void Update() {
        bool isVisible = InSight(this.transform);

        if (isVisible) {
            Transform camTransform = Camera.main.transform;
            RaycastHit hit;
            Vector3 direction = camTransform.TransformDirection(Vector3.forward) - (camTransform.position - transform.root.position);
            int layerMask = 1 << 9;
            layerMask = ~layerMask;

            if (Physics.Raycast(camTransform.position, direction, out hit, Mathf.Infinity, layerMask)) {
                if (hit.transform == transform.root) {
                    if (isVisibleEvent != null)
                        isVisibleEvent(isVisible);
                }
            }
        }
    }

    private bool InSight(Transform transform) {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }

}
