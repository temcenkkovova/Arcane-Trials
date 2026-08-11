using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputController : MonoBehaviour
{
  public GameInputActions inputActions;
  private PlayerMovement playerMovement;

  public Vector2 MoveInput { get; private set; }
  public Vector2 MousePosition { get; private set; }
  public event Action OnRoll;

  private void Awake()
  {
    inputActions = new GameInputActions();
    playerMovement = GetComponent<PlayerMovement>();
  }

  void Update()
  {
    if (playerMovement == null || inputActions == null) return;
    MoveInput = inputActions.Player.Move.ReadValue<Vector2>();
    MousePosition = Mouse.current.position.ReadValue();
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
  public void OnRollStarted(InputAction.CallbackContext context)
  {
    OnRoll?.Invoke();

  }
  private void OnEnable()
  {
    inputActions.Player.Enable();
    inputActions.Player.Sprint.started += OnSprintStarted;
    inputActions.Player.Sprint.canceled += OnSprintCanceled;
    inputActions.Player.Roll.performed += OnRollStarted;
  }

  private void OnDisable()
  {

    inputActions.Player.Sprint.started -= OnSprintStarted;
    inputActions.Player.Sprint.canceled -= OnSprintCanceled;
    inputActions.Player.Roll.performed -= OnRollStarted;
    inputActions.Player.Disable();
  }
}