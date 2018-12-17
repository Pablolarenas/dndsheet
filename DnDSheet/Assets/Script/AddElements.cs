using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class AddElements : MonoBehaviour
{
    public List<InputField> ListOfElementsToAdd;
    [SerializeField] private InputField result;
    [Header("-----------------------------")]
    [SerializeField] private Toggle toggleToListen;
    [Header("-----------------------------")]
    private UiManager uiManager;
    public enum TypeOfCalculation { Suma, Multiplicacion };
    public TypeOfCalculation typeOfCalculation = TypeOfCalculation.Suma;
    [Header("-----------------------------")]
    [SerializeField] private int startingValue = 0;
    private delegate void CalculationMethod();
    private CalculationMethod calculationMethod;

    private void Awake()
    {
        uiManager = FindObjectOfType<UiManager>();

        switch (typeOfCalculation)
        {
            case TypeOfCalculation.Suma:
                calculationMethod = Suma;
                break;
            case TypeOfCalculation.Multiplicacion:
                calculationMethod = Multiplicacion;
                if (startingValue == 0) startingValue = 1;
                break;
            default:
                break;
        }
    }

    private void Suma()
    {
        int totalValue = startingValue;
        foreach (InputField item in ListOfElementsToAdd)
        {
            if (item.text == string.Empty) continue;

            if(item.text[0] == '-')
            {
                totalValue -= int.Parse(Regex.Match(item.text, @"\d+").Value);
            }
            else
            {
                totalValue += int.Parse(Regex.Match(item.text, @"\d+").Value);
            }
        }

        if(toggleToListen != null && toggleToListen.isOn)
        {
            totalValue += uiManager.ProfValue;
        }

        string value = totalValue.ToString();

        if (totalValue > 0)
        {
            value = string.Concat("+", value);
        }

        result.text = value;
    }

    private void Multiplicacion()
    {
        int totalValue = startingValue;
        foreach (InputField item in ListOfElementsToAdd)
        {
            if (item.text == string.Empty) continue;

            if (item.text[0] == '-')
            {
                totalValue *= -int.Parse(Regex.Match(item.text, @"\d+").Value);
            }
            else
            {
                totalValue *= int.Parse(Regex.Match(item.text, @"\d+").Value);
            }
        }

        if (toggleToListen != null && toggleToListen.isOn)
        {
            totalValue += uiManager.ProfValue;
        }

        string value = totalValue.ToString();

        if (totalValue > 0)
        {
            value = string.Concat("+", value);
        }

        result.text = value;
    }

    public void UpdateValue()
    {
        calculationMethod();
    }
}
