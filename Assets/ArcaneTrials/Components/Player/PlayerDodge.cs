using System;
using UnityEngine;
public class PlayerDodge : MonoBehaviour
{
  private CharacterController characterController;
  [SerializeField] private float dodgeSpeed = 12f;
  [SerializeField] private float dashDuration = 0.2f;
  [SerializeField] private float dashCooldown = 2f;
  private Vector3 dashDirection;
  private float dashTimeRemaining;
  private float cooldownTimeRemaining;
  private bool isDashing;
  public event Action OnFinishDash;
  public bool CanDash => !isDashing && cooldownTimeRemaining <= 0f;
  void Awake()
  {
    characterController = GetComponent<CharacterController>();
  }

  void OnEnable()
  {
    isDashing = false;
    dashTimeRemaining = 0f;
    cooldownTimeRemaining = 0f;
  }
  void Update()
  {
    if (cooldownTimeRemaining > 0f) cooldownTimeRemaining -= Time.deltaTime;
    if (!isDashing || characterController == null) return;
    characterController.Move(dashDirection * dodgeSpeed * Time.deltaTime);
    dashTimeRemaining -= Time.deltaTime;
    if (dashTimeRemaining > 0f) return;
    isDashing = false;
    OnFinishDash?.Invoke();
  }
  public void StartDodge(Vector3 direction)
  {
    if (!CanDash) return;
    dashDirection = direction.sqrMagnitude > 0.01f ? direction.normalized : transform.forward;
    dashTimeRemaining = dashDuration;
    cooldownTimeRemaining = dashCooldown;
    isDashing = true;
  }
}
