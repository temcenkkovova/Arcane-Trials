using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputController : MonoBehaviour
{
  public GameInputActions inputActions;
  private PlayerMovement playerMovement;
  public event Action<Vector2> OnMoveChanged;


  private void Awake()
  {
    inputActions = new GameInputActions();
    playerMovement = GetComponent<PlayerMovement>();
  }

  void Update()
  {
    Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
    if (playerMovement == null || inputActions == null) return;
    if (inputVector.sqrMagnitude > 0.1f)
      playerMovement.Move(inputVector);
    OnMoveChanged?.Invoke(inputVector);



  }


  public void OnSprintStarted(InputAction.CallbackContext context)
  {
    // if (!GameStateController.Instance.IsGameplayState()) return;
    playerMovement.ChangeSprintState(true);
  }
  public void OnSprintCanceled(InputAction.CallbackContext context)
  {

    playerMovement.ChangeSprintState(false);
  }
  private void OnEnable()
  {
    inputActions.Player.Enable();
    inputActions.Player.Sprint.started += OnSprintStarted;
    inputActions.Player.Sprint.canceled += OnSprintCanceled;
  }

  private void OnDisable()
  {
    inputActions.Player.Disable();
    inputActions.Player.Sprint.started -= OnSprintStarted;
    inputActions.Player.Sprint.canceled -= OnSprintCanceled;
  }
}