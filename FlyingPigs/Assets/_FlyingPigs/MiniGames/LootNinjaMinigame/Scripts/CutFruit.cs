using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutFruit : MonoBehaviour
{

    [SerializeField] private GameObject fruitRight;
    [SerializeField] private GameObject fruitLeft;
    [SerializeField] private Sprite[] slicedRightObjectSprites;
    [SerializeField] private Sprite[] slicedLeftObjectSprites;

    private GameObject newObjectRight;
    private GameObject newObjectLeft;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cut"))
        {
            spawnSlicedObjects();
            Destroy(this.gameObject);
        }
    }

    private void spawnSlicedObjects()
    {
        newObjectRight = Instantiate(this.fruitRight);

        newObjectRight.transform.position = this.transform.position;
        Sprite objectSpriteRight = slicedRightObjectSprites[SpawnFruit.spriteIndex];
        newObjectRight.GetComponent<SpriteRenderer>().sprite = objectSpriteRight;
        newObjectRight.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity;

        newObjectLeft = Instantiate(this.fruitLeft);

        newObjectLeft.transform.position = this.transform.position;
        Sprite objectSpriteLeft = slicedLeftObjectSprites[SpawnFruit.spriteIndex];
        newObjectLeft.GetComponent<SpriteRenderer>().sprite = objectSpriteLeft;
        newObjectLeft.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity;

    }

}
