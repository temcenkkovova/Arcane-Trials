using UnityEngine;

public class BossChaseState : IEnemyState
{
  private BossFSMController bossFSM;
  private EnemyTargetController enemyTarget;
  private EnemyAnimationsController enemyAnimationsController;
  private BossBootstrap bossBootstrap;
  private EnemyMovement movement;
  private float temporaryBossAttackRange = 2f;
  public void Update()
  {
    if (enemyTarget.targetTr == null) return;

    Vector3 offset = enemyTarget.targetTr.position - bossBootstrap.transform.position;
    offset.y = 0f;
    if (offset.sqrMagnitude <= temporaryBossAttackRange * temporaryBossAttackRange)
    {
      HandleSwitchOnChaseAttack();
      return;
    }
    Vector3 dir = offset.normalized;
    movement.SetDirection(dir);
  }

  public void Enter()
  {
    enemyAnimationsController.PlayRunAnimation(true);
    enemyTarget.OnTargetClear += HandleSwitchOnIdle;
  }
  public void Exit()
  {
    enemyTarget.OnTargetClear -= HandleSwitchOnIdle;
    enemyAnimationsController.PlayRunAnimation(false);
    movement.StopMove();
  }

  public BossChaseState(BossFSMController bossFSM, EnemyTargetController enemyTarget, EnemyAnimationsController enemyAnimationsController, BossBootstrap bossBootstrap, EnemyMovement movement)
  {
    this.bossFSM = bossFSM;
    this.enemyTarget = enemyTarget;
    this.enemyAnimationsController = enemyAnimationsController;
    this.bossBootstrap = bossBootstrap;
    this.movement = movement;
  }

  private void HandleSwitchOnChaseAttack()
  {
    bossFSM.SwitchState(bossBootstrap.Attack);
  }
  private void HandleSwitchOnIdle()
  {
    bossFSM.SwitchState(bossBootstrap.Idle);
  }
}