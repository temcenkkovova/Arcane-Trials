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
  public event Action OnAttack;

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
  public void OnAttackStarted(InputAction.CallbackContext context)
  {
    OnAttack?.Invoke();

  }
  private void OnEnable()
  {
    GameStateController.Instance.OnGameStateChanged += HandleGameStateChange;
    inputActions.Player.Enable();
    inputActions.Player.Sprint.started += OnSprintStarted;
    inputActions.Player.Sprint.canceled += OnSprintCanceled;
    inputActions.Player.Roll.performed += OnRollStarted;
    inputActions.Player.Attack.performed += OnAttackStarted;
  }

  private void OnDisable()
  {
    GameStateController.Instance.OnGameStateChanged -= HandleGameStateChange;
    inputActions.Player.Sprint.started -= OnSprintStarted;
    inputActions.Player.Sprint.canceled -= OnSprintCanceled;
    inputActions.Player.Roll.performed -= OnRollStarted;
    inputActions.Player.Attack.performed -= OnAttackStarted;
    inputActions.Player.Disable();
  }
  private void HandleGameStateChange(GameState state)
  {

    bool newState = state == GameState.Gameplay;

    enabled = newState;


  }
}