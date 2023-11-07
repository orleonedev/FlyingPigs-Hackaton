using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton : MonoBehaviour
{

    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject mainCanva;

    public void showCredits() {

        Debug.Log("test");
        mainCanva.SetActive(false);
        credits.SetActive(true);
    }

    void Start()
    {
        
    }

}
