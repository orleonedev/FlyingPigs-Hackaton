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

    private readonly GameStatisticsManager StatsManager = GameStatisticsManager.Instance;

    public ChatEventWithSender PickGregTutorial() {

         List<ChatEvent> tutorialList = GregChatEvents.Instance.tutorialChatEvents;

        ChatEventWithSender chatEventWithSender = new ChatEventWithSender
        {
            SenderName = "Greg",
            chatEvent = tutorialList.Except(GregChatEvents.Instance.previousTutorialEvents).ToList().First()
        };
        GregChatEvents.Instance.previousTutorialEvents.Add(chatEventWithSender.chatEvent);
        return chatEventWithSender;
    }

    public ChatEventWithSender PickChatEvent() {

        List<Characters> availables = new List<Characters>();

        List<ChatEvent> gregList = ListWithRequirementsMet( Filter(GregChatEvents.Instance));
        if (gregList.Count > 0) {
            availables.Add(Characters.Greg);
        } else {
            ResetPreviousList(GregChatEvents.Instance);
            gregList = ListWithRequirementsMet( Filter(GregChatEvents.Instance));
            if (gregList.Count > 0) availables.Add(Characters.Greg);
        }
        List<ChatEvent> sarahList = ListWithRequirementsMet( Filter(SarahChatEvents.Instance));
        if (sarahList.Count > 0) {
            availables.Add(Characters.Sarah);
        }else {
            ResetPreviousList(SarahChatEvents.Instance);
            sarahList = ListWithRequirementsMet( Filter(SarahChatEvents.Instance));
            if (sarahList.Count > 0) availables.Add(Characters.Sarah);
        }
        List<ChatEvent> evelynList = ListWithRequirementsMet( Filter(EvelyneChatEvents.Instance));
        if (evelynList.Count > 0) {
            availables.Add(Characters.Evelyne);
        }else {
            ResetPreviousList(EvelyneChatEvents.Instance);
            evelynList = ListWithRequirementsMet( Filter(EvelyneChatEvents.Instance));
            if (evelynList.Count > 0) availables.Add(Characters.Evelyne);
        }
        List<ChatEvent> EmployerList = ListWithRequirementsMet( Filter(EmployerChatEvents.Instance));
        if (EmployerList.Count > 0) {
            availables.Add(Characters.Josh);
        }else {
            ResetPreviousList(EmployerChatEvents.Instance);
            EmployerList = ListWithRequirementsMet( Filter(EmployerChatEvents.Instance));
            if (EmployerList.Count > 0) availables.Add(Characters.Josh);
        }
        List<ChatEvent> markList = ListWithRequirementsMet( Filter(MarkChatEvents.Instance));
        if (markList.Count > 0) {
            availables.Add(Characters.Mark);
        }else {
            ResetPreviousList(MarkChatEvents.Instance);
            markList = ListWithRequirementsMet( Filter(MarkChatEvents.Instance));
            if (markList.Count > 0) availables.Add(Characters.Mark);
        }
        List<ChatEvent> billList = ListWithRequirementsMet( Filter(BillChatEvents.Instance));
        if (billList.Count > 0) {
            availables.Add(Characters.Bill);
        }else {
            ResetPreviousList(BillChatEvents.Instance);
            billList = ListWithRequirementsMet( Filter(BillChatEvents.Instance));
            if (billList.Count > 0) availables.Add(Characters.Bill);
        }

        ChatEventWithSender chatEventWithSender = new ChatEventWithSender();
        switch(GetRandomCharacterFromAvailable(availables)){
            case Characters.Greg:
            chatEventWithSender.SenderName = "Greg";
            chatEventWithSender.chatEvent = gregList[UnityEngine.Random.Range(0, gregList.Count)];
            AddToPreviousList(GregChatEvents.Instance, chatEventWithSender.chatEvent);
            break;
            case Characters.Sarah:
            chatEventWithSender.SenderName = "Sarah";
            chatEventWithSender.chatEvent = sarahList[UnityEngine.Random.Range(0, sarahList.Count)];
            AddToPreviousList(SarahChatEvents.Instance, chatEventWithSender.chatEvent);
            break;
            case Characters.Evelyne:
            chatEventWithSender.SenderName = "Evelyne";
            chatEventWithSender.chatEvent = evelynList[UnityEngine.Random.Range(0, evelynList.Count)];
            AddToPreviousList(EvelyneChatEvents.Instance, chatEventWithSender.chatEvent);
            break;
            case Characters.Josh:
            chatEventWithSender.SenderName = StatsManager.ActualEmployer().ToString();
            chatEventWithSender.chatEvent = EmployerList[UnityEngine.Random.Range(0, EmployerList.Count)];
            AddToPreviousList(EmployerChatEvents.Instance, chatEventWithSender.chatEvent);
            break;
            case Characters.Mark:
            chatEventWithSender.SenderName = "Mark";
            chatEventWithSender.chatEvent = markList[UnityEngine.Random.Range(0, markList.Count)];
            AddToPreviousList(MarkChatEvents.Instance, chatEventWithSender.chatEvent);
            break;
            case Characters.Bill:
            chatEventWithSender.SenderName = "Bill";
            chatEventWithSender.chatEvent = billList[UnityEngine.Random.Range(0, billList.Count)];
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
    public void ResetPreviousList<T>(CharacterChatEvents<T> characterChatEvents) where T: ScriptableObject {
        characterChatEvents.previousEvents.Clear();
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
        var values = Enum.GetValues(typeof(Characters));
        //var random = new System.Random();
        Debug.Log(values.Length);
        return (Characters)values.GetValue(UnityEngine.Random.Range(0, values.Length));
    }

    private Characters GetRandomCharacterFromAvailable(List<Characters> available) {
        return available.ElementAt(UnityEngine.Random.Range(0,available.Count));
    }
}
