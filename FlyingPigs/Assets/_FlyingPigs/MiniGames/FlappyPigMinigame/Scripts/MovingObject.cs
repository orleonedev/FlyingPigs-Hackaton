using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float xBound;
    [SerializeField] public bool shouldMove = true;

    // Update is called once per frame
    private void Update()
    {
        if(shouldMove) {
            this.transform.position -= Vector3.right * GetDifficultySpeed() * Time.deltaTime;    
        }

        if(this.transform.position.x < xBound){
            Destroy(this.gameObject);
        }
    }

    private float GetDifficultySpeed(){
        return (speed + (PigScript.level - 1) * 0.4f);
    }
}
