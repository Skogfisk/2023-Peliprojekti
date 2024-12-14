using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private GameObject musicPlayer;

    [SerializeField] public AudioSource _AudioSource1;
    [SerializeField] public AudioSource _AudioSource2;
    [SerializeField] public AudioSource _AudioSource3;
    [SerializeField] public AudioSource _AudioSource4;
    [SerializeField] public AudioSource _AudioSource5;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(musicPlayer);
        // only need one music player
        if (GameObject.Find("Music") != null && GameObject.Find("Music") != musicPlayer)
        {
            Destroy(musicPlayer);
        }
    }

    // Update is called once per frame
     void Update()
    {
        // change music based on what scene it is
        if (SceneManager.GetActiveScene().buildIndex == 0 && !_AudioSource1.isPlaying)
        {
            _AudioSource2.Stop();
            _AudioSource4.Stop();
            _AudioSource3.Stop();
            _AudioSource5.Stop();
            _AudioSource1.Play();
        }
        if (SceneManager.GetActiveScene().buildIndex == 8 && !_AudioSource1.isPlaying)
        {
            _AudioSource2.Stop();
            _AudioSource4.Stop();
            _AudioSource3.Stop();
            _AudioSource5.Stop();
            _AudioSource1.Play();
        }
        if (SceneManager.GetActiveScene().buildIndex == 9 && !_AudioSource4.isPlaying)
        {
            _AudioSource1.Stop();
            _AudioSource2.Stop();
            _AudioSource3.Stop();
            _AudioSource5.Stop();
            _AudioSource4.Play();
        }
        if (SceneManager.GetActiveScene().buildIndex == 1 && !_AudioSource2.isPlaying)
        {
            _AudioSource1.Stop();
            _AudioSource4.Stop();
            _AudioSource3.Stop();
            _AudioSource5.Stop();
            _AudioSource2.Play();
        }
        if (SceneManager.GetActiveScene().buildIndex == 2 && !_AudioSource2.isPlaying)
        {
            _AudioSource1.Stop();
            _AudioSource4.Stop();
            _AudioSource3.Stop();
            _AudioSource5.Stop();
            _AudioSource2.Play();
        }
        if (SceneManager.GetActiveScene().buildIndex == 3 && !_AudioSource2.isPlaying)
        {
            _AudioSource1.Stop();
            _AudioSource4.Stop();
            _AudioSource3.Stop();
            _AudioSource5.Stop();
            _AudioSource2.Play();
        }
        if (SceneManager.GetActiveScene().buildIndex == 4 && !_AudioSource3.isPlaying)
        {
            _AudioSource1.Stop();
            _AudioSource4.Stop();
            _AudioSource2.Stop();
            _AudioSource5.Stop();
            _AudioSource3.Play();
        }
        if (SceneManager.GetActiveScene().buildIndex == 5 && !_AudioSource3.isPlaying)
        {
            _AudioSource1.Stop();
            _AudioSource4.Stop();
            _AudioSource2.Stop();
            _AudioSource5.Stop();
            _AudioSource3.Play();
        }
        if (SceneManager.GetActiveScene().buildIndex == 6 && !_AudioSource3.isPlaying)
        {
            _AudioSource1.Stop();
            _AudioSource4.Stop();
            _AudioSource2.Stop();
            _AudioSource5.Stop();
            _AudioSource3.Play();
        }
        if (SceneManager.GetActiveScene().buildIndex == 7 && !_AudioSource5.isPlaying)
        {
            _AudioSource1.Stop();
            _AudioSource4.Stop();
            _AudioSource2.Stop();
            _AudioSource3.Stop();
            _AudioSource5.Play();
        }
    }

    


}
