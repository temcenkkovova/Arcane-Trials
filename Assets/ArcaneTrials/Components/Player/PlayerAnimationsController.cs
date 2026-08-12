using System;
using UnityEngine;

public class PlayerAnimationsController : MonoBehaviour
{
  public Animator animator;

  private PlayerMovement movement;
  private AttackEffects attackEffects;
  public event Action OnFinishDodge;
  public event Action OnFinishAttack;
  public event Action OnStartAttack;

  void Awake()
  {

    movement = GetComponent<PlayerMovement>();

    attackEffects = GetComponent<AttackEffects>();
  }

  void Start()
  {
    if (movement == null) return;
    movement.OnMoveDirectionChanged += HandleMoveChanged;
  }

  private void OnDisable()
  {
    if (movement == null) return;
    movement.OnMoveDirectionChanged -= HandleMoveChanged;
  }

  public void HandleMoveChanged(Vector3 localDirection)
  {

    animator.SetFloat("MoveX", localDirection.x, 0.1f, Time.deltaTime); // For make smooth threshold .
    animator.SetFloat("MoveY", localDirection.z, 0.1f, Time.deltaTime);

  }
  public void HandleStartDodge()
  {
    animator.SetTrigger("Roll");
  }
  public void HandleFinishDodge()
  {

    OnFinishDodge?.Invoke();
  }
  public void PlayAttackAnimation()
  {

    animator.SetTrigger("Attack");

  }

  public void HandleStartAttack()
  {
    OnStartAttack?.Invoke();
  }
  public void HandleFinishAttack()
  {

    OnFinishAttack?.Invoke();
  }
  public void HandlePlaySlashAttack()
  {
    attackEffects.PlayAttackEffect();
  }
}