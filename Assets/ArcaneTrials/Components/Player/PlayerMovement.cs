using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  private PlayerStats playerStats;
  private CharacterController characterController;
  public float CurrentSpeedPercent { get; private set; }
  public bool isSprinting { get; private set; }
  public event Action<bool> OnSprintChanged;
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

    Vector3 moveDir = new Vector3(input.x, 0, input.y);
    if (characterController == null) return;
    if (moveDir.sqrMagnitude > 1f)
      moveDir.Normalize();
    float currentSpeed = isSprinting ? playerStats.BaseSprintSpeed : playerStats.MoveSpeed;
    characterController.Move(moveDir * currentSpeed * Time.deltaTime);
    CurrentSpeedPercent = moveDir.sqrMagnitude > 0.01f ? (isSprinting ? 1f : 0.5f) : 0f;
    Debug.Log(isSprinting);
  }
  public void ChangeSprintState(bool newSprintState)
  {

    if (isSprinting == newSprintState) return;

    isSprinting = newSprintState;
    OnSprintChanged?.Invoke(isSprinting);
  }
}
