using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MarkChat", menuName ="ChatEvents/MarkChat")]
public class MarkChatEvents : EmployerChatEvents
{
    [SerializeField]
    public List<ChatEvent> specificChatEvents;
    [SerializeField]
    public List<ChatEvent> previousSpecificChatEvents;
}