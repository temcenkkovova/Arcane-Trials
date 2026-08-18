using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputController : MonoBehaviour
{
  public GameInputActions inputActions;
  public Vector2 MoveInput { get; private set; }
  public Vector2 MousePosition { get; private set; }
  public event Action OnRoll;
  public event Action OnAttack;
  private void Awake() => inputActions = new GameInputActions();
  void Update()
  {
    if (inputActions == null) return;
    MoveInput = inputActions.Player.Move.ReadValue<Vector2>();
    MousePosition = Mouse.current != null ? Mouse.current.position.ReadValue() : Vector2.zero;
  }
  public void OnRollStarted(InputAction.CallbackContext context) => OnRoll?.Invoke();
  public void OnAttackStarted(InputAction.CallbackContext context) => OnAttack?.Invoke();
  private void OnEnable()
  {
    GameStateController.Instance.OnGameStateChanged += HandleGameStateChange;
    inputActions.Player.Enable();
    inputActions.Player.Roll.performed += OnRollStarted;
    inputActions.Player.Attack.performed += OnAttackStarted;
  }
  private void OnDisable()
  {
    GameStateController.Instance.OnGameStateChanged -= HandleGameStateChange;
    inputActions.Player.Roll.performed -= OnRollStarted;
    inputActions.Player.Attack.performed -= OnAttackStarted;
    inputActions.Player.Disable();
  }
  private void HandleGameStateChange(GameState state) => enabled = state == GameState.Gameplay;
}
