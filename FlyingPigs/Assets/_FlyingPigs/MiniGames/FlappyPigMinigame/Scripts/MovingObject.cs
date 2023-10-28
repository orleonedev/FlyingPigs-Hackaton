using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float xBound;

    // Update is called once per frame
    private void Update()
    {
        this.transform.position -= Vector3.right * speed * Time.deltaTime;

        if(this.transform.position.x < xBound){
            Destroy(this.gameObject);
        }
    }
}
