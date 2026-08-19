using UnityEngine;

public class BossAttackState : IEnemyState
{
  private BossAttackManager attackManager;
  private BossFSMController bossFSM;
  private BossBootstrap bossBootstrap;
  private EnemyTargetController enemyTargetController;
  private float temporaryBossAttackRange = 2.1f;
  public void Update()
  {
    if (enemyTargetController == null) return;
    Vector3 offset = enemyTargetController.targetTr.position - bossBootstrap.transform.position;
    offset.y = 0f;
    if (offset.sqrMagnitude > temporaryBossAttackRange * temporaryBossAttackRange && !attackManager.isAttacking)
    {
      SwitchOnChaseState();
      return;
    }
    if (!attackManager.isAttacking && attackManager.CanAttack())
      attackManager.Attack(enemyTargetController.targetTr);

  }
  public void Enter()
  {
    Debug.Log("ATTACK");
  }
  public void Exit()
  {

  }
  public BossAttackState(BossAttackManager attackManager, BossFSMController bossFSM, BossBootstrap bossBootstrap, EnemyTargetController enemyTargetController)
  {
    this.attackManager = attackManager;
    this.bossFSM = bossFSM;
    this.bossBootstrap = bossBootstrap;
    this.enemyTargetController = enemyTargetController;
  }

  public void SwitchOnChaseState()
  {
    bossFSM.SwitchState(bossBootstrap.Chase);
  }
}