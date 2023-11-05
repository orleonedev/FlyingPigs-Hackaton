using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSlicedLoot : MonoBehaviour
{
    [SerializeField] private float rotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.forward, rotation * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
