using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class UiElement
{
    public string Name;
    public UiState UiState;
    public GameObject UiObject;
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

    private void CheckUiState(UiState from, UiState to)
    {
        ListOfUiElement.Where(type => type.UiState == from).SingleOrDefault().UiObject.SetActive(false);
        ListOfUiElement.Where(type => type.UiState == to).SingleOrDefault().UiObject.SetActive(true);
    }

    public void SetNewUiState(UiState newUiState)
    {
        CurrentUiState = newUiState;
    }
}
