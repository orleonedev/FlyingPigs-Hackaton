using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillGameHealthValue : DisplayScriptableObjectValues<GameStats>
{
    [SerializeField]
    public Image gameHealthImage;
    
    void Start()
    {
        myScriptableObject.OnValuesChanged += OnValuesChanged;
        this.UpdateUI();
    }
    override protected void UpdateUI() {
        if (gameHealthImage != null) {
            this.gameHealthImage.fillAmount = myScriptableObject.GameHealth;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
