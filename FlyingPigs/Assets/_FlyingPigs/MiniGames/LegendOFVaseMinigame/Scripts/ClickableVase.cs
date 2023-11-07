using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClickableVase : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject vase;
    [SerializeField] private bool isBlue;
    [SerializeField] private GameObject audioManagerObject;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject playAreaObject;
    [SerializeField] private VaseSpawner vaseSpawner;


    private void Start(){
        audioManagerObject = GameObject.Find("AudioManager");
        audioManager = audioManagerObject.GetComponent<AudioManager>();
        playAreaObject = GameObject.Find("PlayArea");
        vaseSpawner = playAreaObject.GetComponent<VaseSpawner>();
    }

    private void Update()
    {
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
                
            if(touch.phase == TouchPhase.Began){
                CheckTouch(touch.position);
            }
        }
    }

    private void CheckTouch(Vector3 pos){
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        Collider2D hit = Physics2D.OverlapPoint(touchPos);
     
        if(hit && hit == vase.GetComponent<Collider2D>())
        {
            //Vase Animation
            if (isBlue)
            {
                Debug.Log ("Tutto ok");
                vaseSpawner.numberOfBlueVases --; 
            }
            else
            {
                Debug.Log("Hai cacato");
            }
            animator.SetBool("isBroken", true); 

        } 

        audioManager.PlaySound(audioManager.shootClip);
    }
    public void BreakVase()
    {
        Destroy(vase);
    }
}
