using System;
using UnityEngine;

public class EnemyAnimationsController : MonoBehaviour
{
  private Animator animator;
  public event Action OnFinishAttack;
  public event Action OnStartAttack;

  public event Action OnFinishJump;

  void Awake()
  {
    animator = GetComponent<Animator>();
  }
  public void PlayDeadAnimation()
  {

    animator.SetTrigger("Dead");
  }
  public void PlayIdleAnimation(bool state)
  {

    animator.SetBool("Idle", state);
  }
  public void PlayRunAnimation(bool state)
  {

    animator.SetBool("Run", state);
  }
  public void PlayAttackAnimation()
  {
    animator.SetTrigger("Attack");
  }

  public void PlayJumpAnimation()
  {
    animator.SetTrigger("Jump");
  }

  public void HandleFinishJump()
  {
    OnFinishJump?.Invoke();
  }
}