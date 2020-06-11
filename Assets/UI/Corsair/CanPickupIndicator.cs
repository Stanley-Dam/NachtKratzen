using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanPickupIndicator : MonoBehaviour {

    [SerializeField] private Image image;
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private new Camera camera;

    // Update is called once per frame
    private void Update() {
        RaycastHit hit;

        int layerMask = 1 << 9;
        layerMask = ~layerMask;

        if (image.color != new Color(1, 1, 1))
            image.color = new Color(1, 1, 1);

        if (camera == null)
            return;

        if(networkManager.seeker != null && networkManager.seeker.IsMainPlayer) {
            if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, 20, layerMask)) {
                if (hit.transform.gameObject.GetComponent<Hider>()) {
                    //turn the corsair green
                    image.color = new Color(0, 1, 0);
                }
            }
        }

        if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, 7, layerMask)) {
            if (hit.transform.gameObject.GetComponent<Prop>()) {
                //turn the corsair green
                image.color = new Color(0, 1, 0);
            }
        }
    }
}
