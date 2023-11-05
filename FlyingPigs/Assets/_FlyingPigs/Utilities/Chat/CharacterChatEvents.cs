using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterChatEvents : SingletonScriptableObject<CharacterChatEvents>
{
    [SerializeField]
    public string SenderName;
    
    [SerializeField]
    public List<ChatEvent> chatEvents;

    [SerializeField]
    public List<ChatEvent> previousEvents;
}
