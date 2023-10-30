using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction pressAction;

    public BarBehaviour bar;

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
        pressAction = playerInput.actions.FindAction("TouchInput");
    }

    private void OnEnable() {
        pressAction.performed += TouchPressed;
    }

    private void OnDisable() {
        pressAction.performed -= TouchPressed;
    }

    private void TouchPressed(InputAction.CallbackContext context) {
        //float value = context.ReadValue<float>();
        bar.OnPress();
    }
}
