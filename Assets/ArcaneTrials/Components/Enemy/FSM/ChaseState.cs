using UnityEngine;

public class ChaseState : IEnemyState
{
  private EnemyTargetController enemyTarget;
  private EnemyFSMController enemyFSM;
  private EnemyAnimationsController enemyAnimationsController;
  private EnemyBootstrap enemyBootstrap;
  private EnemyMovement movement;
  private float temporaryAttackRange = 1.5f;





  public void Enter()
  {
    enemyAnimationsController.PlayRunAnimation(true);
    enemyTarget.OnTargetClear += HandleSwitchIdleState;
  }
  public void Update()
  {

    if (enemyTarget.targetTr == null) return;
    Vector3 offset = enemyTarget.targetTr.position - enemyBootstrap.transform.position;
    offset.y = 0f;
    float distanceSqr = offset.sqrMagnitude;
    if (distanceSqr <= temporaryAttackRange * temporaryAttackRange)
    {
      movement.StopMove();
      enemyFSM.SwitchState(enemyBootstrap.Attack);
      return;
    }
    Vector3 dir = offset.normalized;
    movement.SetDirection(dir);
  }
  public void Exit()
  {
    enemyTarget.OnTargetClear -= HandleSwitchIdleState;
    enemyAnimationsController.PlayRunAnimation(false);
    movement.StopMove();
  }

  public ChaseState(EnemyFSMController enemyFSM, EnemyTargetController enemyTarget, EnemyAnimationsController enemyAnimationsController, EnemyBootstrap enemyBootstrap, EnemyMovement movement)
  {
    this.enemyFSM = enemyFSM;
    this.enemyTarget = enemyTarget;
    this.enemyAnimationsController = enemyAnimationsController;
    this.enemyBootstrap = enemyBootstrap;
    this.movement = movement;

  }

  public void HandleSwitchIdleState()
  {
    enemyFSM.SwitchState(enemyBootstrap.Idle);
  }
}