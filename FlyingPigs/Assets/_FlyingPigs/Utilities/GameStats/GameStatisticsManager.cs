using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStatisticsManager : MonoBehaviour
{
    [SerializeField]
    public GameStats gameStats;

    public delegate void OnDepleatedStatDelegate(GameStatsEnum stat);
    public OnDepleatedStatDelegate OnDepleatedStat;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateStatsWith(Dictionary<GameStatsEnum,float> updates) {
        
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
                case GameStatsEnum.PlayTime:
                    gameStats.NextPlayTime += (uint)stat.Value;
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

    public void NextDay() {
        gameStats.Day = (gameStats.Day+1)%30;
    }
}