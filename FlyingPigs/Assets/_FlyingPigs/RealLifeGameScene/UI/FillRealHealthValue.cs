using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillRealHealthValue : DisplayScriptableObjectValues<GameStats>
{
    [SerializeField]
    public Image realHealthImage;
    
    void Start()
    {
        myScriptableObject.OnValuesChanged += OnValuesChanged;
        this.UpdateUI();
    }
    override protected void UpdateUI() {
        if(realHealthImage != null) {
            this.realHealthImage.fillAmount = myScriptableObject.RealHealth;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
