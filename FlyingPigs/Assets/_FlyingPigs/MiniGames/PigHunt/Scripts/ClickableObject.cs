using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClickableObject : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private MovingPig pig;
    public static Action OnPigDestroy;
    private void Update()
    {
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
                
            if(touch.phase == TouchPhase.Began){
                animator.SetBool("Killed", true);
                pig.SetKilled(true);
                OnPigDestroy?.Invoke();
            }
        }
    }
}
