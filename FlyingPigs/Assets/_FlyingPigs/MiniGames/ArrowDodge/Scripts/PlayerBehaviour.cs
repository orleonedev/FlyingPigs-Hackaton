using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private ArrowBehaviour arrows;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private ImageShow imageShow;
    [SerializeField] private Timer timer;
    [SerializeField] private InputManagerArrow inputManagerArrow;
    private int collisionLevel = 0;

    private void OnCollisionEnter2D(){
        if (collisionLevel != ArrowBehaviour.level) {
            imageShow.SwitchShow(false);
            arrows.speed = 0;
            collisionLevel = ArrowBehaviour.level;
            audioManager.PlaySound(audioManager.playerHit);
            audioManager.PlaySound(audioManager.endMinigameFail);
            inputManagerArrow.canMove = false;
            ArrowBehaviour.timeElapsed += timer.GetTime();
            timer.PauseTimer();
            Invoke("EndGame", 2f);
        }
    }

    private void EndGame() {
        arrows.EndGame();
    }
}
