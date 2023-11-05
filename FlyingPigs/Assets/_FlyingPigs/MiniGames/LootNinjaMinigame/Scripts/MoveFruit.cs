using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFruit : MonoBehaviour
{

    [SerializeField] public float minXSpeed, maxXSpeed, minYSpeed, maxYSpeed;
    [SerializeField] private float destroyTime;
    private SpawnFruit spawnFruit;
    public bool hasBeenCut = false;

    void Start()
    {
        spawnFruit = FindFirstObjectByType<SpawnFruit>();
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(minXSpeed, maxXSpeed), Random.Range(minYSpeed, maxYSpeed));
        Destroy(this.gameObject, this.destroyTime);
    }

    void OnBecameInvisible()
    {
        Debug.Log("aaaaaa");
        if(!hasBeenCut) {
            spawnFruit.GameOver();
        }
        Destroy(gameObject);
    }

}
