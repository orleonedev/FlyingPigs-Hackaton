using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PossibleAnswer : MonoBehaviour
{
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

    }

    public void setIndex(int index) {
        elementIndex = index;
    }
}
