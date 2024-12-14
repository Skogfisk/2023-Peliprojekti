using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class VolumeManager : MonoBehaviour
{
    public static VolumeManager Instance;
    [SerializeField] public Slider soundSlider;
    [SerializeField] public Slider volumeSlider;
    public float volumeSliderValue;
    public float soundSliderValue;

    void Awake()
    {
        Instance = this;
        volumeSlider.value = volumeSliderValue;
        soundSlider.value = soundSliderValue;
    }

    public void ChangeMusicVolume()
    {
        volumeSliderValue = volumeSlider.value;
        AudioManager.Instance._AudioSource1.volume = volumeSlider.value;
        AudioManager.Instance._AudioSource2.volume = volumeSlider.value;
        AudioManager.Instance._AudioSource3.volume = volumeSlider.value;
        AudioManager.Instance._AudioSource4.volume = volumeSlider.value;
        AudioManager.Instance._AudioSource5.volume = volumeSlider.value;
    }

    public void ChangeSoundVolume()
    {
        soundSliderValue = soundSlider.value;
        SoundManager.Instance._AudioSource1.volume = soundSlider.value;
        SoundManager.Instance._AudioSource2.volume = soundSlider.value;
        SoundManager.Instance._AudioSource3.volume = soundSlider.value;
        SoundManager.Instance._AudioSource4.volume = soundSlider.value;
        SoundManager.Instance._AudioSource5.volume = soundSlider.value;
        SoundManager.Instance._AudioSource6.volume = soundSlider.value;
        SoundManager.Instance._AudioSource7.volume = soundSlider.value;
        SoundManager.Instance._AudioSource8.volume = soundSlider.value;
    }
}
