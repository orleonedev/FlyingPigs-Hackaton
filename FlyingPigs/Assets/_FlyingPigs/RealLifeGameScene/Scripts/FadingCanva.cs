using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingCanva : MonoBehaviour
{
    public void ActivateFadingCanva(){
        this.gameObject.SetActive(true);
    }

    public void DeactivateFadingCanva(){
        this.gameObject.SetActive(false);
    }
}
