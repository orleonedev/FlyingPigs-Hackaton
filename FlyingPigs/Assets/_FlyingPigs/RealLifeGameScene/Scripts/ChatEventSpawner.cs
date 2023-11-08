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
    public GameObject PossibleAnswersContainer;
    [SerializeField]
    private GameObject possibleAnswerPrefab;

    [SerializeField] AudioManager audioManager;

    private bool didAnswer = false;
    private bool ghosted = false;
    private ChatEventWithSender newChatEvent;

    private float timelapse;
    private bool updateEnable = false;
    // Start is called before the first frame update
    void Start()
    {
        gameLoopManager.OnChatEvent += OnChatEvent;
        gameLoopManager.OnChatTutorialGhostableEvent += OnChatTutorialGhostableEvent;

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
                ResetUI();
            }
        }
    }

    public void OnTutorialChatEvent() {
        newChatEvent = ChatEventsManager.Instance.PickGregTutorial();
        ResetUI();
        SenderLabel.text = newChatEvent.SenderName;
    } 

    public void OnChatTutorialGhostableEvent() {
        Debug.Log("Create the Chat Notification");

        newChatEvent = ChatEventsManager.Instance.PickGregTutorial();
        ResetUI();
        SenderLabel.text = newChatEvent.SenderName;

        chatNotification.SetActive(true);
        timelapse = 30f;
        updateEnable = true;
        didAnswer = false;
        ghosted = false;
    }

    public void OnChatEvent() {
        Debug.Log("Create the Chat Notification");

        newChatEvent = ChatEventsManager.Instance.PickChatEvent();
        ResetUI();
        SenderLabel.text = newChatEvent.SenderName;

        audioManager.PlaySound(audioManager.messageNotificationClip, 1.0f);
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
            GameObject newObject = Instantiate(possibleAnswerPrefab, PossibleAnswersContainer.transform);
            newObject.GetComponent<PossibleAnswer>().setText(possibleAnswer.answerText);
            newObject.GetComponent<PossibleAnswer>().setIndex(newChatEvent.chatEvent.answers.IndexOf(possibleAnswer));
            newObject.GetComponent<PossibleAnswer>().OnSelectedAnswer += OnAnswerSelected;
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
        Debug.Log("ON ANSWER SELECTED");
        didAnswer = true;
        chatNotification.SetActive(false);
        RemoveElementsFromPossibleAnswerContainer();
        SelectedAnswer.SetActive(true);
        ResponseMessage.SetActive(true);
        audioManager.PlaySound(audioManager.messageBubbleClip, 1.0f);
        SelectedAnswer.GetComponent<SelectedAnswer>().setText(newChatEvent.chatEvent.answers[index].answerText);
        ResponseMessage.GetComponent<ReceivedMessage>().setText(newChatEvent.chatEvent.answers[index].senderResponse);
        GameStatisticsManager.Instance.updateStatsWith(newChatEvent.chatEvent.answers[index].updates);
        
        
    }

    public void OnGhostedSender() {
        RemoveElementsFromPossibleAnswerContainer();
        SelectedAnswer.SetActive(true);
        ResponseMessage.SetActive(true);
        SelectedAnswer.GetComponent<SelectedAnswer>().setText("...");
        ResponseMessage.GetComponent<ReceivedMessage>().setText(newChatEvent.chatEvent.ghosting.senderResponse);
        GameStatisticsManager.Instance.updateStatsWith(newChatEvent.chatEvent.ghosting.updates);
        


    }
}
