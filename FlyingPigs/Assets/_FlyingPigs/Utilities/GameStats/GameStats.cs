using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName ="GameStats", menuName ="GameStatsScriptable")]
public class GameStats : SingletonScriptableObject<GameStats>
{
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

    [Range(0f, 9999f)]
    public float _realMoney = 1200f;
    public float RealMoney{
        get {
            return _realMoney;
        }
        set {
            _realMoney = Math.Clamp(value,0f,9999f);
            OnValuesChanged?.Invoke();
        }
    }

    [Range(1,10)]
    public uint _moneyMultiplier = 1;
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
    public uint _currentHours = 18;
    public uint CurrentHours {
        get {
            return _currentHours;
        }
        set {
            _currentHours = Math.Clamp(value,0,23);
            OnValuesChanged?.Invoke();
        }
    }

    [Range(0,59)]
    public uint _currentMinutes = 0;
    public uint CurrentMinutes {
        get {
            return _currentMinutes;
        }
        set {
            _currentMinutes = Math.Clamp(value,0,59);
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

    [Range(0,300)]
    public uint _nextPlayTime = 240;
    public uint NextPlayTime {
        get {
            return _nextPlayTime;
        }
        set {
            _nextPlayTime = Math.Clamp(value, 0, 300 );
        }
    }

    [Range(-300,300)]
    public int _modifierPlayTime = 0;
    public int ModifierPlayTime {
        get {
            return _modifierPlayTime;
        }
        set {
            _modifierPlayTime = Math.Clamp(value, -300, 300 );
        }
    }

    [Range(0,300)]
    public float _timeElapsed = 0f;
    public float TimeElapsed {
        get {
            return _timeElapsed;
        }
        set {
            _timeElapsed = Math.Clamp(value, 0f, 300f );
        }
    }

    public EmployerKind _employer = EmployerKind.John;
    public EmployerKind Employer {
        get {
            return _employer;
        }
        set {
            _employer = value;
        }
    }

    public void OnEnable() {
        this.RealHealth = 0.6f;
        this.GameHealth = 0.6f;
        this.RealMoney = 1200f;
        this.MoneyMultiplier = 1;
        this.GameCurrency = 300f;
        this.CurrencyMultiplier = 1;
        this.CurrentHours = 0;
        this.CurrentMinutes = 0;
        this.Day = 1;
        this.NextPlayTime = 240;
        this.ModifierPlayTime = 0;
        this.TimeElapsed = 0f;
        this.Employer = EmployerKind.John;
    }

}
