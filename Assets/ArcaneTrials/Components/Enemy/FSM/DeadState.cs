using UnityEngine;

public class DeadState : IEnemyState
{
  private EnemyFSMController enemyFSM;
  private EnemyMovement movement;
  private EnemyAnimationsController enemyAnimations;
  public void Enter()
  {
    enemyAnimations.PlayDeadAnimation();
    movement.StopMove();
    enemyFSM.enabled = false;
  }
  public void Update()
  {

  }
  public void Exit()
  {

  }

  public DeadState(EnemyMovement movement, EnemyFSMController enemyFSM, EnemyAnimationsController enemyAnimations)
  {
    this.enemyFSM = enemyFSM;
    this.movement = movement;
    this.enemyAnimations = enemyAnimations;
  }
}