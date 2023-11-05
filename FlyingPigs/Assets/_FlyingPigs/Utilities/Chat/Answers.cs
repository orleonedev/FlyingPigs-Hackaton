using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Answers
{
   [SerializeField][TextArea(2,10)]
   public string answerText;

   [SerializeField][TextArea(2,10)]
   public string senderResponse;

   [SerializeField]
   public SerializableDictionary<GameStatsEnum,float> updates;
}
