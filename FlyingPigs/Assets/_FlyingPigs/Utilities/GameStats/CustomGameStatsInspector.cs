using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(GameStats))]
public class CustomGameStatsInspector : Editor {
    public override void OnInspectorGUI () {
        base.OnInspectorGUI();
 
        // Take out this if statement to set the value using setter when ever you change it in the inspector.
        // But then it gets called a couple of times when ever inspector updates
        // By having a button, you can control when the value goes through the setter and getter, your self.
        
            if(target.GetType() == typeof(GameStats)){
                GameStats gameStats = (GameStats)target;
                
                gameStats.RealHealth = gameStats._realHealth;
                gameStats.GameHealth = gameStats._gameHealth;
                gameStats.RealMoney = gameStats._realMoney;
                gameStats.MoneyMultiplier = gameStats._moneyMultiplier;
                gameStats.GameCurrency = gameStats._gameCurrency;
                gameStats.CurrencyMultiplier = gameStats._currencyMultiplier;
                gameStats.CurrentHours = gameStats._currentHours;
                gameStats.CurrentMinutes = gameStats._currentMinutes;
                gameStats.Day = gameStats._day;
                gameStats.NextPlayTime = gameStats._nextPlayTime;
            }
        
    }
}
#endif