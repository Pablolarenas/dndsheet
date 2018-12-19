using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Text.RegularExpressions;

[Serializable]
public class UiElement
{
    public string Name;
    public UiState UiState;
    public GameObject UiObject;
    public Image ButtonImage;
}

public enum UiState { Character, Stats, Combat, Inventory, Spells, Notes }

public class UiManager : MonoBehaviour
{
    public List<UiElement> ListOfUiElement;
    private int lenghtOfEnum;
    private UiState currentUiState = UiState.Character; //INITIAL STATE
    public UiState CurrentUiState
    {
        get { return currentUiState; }
        set
        {
            if (currentUiState != value)
            {
                CheckUiState(currentUiState, value);
            }
            currentUiState = value;
        }
    }
    [Header("PROF VALUE--------------------------")]
    [SerializeField] private InputField ProfValueInputField;
    [HideInInspector] public int ProfValue;
    public List<AddElements> ListOfElementsAffectedByProBonus;
    [Header("UI COLOR FLOW----------------------")]
    public Color HighlightedColor;
    public Color NormalColor;
    [Header("SWIPE-----------------------------")]
    public int Sensitivity = 50;
    [Header("PLAYER----------------------------")]
    [SerializeField] private Player player;
    [Header("LIFE------------------------------")]
    [SerializeField] private Image lifeBarCalculator;
    [SerializeField] private Image lifeBar;
    [SerializeField] private Calculator calculator;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().player;
        lenghtOfEnum = Enum.GetNames(typeof(UiState)).Length;
        CheckUiState(currentUiState);
    }

    private void CheckUiState(UiState from, UiState to)
    {
        ListOfUiElement.Where(type => type.UiState == from).SingleOrDefault().UiObject.SetActive(false);
        ListOfUiElement.Where(type => type.UiState == from).SingleOrDefault().ButtonImage.color = NormalColor;

        ListOfUiElement.Where(type => type.UiState == to).SingleOrDefault().UiObject.SetActive(true);
        ListOfUiElement.Where(type => type.UiState == to).SingleOrDefault().ButtonImage.color = HighlightedColor;
    }

    private void CheckUiState(UiState to)
    {
        ListOfUiElement.Where(type => type.UiState == to).SingleOrDefault().UiObject.SetActive(true);
        ListOfUiElement.Where(type => type.UiState == to).SingleOrDefault().ButtonImage.color = HighlightedColor;
    }

    public void SetNewUiState(UiState newUiState)
    {
        CurrentUiState = newUiState;
    }

    public void AdjustProfValue()
    {
        if (ProfValueInputField.text[0] == '-')
        {
            ProfValue = -int.Parse(Regex.Match(ProfValueInputField.text, @"\d+").Value);
        }
        else
        {
            ProfValue = int.Parse(Regex.Match(ProfValueInputField.text, @"\d+").Value);
        }
        AdjustElements();
    }

    public void AdjustElements()
    {
        foreach (AddElements item in ListOfElementsAffectedByProBonus)
        {
            item.UpdateValue();
        }
    }

    public void SwipeRight()
    {
        CurrentUiState = (UiState)(((int)CurrentUiState + 1) % lenghtOfEnum);
        Debug.Log("right current: " + CurrentUiState);
    }

    public void SwipeLeft()
    {
        int current = (int)CurrentUiState - 1;
        if (current < 0) current = lenghtOfEnum - 1;
        CurrentUiState = (UiState)(current % lenghtOfEnum);
        Debug.Log("left current: " + CurrentUiState);
    }

    public void SetLifePorcentage(float value)
    {
        value /= player.MaximumLife;
        lifeBar.fillAmount = lifeBarCalculator.fillAmount = value;
    }

    public void ToggleCalculator()
    {
        calculator.Toggle();
    }
}
