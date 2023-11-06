using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ChatEventSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject chatNotification; 

    [SerializeField]
    public GameLoopManager gameLoopManager;

    //-------InChatScene Objects --------//
    [SerializeField]
    public TMP_Text SenderLabel;
    [SerializeField]
    public GameObject FirstMessage;
    [SerializeField]
    public GameObject SelectedAnswer;
    [SerializeField]
    public GameObject ResponseMessage;
    [SerializeField]
    public VerticalLayoutGroup PossibleAnswersContainer;

    private PossibleAnswer possibleAnswerPrefab;

    private bool didAnswer = false;
    private bool ghosted = false;
    private ChatEventWithSender newChatEvent;

    private float timelapse;
    private bool updateEnable = false;
    // Start is called before the first frame update
    void Start()
    {
        gameLoopManager.OnChatEvent += OnChatEvent;

    }

    // Update is called once per frame
    void Update()
    {
        if (updateEnable && !didAnswer){
            timelapse -= Time.deltaTime;

            if (timelapse <= 0 && !ghosted) {
                ghosted = true;
                OnGhostedSender();
            }

            if (timelapse <= -10 && ghosted) {
                updateEnable = false;
                chatNotification.SetActive(false);
            }
        }
    }

    public void OnChatEvent() {
        Debug.Log("Create the Chat Notification");

        newChatEvent = ChatEventsManager.Instance.PickChatEvent();
        ResetUI();
        SenderLabel.text = newChatEvent.SenderName;

        chatNotification.SetActive(true);
        timelapse = 30f;
        updateEnable = true;
        didAnswer = false;
        ghosted = false;
    }

    public void SetupChat(){
        FirstMessage.GetComponent<ReceivedMessage>().setText(newChatEvent.chatEvent.StartingMessage);
        foreach (Answers possibleAnswer in newChatEvent.chatEvent.answers)
        {
            PossibleAnswer newObject = Instantiate(possibleAnswerPrefab, PossibleAnswersContainer.transform);
            newObject.setText(possibleAnswer.answerText);
        }
    }

    private void ResetUI() {
        SelectedAnswer.SetActive(false);
        ResponseMessage.SetActive(false);
        RemoveElementsFromPossibleAnswerContainer();
    }

    public void RemoveElementsFromPossibleAnswerContainer()
    {
        for (int i = 0; i < PossibleAnswersContainer.transform.childCount; i++)
        {
            Destroy(PossibleAnswersContainer.transform.GetChild(i).gameObject);
        }
    }

    public void OnAnswerSelected(int index){

        didAnswer = true;
        chatNotification.SetActive(false);
        RemoveElementsFromPossibleAnswerContainer();
        SelectedAnswer.SetActive(true);
        ResponseMessage.SetActive(true);
        SelectedAnswer.GetComponent<SelectedAnswer>().setText(newChatEvent.chatEvent.answers[index].answerText);
        ResponseMessage.GetComponent<ReceivedMessage>().setText(newChatEvent.chatEvent.answers[index].senderResponse);
        GameStatisticsManager.Instance.updateStatsWith(newChatEvent.chatEvent.answers[index].updates);
        
        
    }

    public void OnGhostedSender() {
        SelectedAnswer.SetActive(true);
        ResponseMessage.SetActive(true);
        SelectedAnswer.GetComponent<SelectedAnswer>().setText("...");
        ResponseMessage.GetComponent<ReceivedMessage>().setText(newChatEvent.chatEvent.ghosting.senderResponse);
        GameStatisticsManager.Instance.updateStatsWith(newChatEvent.chatEvent.ghosting.updates);
        


    }
}
