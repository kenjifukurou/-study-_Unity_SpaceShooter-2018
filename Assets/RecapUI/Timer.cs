using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    private bool toggleBool;

    public Camera _mainCam;
    public Slider _fovSlider;
    public Dropdown _dropdown;

    public InputField _inputField;
    public Text _titleText;

    public Text timerText;
    public Text timeNowText;
    public Text timeCounter;

    public string timerName;
    public float timer;

    private string _hour;
    private string _minute;
    private string _second;

    private void Start()
    {
        timerText.text = timer.ToString();
    }

    private void Update()
    {
        //_mainCam.fieldOfView = _fovSlider.value;

        if (toggleBool == true)
        {
            TimerCounter();
        }
    }

    public void TimerCounter()
    {
        DateTime currentTime = DateTime.Now;

        _hour = currentTime.Hour.ToString();
        _minute = currentTime.Minute.ToString();
        _second = currentTime.Second.ToString();

        timeCounter.text = _hour + " " + _minute + " " + _second;

        timer -= Time.deltaTime;
        Debug.Log(timer);
        timerText.text = timer.ToString("#.0000");
        
        Debug.Log(currentTime);
        timeNowText.text = currentTime.ToString();
    }

    public void ToggleChanged(bool newValue)
    {
        Debug.Log(newValue);
        toggleBool = newValue;
    }

    public void changeFOV()
    {
        _mainCam.fieldOfView = _fovSlider.value;
    }

    public void dropDownChangeSize()
    {
        switch (_dropdown.value)
        {
            case 0:
                {
                    Debug.Log("Power up 1");
                    transform.localScale = new Vector3(2, 2, 2);
                }
                break;
            case 1:
                {
                    Debug.Log("Power up 2");
                    transform.localScale = new Vector3(4, 4, 4);
                }
                break;
            case 2:
                {
                    Debug.Log("Power up 3");
                    transform.localScale = new Vector3(8, 8, 8);
                }
                break;
            default:
                {
                    Debug.Log("Nothing selected");
                }
                break;
        }
    }

    public void inputFieldEndEdit()
    {
        _titleText.text = _inputField.text;
    }
}
