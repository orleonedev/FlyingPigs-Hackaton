using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManagerArrow : MonoBehaviour
{
    public Transform playerTransform;
    public Camera cam;
    private float animationTimer = 0f;
    private float animationTime = 2f;
    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 value = cam.ScreenToWorldPoint(new Vector3(touch.position.x, 0f, 0f));

            value = new Vector3(value.x, playerTransform.position.y, playerTransform.position.z);

            // Increment the animation timer by the time that has passed since the last frame.
            animationTimer += Time.deltaTime;

            // Calculate the percentage of the animation time that has elapsed.
            float t = animationTimer / animationTime;

            // Use Lerp to smoothly interpolate the game object's position between the start and end positions.
            playerTransform.position = Vector3.Lerp(playerTransform.position, value, t);
        }
        else {
            animationTimer = 0f;
        }
    }
}