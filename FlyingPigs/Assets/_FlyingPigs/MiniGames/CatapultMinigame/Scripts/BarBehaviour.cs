using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarBehaviour : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Color low;
    [SerializeField] private Color high;
    [SerializeField] private Timer timer;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioManager audioManager;
    private ImageShow imageShow;
    private int level = 1;

    private float count = 0.1f;
    private int rising = 1;

    private bool shouldMove = true;
    private bool lastResult = false;

    void Start() {
        imageShow = GetComponentInChildren<ImageShow>();
        audioManager.PlaySoundLoop(audioManager.catapultLoop);
    }

    public void ResetForNextLevel() {
        //if lastResult == true
        animator.SetTrigger("return");
        count = 0.1f;
        SetValue(count, 100);
        shouldMove = true;
        imageShow.SwitchShow(lastResult);
        timer.RestartTimer();
        //else go to other game scene, minigame ended
    }
    
    private void Update()
    {
        SetValue(count, 100);
        if (count >= 100 || count <= 0) {
            rising = -rising;
        }

        if (shouldMove) {
            count += 0.1f * rising * level / 2;
        }
    }

    public void SetValue(float value, float maxValue) {
        slider.value = value;
        slider.maxValue = maxValue;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, value/maxValue);
    }

    public void OnPress() {
        audioManager.StopSoundLoop();
        audioManager.PlaySound(audioManager.catapultThrow);
        if (shouldMove) {
            shouldMove = false;
            if (count > 80) {
                lastResult = true;
                imageShow.SwitchShow(lastResult);
                level ++;
                animator.SetTrigger("succ");
                Invoke("CastleSound", 0.5f);
            }
            else {
                lastResult = false;
                imageShow.SwitchShow(lastResult);
                level = 1;
                animator.SetTrigger("fail");
                Invoke("SplatSound", 0.3f);
            }
            if (level < 5) {
                Invoke("ResetForNextLevel", 2f);
            }
            else {
                //end minigame
            }
        }
    }

    public void SplatSound() {
        audioManager.PlaySound(audioManager.playerWallImpact);
    }

    public void CastleSound() {
        audioManager.PlaySound(audioManager.castleDestruction);
    }
}
