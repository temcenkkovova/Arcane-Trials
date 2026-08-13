public class IdleState : IEnemyState
{
  private EnemyTargetController enemyTarget;
  private EnemyFSMController enemyFSM;
  private EnemyAnimationsController enemyAnimationsController;
  private EnemyBootstrap enemyBootstrap;
  public void Enter()
  {
    enemyTarget.OnTargetClear += HandleSwitchChaseState;
    enemyAnimationsController.PlayIdleAnimation(true);
  }
  public void Update()
  {

  }
  public void Exit()
  {
    enemyTarget.OnTargetClear -= HandleSwitchChaseState;
    enemyAnimationsController.PlayIdleAnimation(false);
  }
  public IdleState(EnemyFSMController enemyFSM, EnemyTargetController enemyTarget, EnemyAnimationsController enemyAnimationsController, EnemyBootstrap enemyBootstrap)
  {
    this.enemyFSM = enemyFSM;
    this.enemyTarget = enemyTarget;
    this.enemyAnimationsController = enemyAnimationsController;
    this.enemyBootstrap = enemyBootstrap;
  }

  public void HandleSwitchChaseState()
  {
    enemyFSM.SwitchState(enemyBootstrap.Chase);
  }
}