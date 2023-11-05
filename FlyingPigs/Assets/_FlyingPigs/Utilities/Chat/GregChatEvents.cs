using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GregChat", menuName ="ChatEvents/GregChat")]
public class GregChatEvents : CharacterChatEvents //SingletonScriptableObject<GregChatEvents>
{

    // [SerializeField]
    // public string SenderName;
    
    // [SerializeField]
    // public List<ChatEvent> chatEvents;

    // [SerializeField]
    // public List<ChatEvent> previousEvents;

    [SerializeField]
    public List<ChatEvent> tutorialChatEvents;
    [SerializeField]
    public List<ChatEvent> previousTutorialEvents;
}