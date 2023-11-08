using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PossibleAnswer : MonoBehaviour
{
    public delegate void OnSelectedAnswerDelegate(int index);
    public OnSelectedAnswerDelegate OnSelectedAnswer;
    private int elementIndex;
    [SerializeField]
    private TMP_Text textComponent;

    void Awake() {
        if (textComponent == null) {
            textComponent = this.gameObject.GetComponentInChildren<TMP_Text>();
        }
        
    }
    
    public void setText(string text) {
        textComponent.text = text;
    }

    public void SelectAnswer() {
        Debug.Log("Tap On Answer");
        OnSelectedAnswer(elementIndex);
    }

    public void setIndex(int index) {
        elementIndex = index;
    }

    public void ResetScrollbar() {
        gameObject.GetComponentInChildren<Scrollbar>().value = 1;
    }
}
