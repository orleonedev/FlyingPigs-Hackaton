using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{

    [SerializeField]
    public GameStateManager gameState;

    [SerializeField]
    public GameStatisticsManager statsManager;
    
    public delegate void OnMinigameEventDelegate();
    public OnMinigameEventDelegate OnMinigameEvent;
    public delegate void OnChatEventDelegate();
    public OnChatEventDelegate OnChatEvent;

    private bool loopEnabled = false;
    private bool alive = true;
    private bool eventTriggered = false;

    private bool tickClock = false;
    private float timelapse = 1f;
    private float dayLength; 

    private GameEventTypes eventToFire;
    // Start is called before the first frame update
    void Start()
    {
        statsManager.OnDepleatedStat += OnDepleatedStat;
        PrepareForNextDay();
        SetLoopTo(true);
        Debug.Log("LOOP START");
    }

    // Update is called once per frame
    void Update() {
        if(alive){ 
            if (loopEnabled) {
                timelapse += Time.deltaTime;

                if (((int)timelapse) % 2 == 0 && !tickClock) {
                    tickClock = true;
                    // Update the UI
                    Debug.Log("UPDATE THE CLOCK");
                    statsManager.TickClock((uint)2);
                    
                }
                if((int)timelapse % 2 == 1 && tickClock) {
                    tickClock = false;
                }
                if (timelapse >= dayLength && loopEnabled) {
                    Debug.Log("STOP DAY");
                    CloseAndRestart();
                    return;
                }

                


                if (((int)timelapse) % 30 == 0 && !eventTriggered) {
                    eventTriggered = true;
                    Debug.Log("SEND EVENT");
                    if ((int)timelapse == (int)dayLength -30) {
                        //fire both events
                        OnMinigameEvent?.Invoke();
                        OnChatEvent?.Invoke();
                    } else {
                        //fire one
                        switch (eventToFire) {
                            case GameEventTypes.MinigameEvent:
                            OnMinigameEvent?.Invoke();
                            break;
                            case GameEventTypes.ChatEvent:
                            OnChatEvent.Invoke();
                            break;
                        }
                        Debug.Log("FIRE");

                    }
                }
                if((int)timelapse % 30 == 1 && eventTriggered) {
                    eventTriggered = false;
                }
        }
    }
    }

    public void PrepareForNextDay() {
        timelapse = 1f;
        dayLength = statsManager.gameStats.NextPlayTime;
        float hoursDifference = dayLength/60f;
        
        uint newCurrentHours = (uint)(24f-hoursDifference);
        uint newCurrentMinutes = (uint)Math.Floor(dayLength%60);
        statsManager.SetClockTo(newCurrentHours,newCurrentMinutes);
        statsManager.NextDay();
        eventToFire = GetRandomEventType();
        Dictionary<GameStatsEnum,float> updates = new Dictionary<GameStatsEnum, float>(){
            {GameStatsEnum.GameHealth, -0.2f},
            {GameStatsEnum.RealMoney, 40},
            {GameStatsEnum.RealHealth, -0.1f}
        };
        statsManager.updateStatsWith(updates);
        MinigamesList.Instance.PlayedGamesOfTheDay = new List<string>();
        }

    public void SetLoopTo(bool state) {
        loopEnabled = state;
    }

    private GameEventTypes GetRandomEventType() {
        var values = Enum.GetValues(typeof(GameEventTypes));
        var random = new System.Random();
        return (GameEventTypes)values.GetValue(random.Next(values.Length));
    }

    public void CloseAndRestart() {
        SetLoopTo(false);
        Debug.Log("RESTART");
        // transizione
        SetLoopTo(true);
        Debug.Log("LOOP START");
        PrepareForNextDay();
        

    }

    public void OnDepleatedStat(GameStatsEnum stat){
        Debug.Log("DEAD: " + stat.ToString());
        SetLoopTo(false);
        alive = false;
        //Gestire morte
    }
}
