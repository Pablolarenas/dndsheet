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
    [Header("-----------------------------")]
    [SerializeField] private InputField ProfValueInputField;
    [HideInInspector] public int ProfValue;
    public List<AddElements> ListOfElementsAffectedByProBonus;
    [Header("-----------------------------")]
    public Color HighlightedColor;
    public Color NormalColor;

    private void CheckUiState(UiState from, UiState to)
    {
        ListOfUiElement.Where(type => type.UiState == from).SingleOrDefault().UiObject.SetActive(false);
        ListOfUiElement.Where(type => type.UiState == from).SingleOrDefault().ButtonImage.color = NormalColor;

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
}
