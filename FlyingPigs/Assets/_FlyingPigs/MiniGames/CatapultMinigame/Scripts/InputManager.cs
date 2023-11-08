using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    private InputAction pressAction;
    [SerializeField] private BarBehaviour bar;

    private void Awake() {
        playerInput.SwitchCurrentActionMap("CatapultMinigame");
        pressAction = playerInput.actions.FindAction("TouchInput");
    }

    private void OnEnable() {
        pressAction.performed += TouchPressed;
    }

    private void OnDisable() {
        pressAction.performed -= TouchPressed;
    }

    private void TouchPressed(InputAction.CallbackContext context) {
        bar.OnPress();
    }
}
