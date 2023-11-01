using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateDayValue : DisplayScriptableObjectValues<GameStats>
{
    [SerializeField]
    public TMP_Text dayText;
    
    void Start()
    {
        myScriptableObject.OnValuesChanged += OnValuesChanged;
        this.UpdateUI();
    }
    override protected void UpdateUI() {
        
        this.dayText.text = myScriptableObject.Day.ToString();
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
