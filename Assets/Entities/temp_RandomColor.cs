using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp_RandomColor : MonoBehaviour {

    [SerializeField] private List<MeshRenderer> colorable;
    [SerializeField] private Color color;

    // Start is called before the first frame update
    void Start() {
        color = new Color(Random.value, Random.value, Random.value);

        foreach (MeshRenderer renderer in colorable) {
            renderer.material.color = color;
        }
    }
}
