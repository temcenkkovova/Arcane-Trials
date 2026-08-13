using UnityEngine;

public class EnemyAnimationsController : MonoBehaviour
{
  private Animator animator;

  void Awake()
  {
    animator = GetComponent<Animator>();
  }
  public void PlayDeadAnimation()
  {
    Debug.Log("Dead An");
    animator.SetTrigger("Dead");
  }
  public void PlayIdleAnimation(bool state)
  {
    Debug.Log("Idle an");
    animator.SetBool("Idle", state);
  }
  public void PlayRunAnimation(bool state)
  {
    Debug.Log("run an");
    animator.SetBool("Run", state);
  }
  public void PlayAttackAnimation()
  {
    animator.SetTrigger("Attack");
  }
}