using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayScriptableObjectValues<MyScriptableObject>: MonoBehaviour where MyScriptableObject: ScriptableObject //, IOnValuesChanged 
{
    [SerializeField]
    public MyScriptableObject myScriptableObject;
    

    // Update the UI with the values from the ScriptableObject
    protected virtual void UpdateUI()
    {
       
    }

    // This method can be called when the values in the ScriptableObject change
    public void OnValuesChanged()
    {
        UpdateUI();
    }
}
