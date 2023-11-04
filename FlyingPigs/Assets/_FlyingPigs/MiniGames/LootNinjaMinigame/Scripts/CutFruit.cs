using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutFruit : MonoBehaviour
{

    private Rigidbody2D fruitRigidBody;
    private Collider2D fruitCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cut"))
        {
            Destroy(this.gameObject);
        }
    }

}
