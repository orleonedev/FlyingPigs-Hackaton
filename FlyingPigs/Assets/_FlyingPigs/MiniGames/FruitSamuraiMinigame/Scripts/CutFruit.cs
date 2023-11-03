using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutFruit : MonoBehaviour
{

    private Rigidbody2D fruitRigidBody;
    private Collider2D fruitCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cut"))
        {
            Destroy(this.gameObject);
        }
    }
    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Cut")
        {
            Destroy(this.gameObject);
        }
    }
    */
}
