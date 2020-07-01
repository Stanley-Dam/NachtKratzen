using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTypeTransition : MonoBehaviour {

    [SerializeField] private RectTransform rect;
    [SerializeField] private float startSize = 5;
    [SerializeField] private Vector2 position = new Vector2(-512, 512);

    private Vector2 originalPosition;

    private void Awake() {
        originalPosition = rect.anchoredPosition;
    }

    // Update is called once per frame
    void Update() {
        if (startSize > 1) {
            startSize -= 2f * Time.deltaTime;
            rect.localScale = new Vector3(startSize, startSize, startSize);
            float x = Mathf.Lerp(position.x, originalPosition.x, 2f * Time.deltaTime);
            float y = Mathf.Lerp(position.y, originalPosition.y, 2f * Time.deltaTime);

            position = new Vector2(x, y);
            rect.anchoredPosition = position;
        } else {
            Destroy(this);
        }
    }
}
