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

    public void UpdateValue()
    {
        int totalValue = 0;
        foreach (InputField item in ListOfElementsToAdd)
        {
            if (item.text == string.Empty) continue;

            totalValue += int.Parse(Regex.Match(item.text, @"\d+").Value);
        }

        if(toggleToListen != null && toggleToListen.isOn)
        {
            totalValue += FindObjectOfType<UiManager>().ProfValue;
        }

        result.text = totalValue.ToString();
    }
}
