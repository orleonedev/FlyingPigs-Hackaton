using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private ArrowBehaviour arrows;
    [SerializeField] private ImageShow imageShow;

    [SerializeField] private Timer timer;

    private void OnCollisionEnter2D(){
        imageShow.SwitchShow(false);
        arrows.speed = 0;
        Invoke("EndGame", 0.5f);
    }

    private void EndGame() {
        arrows.EndGame();
    }
}
