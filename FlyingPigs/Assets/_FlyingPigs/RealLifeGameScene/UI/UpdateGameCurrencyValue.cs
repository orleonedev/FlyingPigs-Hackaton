using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateGameCurrencyValue : DisplayScriptableObjectValues<GameStats>
{
    [SerializeField]
    public TMP_Text currencyText;
    
    void Start()
    {
        myScriptableObject.OnValuesChanged += OnValuesChanged;
        this.UpdateUI();
    }
    override protected void UpdateUI() {
        float money = myScriptableObject.GameCurrency;
        this.currencyText.text = money.ToString(); //Moltiplicatore
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
