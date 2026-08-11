using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  private PlayerStats playerStats;
  private CharacterController characterController;
  public float CurrentSpeedPercent { get; private set; }
  public bool isSprinting { get; private set; }
  public event Action<bool> OnSprintChanged;
  public Vector3 CurrentInverseTransformDirection { get; private set; }
  public event Action<Vector3> OnMoveDirectionChanged;
  public void Init(PlayerStats playerStats)
  {
    this.playerStats = playerStats;
  }

  void Start()
  {
    characterController = GetComponent<CharacterController>();
    isSprinting = false;
  }

  public void Move(Vector2 input)
  {
    if (characterController == null) return;
    Vector3 moveDir = new Vector3(input.x, 0, input.y);
    if (moveDir.sqrMagnitude > 1f)
      moveDir.Normalize();

    CurrentInverseTransformDirection = transform.InverseTransformDirection(moveDir);
    OnMoveDirectionChanged?.Invoke(CurrentInverseTransformDirection);
    float currentSpeed = isSprinting ? playerStats.BaseSprintSpeed : playerStats.MoveSpeed;
    characterController.Move(moveDir * currentSpeed * Time.deltaTime);
    CurrentSpeedPercent = moveDir.sqrMagnitude > 0.01f ? (isSprinting ? 1f : 0.5f) : 0f;

  }
  public void ChangeSprintState(bool newSprintState)
  {

    if (isSprinting == newSprintState) return;

    isSprinting = newSprintState;
    OnSprintChanged?.Invoke(isSprinting);
  }
}
