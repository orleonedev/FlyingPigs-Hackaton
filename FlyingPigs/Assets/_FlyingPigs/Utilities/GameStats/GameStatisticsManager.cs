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

    public delegate void OnDepleatedStatDelegate(GameStatsEnum stat);
    public OnDepleatedStatDelegate OnDepleatedStat;

    public SerializableDictionary<GameStatsEnum,float> fixedUpdates = new SerializableDictionary<GameStatsEnum, float>(){
            {GameStatsEnum.GameHealth, -0.2f},
            {GameStatsEnum.RealMoney, 40},
            {GameStatsEnum.RealHealth, -0.1f}
        };

    public void updateStatsWith(SerializableDictionary<GameStatsEnum,float> updates) {
        
        foreach (KeyValuePair<GameStatsEnum,float> stat in updates) {
            
            switch (stat.Key) {
                case GameStatsEnum.RealHealth:
                    gameStats.RealHealth += stat.Value;
                    if (gameStats.RealHealth <= 0) {
                        OnDepleatedStat(GameStatsEnum.RealHealth);
                    }
                break;
                case GameStatsEnum.GameHealth:
                    gameStats.GameHealth += stat.Value;
                    if (gameStats.GameHealth <= 0) {
                        OnDepleatedStat(GameStatsEnum.GameHealth);
                    }
                break;
                case GameStatsEnum.RealMoney:
                    gameStats.RealMoney += stat.Value;
                    if (gameStats.RealMoney <= 0) {
                        OnDepleatedStat(GameStatsEnum.RealMoney);
                    }
                break;
                case GameStatsEnum.GameCurrency:
                    gameStats.GameCurrency += stat.Value;
                    if (gameStats.GameCurrency <= 0) {
                        gameStats.RealMoney -= 100f;
                        if (gameStats.RealMoney <= 0) {
                            OnDepleatedStat(GameStatsEnum.RealMoney);
                        }
                        gameStats.GameCurrency += 500f;

                    }
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
        gameStats.RealMoney = 1200f;
        gameStats.MoneyMultiplier = 1;
        gameStats.GameCurrency = 300f;
        gameStats.CurrencyMultiplier = 1;
        gameStats.CurrentHours = 0;
        gameStats.CurrentMinutes = 0;
        gameStats.Day = 0;
        gameStats.NextPlayTime = 210;
        gameStats.ModifierPlayTime = 0;
        gameStats.TimeElapsed = 0f;
        gameStats.Employer = EmployerKind.John;
    }
}
