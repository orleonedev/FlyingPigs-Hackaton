using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPig: MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float xLeftBound;
    [SerializeField] private float xRightBound;
    [SerializeField] private SpriteRenderer sprite;
    private float angle;
    private Vector3 dir;

    private void Start(){
        angle = GetAngle();

        dir = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0);
    }

    private void Update()
    {
        this.transform.position += dir * speed * Time.deltaTime;
        //dir += new Vector3(0, -0.01f, 0);
        //dir = Vector3.Normalize(dir);

        if(this.transform.position.x < xLeftBound || this.transform.position.x > xRightBound){
            Destroy(this.gameObject);
        }
    }

    private float GetAngle(){
        float angle = 0f;
        
        if(this.transform.position.x <= 0){
            if(this.transform.position.x >= -0.5f && this.transform.position.x <= 0){
                angle = UnityEngine.Random.Range(75.0f, 90.0f);
            } else if(this.transform.position.x >= -1.5 && this.transform.position.x <= -0.5f){
                angle = UnityEngine.Random.Range(60.0f, 65.0f);
            } else {
                angle = 45.0f;
            }
            sprite.flipX = false;
        } else {
            if(this.transform.position.x <= 0.5f && this.transform.position.x > 0){
                angle = UnityEngine.Random.Range(90.0f, 105.0f);
            } else if(this.transform.position.x <= 1.5 && this.transform.position.x > 0.5f){
                angle = UnityEngine.Random.Range(125.0f, 130.0f);
            } else {
                angle = 135.0f;
            }
            sprite.flipX = true;
        }

        return angle;
    }
}
