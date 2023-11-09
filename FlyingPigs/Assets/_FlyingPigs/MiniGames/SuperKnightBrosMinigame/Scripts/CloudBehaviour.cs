using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudBehaviour : MonoBehaviour
{
    private Vector3 dir;
    private float speed;
    private RectTransform rectTransform;

    void Start()
    {
        dir = Vector3.right;
        speed = 125.0f;
        rectTransform = this.GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        if(rectTransform.localPosition.x >= 1800f){
            Destroy(this.gameObject);
        }

        rectTransform.localPosition += dir * speed * Time.deltaTime;
    }
}
