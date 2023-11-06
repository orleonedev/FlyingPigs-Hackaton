using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="BillChat", menuName ="ChatEvents/BillChat")]
public class BillChatEvents : CharacterChatEvents<BillChatEvents>
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