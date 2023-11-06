using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public enum Characters {
    Greg,
    Bill,
    Josh,
    Mark,
    Sarah,
    Evelyne

}

public class ChatEventWithSender {
    public string SenderName;
    public ChatEvent chatEvent;
}

public class ChatEventsManager
{
    static private ChatEventsManager instance;
    static public ChatEventsManager Instance {
        get {
            instance ??= new ChatEventsManager();
            return instance;
        }
    }

    private ChatEventsManager() {}

    private GameStatisticsManager StatsManager = GameStatisticsManager.Instance;

    public ChatEventWithSender PickChatEvent() {
        
        List<ChatEvent> availableList = new List<ChatEvent>();
        ChatEventWithSender chatEventWithSender = new ChatEventWithSender();
        switch(GetRandomCharacter()){
            case Characters.Greg:
            availableList = ListWithRequirementsMet( Filter(GregChatEvents.Instance));
            chatEventWithSender.SenderName = "Greg";
            chatEventWithSender.chatEvent = availableList[UnityEngine.Random.Range(0, availableList.Count-1)];
            AddToPreviousList(GregChatEvents.Instance, chatEventWithSender.chatEvent);
            break;
            case Characters.Sarah:
            availableList = ListWithRequirementsMet( Filter(SarahChatEvents.Instance));
            chatEventWithSender.SenderName = "Sarah";
            chatEventWithSender.chatEvent = availableList[UnityEngine.Random.Range(0, availableList.Count-1)];
            AddToPreviousList(SarahChatEvents.Instance, chatEventWithSender.chatEvent);
            break;
            case Characters.Evelyne:
            availableList = ListWithRequirementsMet( Filter(EvelyneChatEvents.Instance));
            chatEventWithSender.SenderName = "Evelyne";
            chatEventWithSender.chatEvent = availableList[UnityEngine.Random.Range(0, availableList.Count-1)];
            AddToPreviousList(EvelyneChatEvents.Instance, chatEventWithSender.chatEvent);
            break;
            case Characters.Josh:
            availableList = ListWithRequirementsMet( Filter(EmployerChatEvents.Instance));
            chatEventWithSender.SenderName = "Josh";
            chatEventWithSender.chatEvent = availableList[UnityEngine.Random.Range(0, availableList.Count-1)];
            AddToPreviousList(EmployerChatEvents.Instance, chatEventWithSender.chatEvent);
            break;
            case Characters.Mark:
            availableList = ListWithRequirementsMet( Filter(MarkChatEvents.Instance));
            chatEventWithSender.SenderName = "Mark";
            chatEventWithSender.chatEvent = availableList[UnityEngine.Random.Range(0, availableList.Count-1)];
            AddToPreviousList(MarkChatEvents.Instance, chatEventWithSender.chatEvent);
            break;
            case Characters.Bill:
            availableList = ListWithRequirementsMet( Filter(BillChatEvents.Instance));
            chatEventWithSender.SenderName = "Bill";
            chatEventWithSender.chatEvent = availableList[UnityEngine.Random.Range(0, availableList.Count-1)];
            AddToPreviousList(BillChatEvents.Instance, chatEventWithSender.chatEvent);
            break;
        }

        return chatEventWithSender;
    }

    public List<ChatEvent> Filter<T>(CharacterChatEvents<T> characterChatEvents ) where T: ScriptableObject {

        return characterChatEvents.chatEvents.Except( characterChatEvents.previousEvents).ToList();

    }

    public void AddToPreviousList<T>(CharacterChatEvents<T> characterChatEvents, ChatEvent chatEvent) where T: ScriptableObject {
        characterChatEvents.previousEvents.Add(chatEvent);
    }

    public List<ChatEvent> ListWithRequirementsMet(List<ChatEvent> startingList){
        List<ChatEvent> requirementsMet = new List<ChatEvent>();

        foreach (ChatEvent chatEvent in startingList)
        {
            if (StatConditionsAreMet(chatEvent.statConditions) && EmployerConditionAreMet(chatEvent.employerConditions)) {
                requirementsMet.Add(chatEvent);
            }

        }
        return requirementsMet;
    }

    private bool StatConditionsAreMet(List<StatCondition> conditions) {
        foreach (StatCondition condition in conditions)
        {
            if (!StatsManager.StatIsMet(condition.gameStat, condition.statComparisonType, condition.value)) return false;
        }
        return true;
    }

    private bool EmployerConditionAreMet(List<EmployerCondition> conditions) {
        foreach (EmployerCondition condition in conditions)
        {
            if(condition.neededEmployer != StatsManager.ActualEmployer()) return false;
        }
        return true;
    }

    private List<ChatEvent> AggregateAvailableChatEvents() {

        List<ChatEvent> availableListGreg = ListWithRequirementsMet( Filter(GregChatEvents.Instance));
        List<ChatEvent> availableListSarah = ListWithRequirementsMet( Filter(SarahChatEvents.Instance));
        List<ChatEvent> availableListJohn = ListWithRequirementsMet( Filter(EmployerChatEvents.Instance));
        List<ChatEvent> availableListBill = ListWithRequirementsMet( Filter(BillChatEvents.Instance));
        List<ChatEvent> availableListMark = ListWithRequirementsMet( Filter(MarkChatEvents.Instance));
        List<ChatEvent> availableListEvelyne = ListWithRequirementsMet( Filter(EvelyneChatEvents.Instance));

        List<List<ChatEvent>> allLists = new List<List<ChatEvent>>{availableListGreg,availableListSarah,availableListJohn,availableListBill,availableListMark,availableListEvelyne};

        List<ChatEvent> mergedList = allLists.SelectMany(x => x).ToList();

        return mergedList;

    }

    private Characters GetRandomCharacter() {
        var values = Enum.GetValues(typeof(GameEventTypes));
        var random = new System.Random();
        return (Characters)values.GetValue(random.Next(values.Length));
    }
}
