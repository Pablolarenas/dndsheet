
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Modificator : MonoBehaviour
{
    private string[] listOfMod = { "-5", "-5", "-4", "-4", "-3", "-3", "-2", "-2", "-1", "-1", "+0", "+0", "+1", "+1", "+2", "+2", "+3", "+3", "+4", "+4", "+5", "+5", "+6", "+6", "+7", "+7", "+8", "+8", "+9", "+9", "+10" };
    private InputField currentField;
    [SerializeField] private InputField baseField;

    private void Awake()
    {
        currentField = GetComponent<InputField>();
    }


    public void AdjustMod()
    {
        int index = int.Parse(baseField.text);
        currentField.text = listOfMod[index];
    }
}
