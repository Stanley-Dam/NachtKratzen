using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalBodyObjects : MonoBehaviour {

    //Just holds player body information
    public Animator anim;
    public Transform neck;
    public Transform head;
    public Quaternion headRotation;
    public Transform cameraHolder;
    public Canvas canvas;

    public bool isSeeker = false;

    private void Awake() {
        neck = anim.GetBoneTransform(HumanBodyBones.Neck);
        head = anim.GetBoneTransform(HumanBodyBones.Head);
        headRotation = neck.transform.localRotation;
    }

}
