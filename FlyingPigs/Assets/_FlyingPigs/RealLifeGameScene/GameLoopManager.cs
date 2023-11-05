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
    private float dayLength; 

    private GameEventTypes eventToFire;

    void Awake() {
        statsManager = GameStatisticsManager.Instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        statsManager.OnDepleatedStat += OnDepleatedStat;
        if (statsManager.gameStats.TimeElapsed == 0) {
            PrepareForNextDay();
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
                if (statsManager.gameStats.TimeElapsed >= dayLength && loopEnabled) {
                    Debug.Log("STOP DAY");
                    CloseAndRestart();
                    return;
                }

                


                if (((int)statsManager.gameStats.TimeElapsed) % 30 == 0 && !eventTriggered) {
                    eventTriggered = true;
                    Debug.Log("SEND EVENT");
                    if ((int)statsManager.gameStats.TimeElapsed == (int)dayLength -30) {
                        //fire both events
                        OnMinigameEvent?.Invoke();
                        OnChatEvent?.Invoke();
                    } else {
                        //fire one
                        switch (eventToFire) {
                            case GameEventTypes.MinigameEvent:
                            OnMinigameEvent?.Invoke();
                            eventToFire = GameEventTypes.ChatEvent;
                            break;
                            case GameEventTypes.ChatEvent:
                            OnChatEvent.Invoke();
                            eventToFire = GameEventTypes.MinigameEvent;
                            break;
                        }
                        Debug.Log("FIRE");

                    }
                }
                if((int)statsManager.gameStats.TimeElapsed % 30 == 1 && eventTriggered) {
                    eventTriggered = false;
                }
        }
    }
    }

    public void PrepareForNextDay() {
        
        dayLength = statsManager.gameStats.NextPlayTime;
        float hoursDifference = dayLength/60f;
        uint newCurrentHours = (uint)(24f-hoursDifference);
        uint newCurrentMinutes = (uint)Math.Floor(dayLength%60);
        statsManager.SetClockTo(newCurrentHours,newCurrentMinutes);
        statsManager.NextDay();
        statsManager.gameStats.TimeElapsed = 0f;
        eventToFire = GetRandomEventType();
        SerializableDictionary<GameStatsEnum,float> updates = new SerializableDictionary<GameStatsEnum, float>(){
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
