using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClickableObject : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private MovingPig pig;
    [SerializeField] private GameObject audioManagerObject;
    [SerializeField] private AudioManager audioManager;

    private void Start(){
        audioManagerObject = GameObject.Find("AudioManager");
        audioManager = audioManagerObject.GetComponent<AudioManager>();
    }

    private void Update()
    {
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
                
            if(touch.phase == TouchPhase.Began && pig.GetIsAlive()){
                CheckTouch(touch.position);
            }
        }
    }

    private void CheckTouch(Vector3 pos){
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        Collider2D hit = Physics2D.OverlapPoint(touchPos);
     
        if(hit && hit == pig.GetComponent<Collider2D>())
        {
            animator.SetBool("isDead", true); 
            // after this, in the animator moving pig functions for changing directions are called
        } 

        audioManager.PlaySound(audioManager.shootClip);
    }
}
