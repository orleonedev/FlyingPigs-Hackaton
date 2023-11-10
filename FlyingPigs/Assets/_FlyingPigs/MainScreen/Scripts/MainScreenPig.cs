using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreenPig : MonoBehaviour
{
    private Vector3 dir;
    private float speed;
    private RectTransform rectTransform;
    private bool isGoingRight = true;
    private Image pigImage;

    void Start()
    {
        dir = Vector3.right;
        speed = 250.0f;
        rectTransform = this.GetComponent<RectTransform>();
        pigImage = this.GetComponent<Image>();
    }

    void FixedUpdate()
    {
        if(isGoingRight){
            if(rectTransform.localPosition.x >= 1000f){
                dir = Vector3.left;
                isGoingRight = false;
                this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y);
            }
        } else {
            if(rectTransform.localPosition.x <= -1200f){
                dir = Vector3.right;
                isGoingRight = true;
                this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y);
            }
        }

        rectTransform.localPosition += dir * speed * Time.deltaTime;
        rectTransform.localPosition += Vector3.up * Mathf.Sin(rectTransform.localPosition.x) * 400 * Time.deltaTime;
    }

}
