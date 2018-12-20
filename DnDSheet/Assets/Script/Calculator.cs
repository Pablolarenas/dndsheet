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
    [SerializeField] private InputField lifeInput;
    [SerializeField] private GameObject parentObject;
    [SerializeField] private Text currentLifeText;
    private Player player;
    private string equation;
    ExpressionParser parser;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().player;
    }

    public void SetMaxLife()
    {
        player.SetMaxLife(int.Parse(lifeInput.text));
        currentLifeText.text = string.Concat("HP ", player.Life.ToString(), "/", player.MaximumLife.ToString());
    }

    public void Toggle()
    {
        parentObject.SetActive(!parentObject.activeSelf);
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
                player.Life = player.Life + Mathf.Abs((float)exp.Value);
                if (player.Life < -player.MaximumLife) player.Life = -player.MaximumLife; else if (player.Life > player.MaximumLife) player.Life = player.MaximumLife;
                break;
            case TypeOfCalculation.Damage:
                player.Life = player.Life - Mathf.Abs((float)exp.Value);
                if (player.Life < -player.MaximumLife) player.Life = -player.MaximumLife; else if (player.Life > player.MaximumLife) player.Life = player.MaximumLife;
                break;
            case TypeOfCalculation.TempHp:
                player.Life = player.Life >= 0 ? player.Life + Mathf.Abs((float)exp.Value) : player.Life - Mathf.Abs((float)exp.Value);
                break;
            default:
                break;
        }

        currentLifeText.text = string.Concat("HP ", player.Life.ToString(), "/", player.MaximumLife.ToString());
        calculatorInput.text = string.Empty;
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
