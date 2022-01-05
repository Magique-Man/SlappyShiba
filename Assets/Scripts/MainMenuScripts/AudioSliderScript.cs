using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSliderScript : MonoBehaviour
{
    [SerializeField] private Slider audioSlider;

    [SerializeField] private float sliderValue;
    [SerializeField] private int sliderValueInt;

    private void Awake()
    {
        GetAudioValue();
    }

    private void Start()
    {
        audioSlider.value = sliderValue;
    }

    void Update()
    {
        AudioListener.volume = audioSlider.value / 100;

        if(audioSlider.value != sliderValue)
        {
            sliderValue = audioSlider.value;
            SetAudioValue();
        }
    }

    void GetAudioValue()
    {
       // sliderValue = PlayerPrefs.GetFloat("audio_value", 0.5f);
        sliderValue = PlayerPrefs.GetInt("audio_value", 50);
    }

    void SetAudioValue()
    {
        //PlayerPrefs.SetFloat("audio_value", sliderValue);
        PlayerPrefs.SetInt("audio_value", (int)sliderValue);
    }
}
