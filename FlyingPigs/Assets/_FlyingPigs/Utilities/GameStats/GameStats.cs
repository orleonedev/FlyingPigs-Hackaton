using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName ="GameStats", menuName ="GameStatsScriptable")]
public class GameStats : ScriptableObject //, IOnValuesChanged
{
    //public event System.Action OnValuesChanged;
    public delegate void OnValuesChangedDelegate();
    public OnValuesChangedDelegate OnValuesChanged;

    [Range(0f, 1f)]
    public float _realHealth;
    public float RealHealth
    {
        get {
            return _realHealth;
        }
        set {
            _realHealth = Math.Clamp(value,0f,1f);
            OnValuesChanged?.Invoke();
        }
    }

    [Range(0f, 1f)]
    public float _gameHealth;
    public float GameHealth{
        get {
            return _gameHealth;
        }
        set {
            _gameHealth = Math.Clamp(value,0f,1f);;
            OnValuesChanged?.Invoke();
        }
    }

    [Range(0f, 999f)]
    public float _realMoney = 120f;
    public float RealMoney{
        get {
            return _realMoney;
        }
        set {
            _realMoney = Math.Clamp(value,0f,999f);
            OnValuesChanged?.Invoke();
        }
    }

    [Range(1,10)]
    public uint _moneyMultiplier = 2;
    public uint MoneyMultiplier {
        get {
            return _moneyMultiplier;
        }
        set {
            _moneyMultiplier = Math.Clamp(value,1,10);
            OnValuesChanged?.Invoke();
        }
    }
    
    [Range(0f, 999f)]
    public float _gameCurrency = 300f;
    public float GameCurrency {
        get {
            return _gameCurrency;
        }
        set {
            _gameCurrency = Math.Clamp(value,0f,999f);
            OnValuesChanged?.Invoke();
        }
    }

    [Range(1,10)]
    public uint _currencyMultiplier = 1;
    public uint CurrencyMultiplier {
        get {
            return _currencyMultiplier;
        }
        set {
            _currencyMultiplier = Math.Clamp(value,1,10);
            OnValuesChanged?.Invoke();
        }
    }

    [Range(0,23)]
    public uint _hours = 18;
    public uint Hours {
        get {
            return _hours;
        }
        set {
            _hours = Math.Clamp(value,0,23);
            OnValuesChanged?.Invoke();
        }
    }

    [Range(0,59)]
    public uint _minutes = 30;
    public uint Minutes {
        get {
            return _minutes;
        }
        set {
            _minutes = Math.Clamp(value,0,59);
            OnValuesChanged?.Invoke();
        }
    }

    [Range(1,30)]
    public uint _day = 1;
    public uint Day {
        get {
            return _day;
        }
        set {
            _day = Math.Clamp(value,1,30);
            OnValuesChanged?.Invoke();
        }
    }

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
