using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton : MonoBehaviour
{

    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject mainCanva;
    [SerializeField] private GameObject creditsButton;
    [SerializeField] private GameObject backButton;



    public void showCredits() {

        mainCanva.SetActive(false);
        credits.SetActive(true);
        creditsButton.SetActive(false);
        backButton.SetActive(true);

    }

    public void unshowCredits() {

        mainCanva.SetActive(true);
        credits.SetActive(false);
        creditsButton.SetActive(true);
        backButton.SetActive(false);


    }

    void Start()
    {
        
    }

}
