using UnityEngine;

public class AttackState : IEnemyState
{
  private EnemyFSMController enemyFSM;
  private EnemyMovement movement;
  private EnemyAnimationsController enemyAnimations;
  private EnemyTargetController enemyTarget;
  private EnemyBootstrap enemyBootstrap;
  private float temporaryAttackRange = 1.5f;
  private EnemyAttackManager attackManager;
  public void Enter()
  {

  }
  public void Update()
  {
    if (enemyTarget.targetTr == null) return;
    Vector3 offset = enemyTarget.targetTr.position - enemyBootstrap.transform.position;
    offset.y = 0f;
    float distanceSqr = offset.sqrMagnitude;
    if (distanceSqr > temporaryAttackRange * temporaryAttackRange && !attackManager.isAttacking)
    {
      enemyFSM.SwitchState(enemyBootstrap.Chase);
      return;
    }
    attackManager.ManageAttack();
  }
  public void Exit()
  {

  }

  public AttackState(EnemyMovement movement, EnemyFSMController enemyFSM, EnemyAnimationsController enemyAnimations, EnemyTargetController enemyTarget, EnemyBootstrap enemyBootstrap, EnemyAttackManager attackManager)
  {
    this.enemyFSM = enemyFSM;
    this.movement = movement;
    this.enemyAnimations = enemyAnimations;
    this.enemyTarget = enemyTarget;
    this.enemyBootstrap = enemyBootstrap;
    this.attackManager = attackManager;
  }
}