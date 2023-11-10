using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSlicedLoot : MonoBehaviour
{
    [SerializeField] private float rotation;

    void Start()
    {
        //this.transform.Rotate(Vector3.forward, rotation * Time.deltaTime);
        var body = GetComponent<Rigidbody2D>();
        var impulse = rotation * Mathf.Deg2Rad * body.inertia;

        body.AddTorque(impulse, ForceMode2D.Impulse);
    }
    /*
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    */
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GameOver")
        {
            Destroy(gameObject);
        }
    }
}
