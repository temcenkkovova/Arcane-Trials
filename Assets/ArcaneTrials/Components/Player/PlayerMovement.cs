using System;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
  private PlayerStats playerStats;
  private CharacterController characterController;
  public float CurrentSpeedPercent { get; private set; }
  public Vector3 CurrentInverseTransformDirection { get; private set; }
  public Vector3 CurrentMoveDirection { get; private set; }
  public event Action<Vector3> OnMoveDirectionChanged;
  public void Init(PlayerStats stats) => playerStats = stats;
  void Start() => characterController = GetComponent<CharacterController>();
  public void Move(Vector2 input)
  {
    if (characterController == null || playerStats == null) return;
    Vector3 direction = new Vector3(input.x, 0f, input.y);
    if (direction.sqrMagnitude > 0.01f) direction.Normalize();
    CurrentMoveDirection = direction;
    CurrentInverseTransformDirection = transform.InverseTransformDirection(direction);
    OnMoveDirectionChanged?.Invoke(CurrentInverseTransformDirection);
    characterController.Move(direction * playerStats.MoveSpeed * Time.deltaTime);
    CurrentSpeedPercent = direction.sqrMagnitude > 0.01f ? 1f : 0f;
  }
}
