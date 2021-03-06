﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public Player player;
    private UiManager uiManager;

    private void Awake()
    {
        uiManager = FindObjectOfType<UiManager>();
    }
}

[Serializable]
public class Player
{
    public string Name;
    public float MaximumLife;
    public float Life
    {
        set
        {
            life = value;
            uiManager.SetLifePorcentage(life);
        }

        get
        {
            return life;
        }
    }

    public void SetMaxLife(float valor)
    {
        Debug.Log(valor);
        MaximumLife = Life = valor;
    }
    private float life;
    [SerializeField] private UiManager uiManager;
}

