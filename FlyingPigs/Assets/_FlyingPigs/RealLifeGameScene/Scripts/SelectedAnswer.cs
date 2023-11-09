using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public void ResetScrollbar() {
        Scrollbar scrollbar = gameObject.GetComponentInChildren<Scrollbar>();
        if(scrollbar != null) {
            scrollbar.value = 1;
        }
    }
}
