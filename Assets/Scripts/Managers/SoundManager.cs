using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private GameObject soundPlayer;

    [SerializeField] public AudioSource _AudioSource1;
    [SerializeField] public AudioSource _AudioSource2;
    [SerializeField] public AudioSource _AudioSource3;
    [SerializeField] public AudioSource _AudioSource4;
    [SerializeField] public AudioSource _AudioSource5;
    [SerializeField] public AudioSource _AudioSource6;
    [SerializeField] public AudioSource _AudioSource7;
    [SerializeField] public AudioSource _AudioSource8;
    [SerializeField] public AudioSource _AudioSource9;
    [SerializeField] public AudioSource _AudioSource10;



    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(soundPlayer);
        // only need one music player
        if (GameObject.Find("Sound") != null && GameObject.Find("Sound") != soundPlayer)
        {
            Destroy(soundPlayer);
        }
    }

    // Update is called once per frame
     void Update()
    {
        
    }

    public void PlayHitSound()
    {
        _AudioSource1.Play();

    }

    public void PlayArmorSound()
    {
        _AudioSource2.Play();

    }

    public void PlayMissSound()
    {
        _AudioSource3.Play();

    }

    public void PlayArrowSound()
    {
        _AudioSource4.Play();

    }

    public void PlayFireballSound()
    {
        _AudioSource5.Play();

    }

    public void PlayHealSound()
    {
        _AudioSource6.Play();

    }

    public void PlayBuffSound()
    {
        _AudioSource7.Play();

    }

    public void PlayTankSound()
    {
        _AudioSource8.Play();

    }

    public void PlayWalkingSound()
    {
        _AudioSource9.Play();

    }

    public void StopWalkingSound()
    {
        _AudioSource9.Stop();

    }

    public void PlayDeathSound()
    {
        _AudioSource10.Play();

    }
}
    

