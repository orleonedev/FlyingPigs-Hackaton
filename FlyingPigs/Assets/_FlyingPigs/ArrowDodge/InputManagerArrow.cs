using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManagerArrow : MonoBehaviour
{
    public Transform playerTransform;
    public Camera cam;
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 value = cam.ScreenToWorldPoint(new Vector3(touch.position.x, 0f, 0f));

            playerTransform.position = new Vector3(value.x, playerTransform.position.y, playerTransform.position.z);

        }
    }
}
