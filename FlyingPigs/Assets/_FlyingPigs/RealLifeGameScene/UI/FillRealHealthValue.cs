using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillRealHealthValue : DisplayScriptableObjectValues<GameStats>
{
    [SerializeField]
    public Image realHealthImage;
    
    override protected void UpdateUI() {
        this.realHealthImage.fillAmount = myScriptableObject.RealHealth;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
