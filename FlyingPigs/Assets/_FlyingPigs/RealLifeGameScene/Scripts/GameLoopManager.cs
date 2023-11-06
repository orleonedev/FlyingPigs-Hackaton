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
    public GameStatisticsManager statsManager;
    public delegate void OnMinigameEventDelegate();
    public OnMinigameEventDelegate OnMinigameEvent;
    public delegate void OnChatEventDelegate();
    public OnChatEventDelegate OnChatEvent;

    private bool loopEnabled = false;
    private bool alive = true;
    private bool eventTriggered = true;

    private bool tickClock = true; 

    private GameEventTypes eventToFire;

    void Awake() {
        statsManager = GameStatisticsManager.Instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        statsManager.OnDepleatedStat += OnDepleatedStat;
        Debug.Log("Time Elapsed: "+statsManager.gameStats.TimeElapsed);
        if (statsManager.gameStats.TimeElapsed == 0) {
            Debug.Log("PREPARE FOR NEXT DAY");
            PrepareForNextDay();
        } else {
            Debug.Log("UPDATE UI");
            statsManager.gameStats.OnValuesChanged?.Invoke();
        }
        SetLoopTo(true);
        Debug.Log("LOOP START");
    }

    // Update is called once per frame
    void Update() {
        if(alive){ 
            if (loopEnabled) {
                statsManager.gameStats.TimeElapsed += Time.deltaTime;

                if (((int)statsManager.gameStats.TimeElapsed) % 2 == 0 && !tickClock) {
                    tickClock = true;
                    // Update the UI
                    Debug.Log("UPDATE THE CLOCK");
                    statsManager.TickClock((uint)2);
                    
                }
                if((int)statsManager.gameStats.TimeElapsed % 2 == 1 && tickClock) {
                    tickClock = false;
                }
                if (statsManager.gameStats.TimeElapsed >= statsManager.gameStats.CurrentDayLenght && loopEnabled) {
                    Debug.Log("STOP DAY");
                    CloseAndRestart();
                    return;
                }

                


                if (((int)statsManager.gameStats.TimeElapsed) % 30 == 0 && !eventTriggered) {
                    eventTriggered = true;
                    Debug.Log("SEND EVENT");
                    if ((int)statsManager.gameStats.TimeElapsed == (int)statsManager.gameStats.CurrentDayLenght -30) {
                        //fire both events
                        OnMinigameEvent?.Invoke();
                        OnChatEvent?.Invoke();
                    } else {
                        //fire one
                        switch (eventToFire) {
                            case GameEventTypes.MinigameEvent:
                            Debug.Log("FIRE GAME");
                            OnMinigameEvent?.Invoke();
                            eventToFire = GameEventTypes.ChatEvent;
                            break;
                            case GameEventTypes.ChatEvent:
                            Debug.Log("FIRE CHAT");
                            OnChatEvent?.Invoke();
                            eventToFire = GameEventTypes.MinigameEvent;
                            break;
                        }
                        

                    }
                }
                if((int)statsManager.gameStats.TimeElapsed % 30 == 1 && eventTriggered) {
                    eventTriggered = false;
                }
        }
    }
    }

    public void PrepareForNextDay() {
        
        statsManager.gameStats.CurrentDayLenght = statsManager.gameStats.NextPlayTime + statsManager.gameStats.ModifierPlayTime;
        float hoursDifference = statsManager.gameStats.CurrentDayLenght/60f;
        uint newCurrentHours = (uint)(23.5f-hoursDifference);
        uint newCurrentMinutes = (uint)Math.Floor(statsManager.gameStats.CurrentDayLenght%60);
        statsManager.SetClockTo(newCurrentHours,newCurrentMinutes);
        statsManager.SetNextDay();
        statsManager.gameStats.TimeElapsed = 0f;
        eventToFire = GetRandomEventType();
        statsManager.updateStatsWith(statsManager.fixedUpdates);
        MinigamesList.Instance.PlayedGamesOfTheDay = new List<string>();
        statsManager.gameStats.ModifierPlayTime = 0;
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
