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
        float money = myScriptableObject.RealMoney/100f;
        this.realMoneyText.text = money.ToString()+"K"; //Moltiplicatore
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
