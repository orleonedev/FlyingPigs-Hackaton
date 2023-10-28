using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    public Transform arrows;
    private bool descending = false;
    private float speed;

    void Start()
    {
        Transform[] children = arrows.GetComponentsInChildren<Transform>();
        Destroy(children[UnityEngine.Random.Range(1, children.Count())].gameObject);
        speed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(!descending) {
            if(arrows.position.y > 6) {
                descending = true;
                speed = -speed;
                arrows.transform.localScale = new Vector3(arrows.transform.localScale.x, -arrows.transform.localScale.y, arrows.transform.localScale.z);
            }
        }
        else {
            if(arrows.position.y < -6) {
                //level up
            }
        }
        arrows.transform.position += transform.up * speed * Time.deltaTime;
    }
}
