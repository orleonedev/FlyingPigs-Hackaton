using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MarkChat", menuName ="ChatEvents/MarkChat")]
public class MarkChatEvents : CharacterChatEvents<MarkChatEvents>
{
    [SerializeField]
    public List<ChatEvent> specificChatEvents;
    [SerializeField]
    public List<ChatEvent> previousSpecificChatEvents;

    public override void ResetPreviousList() {
        previousEvents = new List<ChatEvent>();
        previousSpecificChatEvents = new List<ChatEvent>();
    }
}