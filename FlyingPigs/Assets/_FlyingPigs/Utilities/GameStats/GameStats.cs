using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName ="GameStats", menuName ="GameStatsScriptable")]
public class GameStats : ScriptableObject, IOnValuesChanged
{
    public event System.Action OnValuesChanged;
    // public delegate void OnValuesChangedDelegate();
    // public OnValuesChangedDelegate OnValuesChanged;

    //[Range(0f, 1f)]
    public float RealHealth {
        get {
            return RealHealth;
        }
        set {
            if (RealHealth != value) {
                RealHealth = value;
                OnValuesChanged?.Invoke();
            }
        }
    }

    [Range(0f, 1f)]
    public float GameHealth = 0.5f;

    [Range(0f, 999f)]
    public float RealMoney = 120f;

    [Range(1,10)]
    public uint MoneyMultiplier = 2;
    
    [Range(0f, 999f)]
    public float GameCurrency = 300f;

    [Range(1,10)]
    public uint CurrencyMultiplier = 1;

    [Range(0,23)]
    public uint Hours = 18;

    [Range(0,59)]
    public uint Minutes = 30;

    [Range(1,30)]
    public uint Day = 1;

    public void OnEnable() {
        this.RealHealth = 0.5f;
        this.GameHealth = 0.5f;
        this.RealMoney = 120f;
        this.MoneyMultiplier = 2;
        this.GameCurrency = 300f;
        this.CurrencyMultiplier = 1;
        this.Hours = 18;
        this.Minutes = 30;
        this.Day = 1;
    }

}
