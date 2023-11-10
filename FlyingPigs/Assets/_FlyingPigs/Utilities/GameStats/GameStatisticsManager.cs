using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStatisticsManager
{
    static private GameStatisticsManager instance;
    static public GameStatisticsManager Instance {
        get {
            instance ??= new GameStatisticsManager();
            return instance;
        }
    }

    private GameStatisticsManager() {}
    
    public GameStats gameStats = GameStats.Instance;

    public SerializableDictionary<GameStatsEnum,float> fixedUpdates = new SerializableDictionary<GameStatsEnum, float>(){
            {GameStatsEnum.GameHealth, -0.1f},
            {GameStatsEnum.RealMoney, 40},
            {GameStatsEnum.RealHealth, -0.1f}
        };

    public void updateStatsWith(SerializableDictionary<GameStatsEnum,float> updates) {
        
        foreach (KeyValuePair<GameStatsEnum,float> stat in updates) {
            
            switch (stat.Key) {
                case GameStatsEnum.RealHealth:
                    gameStats.RealHealth += stat.Value;
                    
                break;
                case GameStatsEnum.GameHealth:
                    gameStats.GameHealth += stat.Value;
                    
                break;
                case GameStatsEnum.RealMoney:
                    gameStats.RealMoney += stat.Value;
                    
                break;
                case GameStatsEnum.GameCurrency:
                    gameStats.GameCurrency += stat.Value;
                    CheckAndRechargeCurrency();
                    
                break;

                case GameStatsEnum.NextPlayTime:
                    gameStats.NextPlayTime += (uint)stat.Value;
                break;

                case GameStatsEnum.ModifierPlayTime:
                    gameStats.ModifierPlayTime += (int)stat.Value;
                break;

                case GameStatsEnum.TimeElapsed:
                    gameStats.TimeElapsed += stat.Value;
                break;

                case GameStatsEnum.ActualEmployer:
                    switch ((int)stat.Value) {
                        case 0:
                            gameStats.Employer = EmployerKind.John;
                        break;
                        case 1:
                            gameStats.Employer = EmployerKind.Mark;
                        break;
                        case 2:
                            gameStats.Employer = EmployerKind.Bill;
                        break;
                    }
                break;
                case GameStatsEnum.SpokeToEvelyn:
                    if (stat.Value > 0f) 
                    {gameStats.SpokeToEvelyn = true; }
                    else {
                        gameStats.SpokeToEvelyn = false;
                    }
                break;
                case GameStatsEnum.SpokeToGreg:
                    if (stat.Value > 0f) 
                    {gameStats.SpokeToGreg = true; }
                    else {
                        gameStats.SpokeToGreg = false;
                    }
                break;

            }
        }
    }
    
    public void TickClock(uint delta) {

        uint minutes = gameStats.CurrentMinutes + delta;
        uint hours = gameStats.CurrentHours;

        gameStats.CurrentMinutes = minutes%60;
        if (minutes > 59 ) {
            hours = (hours+1)%24;
            gameStats.CurrentHours = hours;
        } 
    }


    public void UpdateClock(){
        TimeSpan endTime = new TimeSpan(23, 30, 0);

        // Subtract the given duration from 23:30
        TimeSpan remainingTime = endTime.Subtract(TimeSpan.FromMinutes(gameStats.CurrentDayLenght - gameStats.TimeElapsed));

        // Assign the calculated hour and minutes
        uint currentHour = (uint)remainingTime.Hours;
        uint currentMinutes = (uint) remainingTime.Minutes;
        SetClockTo(currentHour,currentMinutes);
    }

    public void SetClockTo(uint newHours, uint newMinutes) {
        gameStats.CurrentHours = newHours;
        gameStats.CurrentMinutes = newMinutes;
    }

    public void SetNextDay() {
        gameStats.Day = (gameStats.Day+1)%30;
    }

    public bool StatIsMet(GameStatsEnum gameStat, StatComparisonType comparisonType, float value) {
        switch(gameStat){
            case GameStatsEnum.RealHealth:
                if (comparisonType == StatComparisonType.LessOrEqualThan)
                {
                    return gameStats.RealHealth <= value;
                }
                else if (comparisonType == StatComparisonType.MoreThan)
                {
                    return gameStats.RealHealth > value;
                }
            break;
            case GameStatsEnum.GameHealth:
            if (comparisonType == StatComparisonType.LessOrEqualThan)
                {
                    return gameStats.GameHealth <= value;
                }
                else if (comparisonType == StatComparisonType.MoreThan)
                {
                    return gameStats.GameHealth > value;
                }
            break;
            case GameStatsEnum.RealMoney:
            if (comparisonType == StatComparisonType.LessOrEqualThan)
                {
                    return gameStats.RealMoney <= value;
                }
                else if (comparisonType == StatComparisonType.MoreThan)
                {
                    return gameStats.RealMoney > value;
                }
            break;
            case GameStatsEnum.GameCurrency:
            if (comparisonType == StatComparisonType.LessOrEqualThan)
                {
                    return gameStats.GameCurrency <= value;
                }
                else if (comparisonType == StatComparisonType.MoreThan)
                {
                    return gameStats.GameCurrency > value;
                }
            break;
            case GameStatsEnum.Day:
            if (comparisonType == StatComparisonType.LessOrEqualThan)
                {
                    return gameStats.Day <= value;
                }
                else if (comparisonType == StatComparisonType.MoreThan)
                {
                    return gameStats.Day > value;
                }
            break;
            case GameStatsEnum.ModifierPlayTime:
            if (comparisonType == StatComparisonType.LessOrEqualThan)
                {
                    return gameStats.ModifierPlayTime <= value;
                }
                else if (comparisonType == StatComparisonType.MoreThan)
                {
                    return gameStats.ModifierPlayTime > value;
                }
            break;
            case GameStatsEnum.NextPlayTime:
            if (comparisonType == StatComparisonType.LessOrEqualThan)
                {
                    return gameStats.NextPlayTime <= value;
                }
                else if (comparisonType == StatComparisonType.MoreThan)
                {
                    return gameStats.NextPlayTime > value;
                }
            break;
            case GameStatsEnum.TimeElapsed:
            if (comparisonType == StatComparisonType.LessOrEqualThan)
                {
                    return gameStats.TimeElapsed <= value;
                }
                else if (comparisonType == StatComparisonType.MoreThan)
                {
                    return gameStats.TimeElapsed > value;
                }
            break;
            case GameStatsEnum.SpokeToEvelyn:
            if (comparisonType == StatComparisonType.LessOrEqualThan)
                {
                    return gameStats.SpokeToEvelyn;
                }
                else if (comparisonType == StatComparisonType.MoreThan)
                {
                    return gameStats.SpokeToEvelyn;
                }
            break;
            case GameStatsEnum.SpokeToGreg:
            if (comparisonType == StatComparisonType.LessOrEqualThan)
                {
                    return gameStats.SpokeToGreg;
                }
                else if (comparisonType == StatComparisonType.MoreThan)
                {
                    return gameStats.SpokeToGreg;
                }
            break;
        }
        return false;
    }

    public EmployerKind ActualEmployer() {
        return gameStats.Employer;
    }

    public void RemoveFixedGameMalus() {
        fixedUpdates.Remove(GameStatsEnum.GameHealth);
    }
    
    public void ReloadGame() {
        gameStats.RealHealth = 0.6f;
        gameStats.GameHealth = 0.6f;
        gameStats.RealMoney = 760f;
        gameStats.MoneyMultiplier = 1;
        gameStats.GameCurrency = 50f;
        gameStats.CurrencyMultiplier = 1;
        gameStats.CurrentHours = 0;
        gameStats.CurrentMinutes = 0;
        gameStats.Day = 0;
        gameStats.NextPlayTime = 210;
        gameStats.ModifierPlayTime = 0;
        gameStats.TimeElapsed = 0f;
        gameStats.Employer = EmployerKind.John;
        gameStats.CurrentDayLenght = 210;
        gameStats.SpokeToEvelyn = false;
        gameStats.SpokeToGreg = false;
    }

    public bool CheckDepleatedStats(){
        if(gameStats.RealHealth <= 0f || gameStats.GameHealth <= 0f || gameStats.RealMoney <= 0f){
            return true;
        } else {
            return false;
        }
    }

    public void CheckAndRechargeCurrency() {
        if (gameStats.GameCurrency <= 0) {
            gameStats.RealMoney -= 100f;
            gameStats.GameCurrency += 500f;
        }
    }
}
