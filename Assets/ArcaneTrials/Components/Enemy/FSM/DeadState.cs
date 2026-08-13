using UnityEngine;

public class DeadState : IEnemyState
{
  private EnemyFSMController enemyFSM;
  private EnemyMovement movement;
  private EnemyAnimationsController enemyAnimations;
  private EnemyDeathFeedback enemyDeathFeedback;
  public void Enter()
  {
    enemyAnimations.PlayDeadAnimation();
    enemyDeathFeedback.Play(() =>
   {
     Object.Destroy(movement.gameObject);
   });
    movement.StopMove();
    enemyFSM.enabled = false;
  }
  public void Update()
  {

  }
  public void Exit()
  {

  }

  public DeadState(EnemyMovement movement, EnemyFSMController enemyFSM, EnemyAnimationsController enemyAnimations, EnemyDeathFeedback enemyDeathFeedback)
  {
    this.enemyFSM = enemyFSM;
    this.movement = movement;
    this.enemyAnimations = enemyAnimations;
    this.enemyDeathFeedback = enemyDeathFeedback;
  }
}