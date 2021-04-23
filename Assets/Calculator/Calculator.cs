using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calculator : MonoBehaviour
{
    public InputField inputNum1;
    public InputField inputNum2;
    public Text outputResult;

    private void Start()
    {
        outputResult.text = "0";
    }

    public void Add()
    {
        int num1 = int.Parse(inputNum1.text);
        int num2 = int.Parse(inputNum2.text);
        outputResult.text = (num1 + num2).ToString();
    }

    public void Subtract()
    {
        int num1 = int.Parse(inputNum1.text);
        int num2 = int.Parse(inputNum2.text);
        outputResult.text = (num1 - num2).ToString();
    }
}
