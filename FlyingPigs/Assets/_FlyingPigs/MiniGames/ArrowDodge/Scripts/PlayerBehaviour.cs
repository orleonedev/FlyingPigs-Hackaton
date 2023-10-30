using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private ArrowBehaviour arrows;
    [SerializeField] private ImageShow imageShow;

    private void OnCollisionEnter2D(){
        Time.timeScale = 0f;
        imageShow.SwitchShow(false);
        Invoke("EndGame", 0.5f);
    }

    private void EndGame() {
        arrows.EndGame();
    }
}
