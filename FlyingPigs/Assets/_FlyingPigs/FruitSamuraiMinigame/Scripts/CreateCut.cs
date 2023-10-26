using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCut : MonoBehaviour
{
    [SerializeField] private GameObject cut;
    [SerializeField] private float cutDestroyTime;

    private bool dragging = false;
    private Vector2 swipeStart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.dragging = true;
            this.swipeStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0) && this.dragging)
        {
            this.createCut();
        }
    }
    private void createCut()
    {
        this.dragging = false;
        Vector2 swipeEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject cut = Instantiate(this.cut, this.swipeStart, Quaternion.identity) as GameObject;
        cut.GetComponent<LineRenderer>().SetPosition(0, this.swipeStart);
        cut.GetComponent<LineRenderer>().SetPosition(1, swipeEnd);
        Vector2[] colliderPoints = new Vector2[2];
        colliderPoints[0] = new Vector2(0.0f, 0.0f);
        colliderPoints[1] = swipeEnd - this.swipeStart;
        cut.GetComponent<EdgeCollider2D>().points = colliderPoints;
        Destroy(cut.gameObject, this.cutDestroyTime);
    }
}
