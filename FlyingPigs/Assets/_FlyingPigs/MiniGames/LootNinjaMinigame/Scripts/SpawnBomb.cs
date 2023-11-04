using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBomb : MonoBehaviour
{
    [SerializeField] private GameObject fruitToSpawn;
    [SerializeField] private float spawnInterval, objectMinX, objectMaxX, objectY;
    //[SerializeField] private Sprite[] objectSprites;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnObject", this.spawnInterval, this.spawnInterval);
    }

    private void spawnObject()
    {
        GameObject newObject = Instantiate(this.fruitToSpawn);
        newObject.transform.position = new Vector2(Random.Range(this.objectMinX, this.objectMaxX), this.objectY);
        //Sprite objectSprite = objectSprites[Random.Range(0, this.objectSprites.Length)];
        //newObject.GetComponent<SpriteRenderer>().sprite = objectSprite;
    }

}
