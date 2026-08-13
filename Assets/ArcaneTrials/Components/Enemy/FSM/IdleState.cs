public class IdleState : IEnemyState
{
  private EnemyTargetController enemyTarget;
  private EnemyFSMController enemyFSM;
  public void Enter()
  {
    enemyTarget.OnTargetClear += HandleSwitchChaseState;
  }
  public void Update()
  {

  }
  public void Exit()
  {
    enemyTarget.OnTargetClear -= HandleSwitchChaseState;
  }
  public IdleState(EnemyFSMController enemyFSM, EnemyTargetController enemyTarget)
  {
    this.enemyFSM = enemyFSM;
    this.enemyTarget = enemyTarget;
  }

  public void HandleSwitchChaseState()
  {
    enemyFSM.SwitchState(enemyFSM.chaseState);
  }
}