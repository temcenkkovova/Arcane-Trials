using UnityEngine;

public class ChaseState : IEnemyState
{
  private EnemyTargetController enemyTarget;
  private EnemyFSMController enemyFSM;
  private EnemyAnimationsController enemyAnimationsController;
  private EnemyBootstrap enemyBootstrap;
  public void Enter()
  {
    enemyAnimationsController.PlayRunAnimation(true);
    enemyTarget.OnTargetClear += HandleSwitchIdleState;
  }
  public void Update()
  {
    Debug.Log("Chase");
  }
  public void Exit()
  {
    enemyTarget.OnTargetClear -= HandleSwitchIdleState;
    enemyAnimationsController.PlayRunAnimation(false);
  }

  public ChaseState(EnemyFSMController enemyFSM, EnemyTargetController enemyTarget, EnemyAnimationsController enemyAnimationsController, EnemyBootstrap enemyBootstrap)
  {
    this.enemyFSM = enemyFSM;
    this.enemyTarget = enemyTarget;
    this.enemyAnimationsController = enemyAnimationsController;
    this.enemyBootstrap = enemyBootstrap;
  }

  public void HandleSwitchIdleState()
  {
    enemyFSM.SwitchState(enemyBootstrap.Idle);
  }
}