/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioSource audio_;

    private void Start()
    {
        SliderController.oldVolume = SliderController.slider.value;
        if (!PlayerPrefs.HasKey("volume")) audio_.volume = 1;
        else SliderController.slider.value = PlayerPrefs.GetFloat("volume");
    }
    private void Update()
    {
        audio_.volume = PlayerPrefs.GetFloat("volume");
    }
}
*/