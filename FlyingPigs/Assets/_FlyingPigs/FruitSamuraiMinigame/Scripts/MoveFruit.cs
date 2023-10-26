using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFruit : MonoBehaviour
{

    [SerializeField] private float minXSpeed, maxXSpeed, minYSpeed, maxYSpeed;
    [SerializeField] private float destroyTime;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(minXSpeed, maxXSpeed), Random.Range(minYSpeed, maxYSpeed));
        Destroy(this.gameObject, this.destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
