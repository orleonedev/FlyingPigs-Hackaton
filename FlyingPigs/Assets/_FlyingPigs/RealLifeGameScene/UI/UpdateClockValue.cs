using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateClockValue : DisplayScriptableObjectValues<GameStats>
{
    [SerializeField]
    public TMP_Text clockText;
    
    void Start()
    {
        myScriptableObject.OnValuesChanged += OnValuesChanged;
        this.UpdateUI();
    }
    override protected void UpdateUI() {

        if (clockText != null) {
            string hours = myScriptableObject.CurrentHours.ToString("00");
            string minutes = myScriptableObject.CurrentMinutes.ToString("00");
            this.clockText.text = hours+":"+minutes;
        }
        
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
