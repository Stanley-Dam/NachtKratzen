using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Slider audioSlider;
    public void changeVolume()
    {
        AudioListener.volume = audioSlider.value / audioSlider.maxValue;
    }
}
