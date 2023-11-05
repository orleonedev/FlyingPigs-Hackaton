using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChatEvent 
{
   [SerializeField][TextArea(2,10)]
   public string StartingMessage;

   [SerializeField]
   public List<Answers> answers;

   [SerializeField]
   public Answers ghosting;

   [SerializeField]
   public List<StatCondition> statConditions;
   [SerializeField]
   public List<EmployerCondition> employerConditions;


}

[Serializable]
public class Condition {

}

[Serializable]
public class StatCondition : Condition {

    [SerializeField]
    public GameStatsEnum gameStat;
    [SerializeField]
    public StatComparisonType statComparisonType;
    [SerializeField]
    public float value;

}

[Serializable]
public enum StatComparisonType {
        LessOrEqualThan,
        MoreThan,
    }

[Serializable]
public enum EmployerKind {
    John,
    Mark,
    Bill
}

[Serializable]
public class EmployerCondition : Condition {
    
    [SerializeField]
    public EmployerKind neededEmployer;
}