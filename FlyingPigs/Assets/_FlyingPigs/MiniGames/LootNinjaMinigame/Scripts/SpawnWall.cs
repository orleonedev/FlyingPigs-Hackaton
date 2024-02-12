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

        float cameraHeight = gameCamera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * gameCamera.aspect;

        GameObject leftWall = Instantiate(this.leftW);
        leftWall.transform.position = new Vector2(-(cameraWidth / 2), 0);

        GameObject rightWall = Instantiate(this.rightW);
        rightWall.transform.position = new Vector2(cameraWidth / 2, 0);

        GameObject upWall = Instantiate(this.upW);
        upWall.transform.position = new Vector2(0, cameraHeight / 2);
    }

}
