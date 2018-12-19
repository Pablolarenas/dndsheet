using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using B83.ExpressionParser;

public class Calculator : MonoBehaviour
{
    public enum TypeOfCalculation {  Heal, Damage, TempHp }
    private TypeOfCalculation typeOfCalculation;
    [SerializeField] private InputField calculatorInput;
    [SerializeField] private Image lifeBar;
    private Player player;
    private string equation;
    ExpressionParser parser;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().player;
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        calculatorInput.text = string.Empty;
    }

    public void Calculate(int calculation)
    {
        typeOfCalculation = (TypeOfCalculation)calculation;

        equation = calculatorInput.text;
        if (equation == string.Empty) return;
        equation = equation.Replace("x", "*");
        parser = new ExpressionParser();
        Expression exp = parser.EvaluateExpression(equation);

        if (double.IsInfinity(exp.Value)) return;

        switch (typeOfCalculation)
        {
            case TypeOfCalculation.Heal:
                player.Life += (float)exp.Value;
                if (player.Life > player.MaximumLife) player.Life = player.MaximumLife; else if (player.Life < 0) player.Life = 0;
                break;
            case TypeOfCalculation.Damage:
                player.Life -= (float)exp.Value;
                if (player.Life < 0) player.Life = 0; else if (player.Life > player.MaximumLife) player.Life = player.MaximumLife;
                break;
            case TypeOfCalculation.TempHp:
                player.Life += (float)exp.Value;
                break;
            default:
                break;
        }

        Debug.Log(exp.Value + " / " + player.Life);
    }

    public void WriteChar(Text text)
    {
        calculatorInput.text += text.text;
    }

    public void DeleteChar()
    {
        if (calculatorInput.text.Length == 0) return;
        calculatorInput.text = calculatorInput.text.Remove(calculatorInput.text.Length - 1);
    }
}
