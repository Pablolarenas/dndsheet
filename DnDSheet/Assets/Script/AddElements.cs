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
    private UiManager uiManager;

    private void Awake()
    {
        uiManager = FindObjectOfType<UiManager>();
    }

    public void UpdateValue()
    {
        int totalValue = 0;
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
}
