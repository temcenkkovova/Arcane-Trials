public class EntranceState : IEnemyState
{
  private EnemyMovement movement;
  private BossFSMController bossFSM;
  private BossEntrance bossEntrance;
  private EnemyAnimationsController enemyAnimations;
  public void Update()
  {
    bossEntrance.Entrance();
  }

  public void Enter()
  {
    bossEntrance.OnPosition += HandleSwitchOnChaseState;

    enemyAnimations.PlayRunAnimation(true);
  }
  public void Exit()
  {
    bossEntrance.OnPosition -= HandleSwitchOnChaseState;
    enemyAnimations.PlayRunAnimation(false);
    movement.StopMove();
  }

  public EntranceState(BossFSMController bossFSM, BossEntrance bossEntrance, EnemyMovement movement, EnemyAnimationsController enemyAnimations)
  {
    this.bossFSM = bossFSM;
    this.bossEntrance = bossEntrance;
    this.movement = movement;
    this.enemyAnimations = enemyAnimations;
  }

  private void HandleSwitchOnChaseState()
  {
    bossFSM.SwitchState(bossFSM.chaseState);
  }


}
