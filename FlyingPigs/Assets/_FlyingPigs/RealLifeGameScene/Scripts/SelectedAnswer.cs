using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectedAnswer : MonoBehaviour
{
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
}
