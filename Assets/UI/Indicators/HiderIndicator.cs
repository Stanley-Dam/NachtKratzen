using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiderIndicator : MonoBehaviour {

    /** Got this system from: https://www.youtube.com/watch?v=BC3AKOQUx04
     * Modified it to work in our system ofcourse ;)
     * 
     * ~Stanley Dam
     */

    [SerializeField] private const float maxTimer = 2f;
    private float timer = maxTimer;

    private CanvasGroup canvasGroup;
    protected CanvasGroup CanvasGroup {
        get {
            if (canvasGroup == null) {
                canvasGroup = this.GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                    canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }

            return canvasGroup;
        }
    }

    private RectTransform rectTransform;
    protected RectTransform RectTransform {
        get {
            if (rectTransform == null) {
                rectTransform = this.GetComponent<RectTransform>();
                if (rectTransform == null)
                    rectTransform = gameObject.AddComponent<RectTransform>();
            }

            return rectTransform;
        }
    }

    public Hider targetHider { get; protected set; } = null;
    private Transform player;

    private IEnumerator countDown;
    private Action unRegister;

    private Quaternion targetRotation = Quaternion.identity;
    private Vector3 targetPosition = Vector3.zero;

    public void Register(Hider targetHider, Transform player, Action unRegister) {
        this.targetHider = targetHider;
        this.player = player;
        this.unRegister = unRegister;

        StartCoroutine(RotateToTarget());
        StartTimer();
    }

    public void Restart() {
        timer = maxTimer;
        StartTimer();
    }

    private void StartTimer() {
        if(countDown != null)
            StopCoroutine(countDown);

        countDown = CountDown();
        StartCoroutine(countDown);
    }

    private IEnumerator CountDown() {
        while(CanvasGroup.alpha < 1f) {
            CanvasGroup.alpha += 4 * Time.deltaTime;
            yield return null;
        }

        while(timer > 0) {
            timer--;

            yield return new WaitForSeconds(1);
        }

        while(CanvasGroup.alpha > 0) {
            CanvasGroup.alpha -= 2 * Time.deltaTime;
            yield return null;
        }

        unRegister();
        Destroy(this.gameObject);
    }

    private IEnumerator RotateToTarget() {
        while(enabled) {
            if(targetHider) {
                targetPosition = targetHider.transform.position;
                targetRotation = targetHider.transform.rotation;
            }

            Vector3 direction = player.position - targetPosition;
            targetRotation = Quaternion.LookRotation(direction);
            targetRotation.z = -targetRotation.y;
            targetRotation.x = 0;
            targetRotation.y = 0;

            Vector3 northDirection = new Vector3(0, 0, player.eulerAngles.y);
            RectTransform.localRotation = targetRotation * Quaternion.Euler(northDirection);

            yield return null;
        }
    }
}
