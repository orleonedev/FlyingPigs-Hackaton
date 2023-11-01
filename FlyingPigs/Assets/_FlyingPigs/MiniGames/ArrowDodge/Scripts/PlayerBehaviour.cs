using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private ArrowBehaviour arrows;
    [SerializeField] private ImageShow imageShow;
    [SerializeField] private Timer timer;
    private int collisionLevel = 0;

    private void OnCollisionEnter2D(){
        if (collisionLevel != ArrowBehaviour.level) {
            imageShow.SwitchShow(false);
            arrows.speed = 0;
            collisionLevel = ArrowBehaviour.level;
            Invoke("EndGame", 0.5f);
        }
    }

    private void EndGame() {
        arrows.EndGame();
    }
}
