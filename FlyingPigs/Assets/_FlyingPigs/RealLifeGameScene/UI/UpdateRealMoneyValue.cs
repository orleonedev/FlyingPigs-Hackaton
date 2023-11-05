using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateRealMoneyValue : DisplayScriptableObjectValues<GameStats>
{
    [SerializeField]
    public TMP_Text realMoneyText;
    
    void Start()
    {
        myScriptableObject.OnValuesChanged += OnValuesChanged;
        this.UpdateUI();
    }
    override protected void UpdateUI() {
        if(realMoneyText != null ) {
            float money = myScriptableObject.RealMoney;
            this.realMoneyText.text = money.ToString();
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
