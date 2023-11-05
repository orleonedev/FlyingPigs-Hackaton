using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    [SerializeField] Coordinator coordinator;

    public void EndGame(){
        coordinator.LoadScene("MainScene");
    }
}
