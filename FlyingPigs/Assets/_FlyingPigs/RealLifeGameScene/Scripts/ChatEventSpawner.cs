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
    [SerializeField]
    public GameStateManager GameState;

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
    private bool chatWasSet = false;
    // Start is called before the first frame update
    void Start()
    {
        gameLoopManager.OnChatEvent += OnChatEvent;
        gameLoopManager.OnChatTutorialGhostableEvent += OnChatTutorialGhostableEvent;

    }

    // Update is called once per frame
    void Update()
    {
        if (updateEnable && !didAnswer && GameState.InGameSceneObject.activeInHierarchy){
            timelapse -= (Time.deltaTime*2);

            if (timelapse <= 0 && !ghosted) {
                ghosted = true;
                OnGhostedSender();
            }

            if (timelapse <= -10 && ghosted) {
                updateEnable = false;
                chatNotification.SetActive(false);
                if (GameState.ChatSceneObject.activeInHierarchy) {
                    GameState.SwitchToInGame();
                }
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
        if(!chatWasSet) {
            FirstMessage.GetComponent<ReceivedMessage>().setText(newChatEvent.chatEvent.StartingMessage);

            if (!ghosted) {
                foreach (Answers possibleAnswer in newChatEvent.chatEvent.answers)
                {
                    GameObject newObject = Instantiate(possibleAnswerPrefab, PossibleAnswersContainer.transform);
                    newObject.GetComponent<PossibleAnswer>().setText(possibleAnswer.answerText);
                    newObject.GetComponent<PossibleAnswer>().setIndex(newChatEvent.chatEvent.answers.IndexOf(possibleAnswer));
                    newObject.GetComponent<PossibleAnswer>().OnSelectedAnswer += OnAnswerSelected;
                }
            }
        
        chatWasSet = true;
        }
        
    }

    private void ResetUI() {
        FirstMessage.GetComponentInChildren<ReceivedMessage>(true).ResetScrollbar();
        SelectedAnswer.SetActive(false);
        SelectedAnswer.GetComponentInChildren<SelectedAnswer>(true).ResetScrollbar();
        ResponseMessage.SetActive(false);
        ResponseMessage.GetComponentInChildren<ReceivedMessage>(true).ResetScrollbar();
        RemoveElementsFromPossibleAnswerContainer();
        chatWasSet = false;
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
        //audioManager.PlaySound(audioManager.messageBubbleClip, 2.0f);
        ResponseMessage.SetActive(true);
        audioManager.PlaySound(audioManager.messageNotificationClip, 1.0f);
        SelectedAnswer.GetComponent<SelectedAnswer>().setText(newChatEvent.chatEvent.answers[index].answerText);
        ResponseMessage.GetComponent<ReceivedMessage>().setText(newChatEvent.chatEvent.answers[index].senderResponse);
        GameStatisticsManager.Instance.updateStatsWith(newChatEvent.chatEvent.answers[index].updates);
        
        
    }

    public void OnGhostedSender() {
        RemoveElementsFromPossibleAnswerContainer();
        SelectedAnswer.SetActive(true);
        ResponseMessage.SetActive(true);
        SelectedAnswer.GetComponentInChildren<SelectedAnswer>(true).setText("...");
        ResponseMessage.GetComponentInChildren<ReceivedMessage>(true).setText(newChatEvent.chatEvent.ghosting.senderResponse);
        GameStatisticsManager.Instance.updateStatsWith(newChatEvent.chatEvent.ghosting.updates);
    }

    public void ResetSpawner() {
        updateEnable = false;
        chatNotification.SetActive(false);
        ResetUI();
    }
}
