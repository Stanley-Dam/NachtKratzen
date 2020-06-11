using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotation : MonoBehaviour {

    [SerializeField] private LocalBodyObjects bodyObjects;

    private Vector3 startRotationEuler;
    private Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
        startRotationEuler = anim.GetBoneTransform(HumanBodyBones.Neck).localRotation.eulerAngles;
    }

    void OnAnimatorIK(int layerIndex) {
        Quaternion rotation = Quaternion.Euler(startRotationEuler);

        if (bodyObjects.isSeeker)
            rotation *= Quaternion.Euler(new Vector3(0, 0, bodyObjects.headRotation.eulerAngles.x));
        else
            rotation *= Quaternion.Euler(new Vector3(0, bodyObjects.headRotation.eulerAngles.x));

        anim.SetBoneLocalRotation(HumanBodyBones.Neck, rotation);
    }
}
