using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenPig : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        var dir = Vector3.right;
        var speed = 250.0f;

        if(this.transform.position.x >= 1000f){
            dir = Vector3.left;
        } else if(this.transform.position.x <= -1000f){
            dir = Vector3.right;
        }

        this.transform.position += dir * speed * Time.deltaTime;
    }
}
