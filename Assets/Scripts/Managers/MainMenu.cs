using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] public Button _levelButton1, _levelButton2, _levelButton3, _levelButton4, _levelButton5, _levelButton6, _levelButton7;
    [SerializeField] public Toggle _toggelFast, _toggleSlow;

    [SerializeField] private ScriptableValue levelSO, speedSO;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            levelSO._value = 0;
            if (speedSO._value == 2)
            {
                _toggelFast.isOn = false;
                _toggleSlow.isOn = true;
            }
        }
    }

    void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            if(levelSO._value == 0)
            {
                _levelButton1.interactable = true;
                _levelButton2.interactable = false;
                _levelButton3.interactable = false;
                _levelButton4.interactable = false;
                _levelButton5.interactable = false;
                _levelButton6.interactable = false;
                _levelButton7.interactable = false;

            }
            if (levelSO._value == 1)
            {
                _levelButton1.interactable = false;
                _levelButton2.interactable = true;
                _levelButton3.interactable = true;
                _levelButton4.interactable = false;
                _levelButton5.interactable = false;
                _levelButton6.interactable = false;
                _levelButton7.interactable = false;

            }
            if (levelSO._value == 2 )
            {
                _levelButton1.interactable = false;
                _levelButton2.interactable = false;
                _levelButton3.interactable = false;
                _levelButton4.interactable = true;
                _levelButton5.interactable = true;
                _levelButton6.interactable = false;
                _levelButton7.interactable = false;

            }
            if ( levelSO._value == 3)
            {
                _levelButton1.interactable = false;
                _levelButton2.interactable = false;
                _levelButton3.interactable = false;
                _levelButton4.interactable = false;
                _levelButton5.interactable = true;
                _levelButton6.interactable = true;
                _levelButton7.interactable = false;

            }
            if (levelSO._value == 4 || levelSO._value == 5 || levelSO._value == 6)
            {
                _levelButton1.interactable = false;
                _levelButton2.interactable = false;
                _levelButton3.interactable = false;
                _levelButton4.interactable = false;
                _levelButton5.interactable = false;
                _levelButton6.interactable = false;
                _levelButton7.interactable = true;

            }
            if (levelSO._value == 7)
            {
                _levelButton1.interactable = true;
                _levelButton2.interactable = false;
                _levelButton3.interactable = false;
                _levelButton4.interactable = false;
                _levelButton5.interactable = false;
                _levelButton6.interactable = false;
                _levelButton7.interactable = false;

            }
        }

    }

    public void StartGame ()
    {
        SceneManager.LoadScene(8);
    }

    public void OpenShop()
    {
        SceneManager.LoadScene(9);
    }

    public void ReturnToMenu()
    {
        levelSO._value = 0;
        SceneManager.LoadScene(0);
        
    }

    public void Startmap1()
    {
        levelSO._value = 1;
        SceneManager.LoadScene(1);
        
    }

    public void Startmap2()
    {
        levelSO._value = 2;
        SceneManager.LoadScene(2);
        
    }

    public void Startmap3()
    {
        levelSO._value = 3;
        SceneManager.LoadScene(3);
        
    }

    public void Startmap4()
    {
        levelSO._value = 4;
        SceneManager.LoadScene(4);
        
    }
    public void Startmap5()
    {
        levelSO._value = 5;
        SceneManager.LoadScene(5);
        
    }
    public void Startmap6()
    {
        levelSO._value = 6;
        SceneManager.LoadScene(6);
        
    }
    public void Startmap7()
    {
        levelSO._value = 7;
        SceneManager.LoadScene(7);

    }

    public void SetFast()
    {
        speedSO._value = 1;
    }
    public void SetSlow()
    {
        speedSO._value = 2;
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
