using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWall : MonoBehaviour
{
    [SerializeField] private GameObject leftW;
    [SerializeField] private GameObject rightW;
    [SerializeField] private GameObject upW;

    [SerializeField] private Camera gameCamera;

    // Start is called before the first frame update
    void Start()
    {
        float altezzaCamera = gameCamera.orthographicSize * 2f;
        float larghezzaCamera = altezzaCamera * gameCamera.aspect;

        Debug.Log(altezzaCamera);
        Debug.Log(larghezzaCamera);

        GameObject leftWall = Instantiate(this.leftW);
        leftWall.transform.position = new Vector2(-(larghezzaCamera / 2), 0);

        GameObject rightWall = Instantiate(this.rightW);
        rightWall.transform.position = new Vector2(larghezzaCamera / 2, 0);

        GameObject upWall = Instantiate(this.upW);
        upWall.transform.position = new Vector2(0, altezzaCamera / 2);
    }

}
