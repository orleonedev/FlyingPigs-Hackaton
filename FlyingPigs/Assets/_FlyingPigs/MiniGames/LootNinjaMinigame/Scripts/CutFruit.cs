using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutFruit : MonoBehaviour
{

    [SerializeField] private GameObject fruitRight;
    [SerializeField] private GameObject fruitLeft;
    [SerializeField] private Sprite[] slicedRightObjectSprites;
    [SerializeField] private Sprite[] slicedLeftObjectSprites;
    public int spriteIndex;

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
        newObjectRight.transform.localScale = transform.localScale;
        newObjectRight.transform.position = new Vector3 (this.transform.position.x + 0.1f, this.transform.position.y, this.transform.position.z);
        Sprite objectSpriteRight = slicedRightObjectSprites[spriteIndex];
        newObjectRight.GetComponent<SpriteRenderer>().sprite = objectSpriteRight;
        newObjectRight.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity * 0.5f;

        newObjectLeft = Instantiate(this.fruitLeft);
        newObjectLeft.transform.localScale = transform.localScale;
        newObjectLeft.transform.position = new Vector3 (this.transform.position.x -0.1f, this.transform.position.y, this.transform.position.z);
        Sprite objectSpriteLeft = slicedLeftObjectSprites[spriteIndex];
        newObjectLeft.GetComponent<SpriteRenderer>().sprite = objectSpriteLeft;
        newObjectLeft.GetComponent<Rigidbody2D>().velocity = 
        new Vector2(-this.GetComponent<Rigidbody2D>().velocity.x, this.GetComponent<Rigidbody2D>().velocity.y) * 0.5f;

    }

}
