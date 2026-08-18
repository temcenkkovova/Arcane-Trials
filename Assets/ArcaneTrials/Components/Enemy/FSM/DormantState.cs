public class DormantState : IEnemyState
{
  private ArenaSealProgress sealProgress;
  private BossFSMController bossFSM;
  public void Update()
  {
    // I can make playing some sound or animation here
  }

  public void Enter()
  {
    sealProgress.OnCompletedProgress += HandleCompletedSeal;
  }
  public void Exit()
  {
    sealProgress.OnCompletedProgress -= HandleCompletedSeal;
  }

  public DormantState(ArenaSealProgress sealProgress, BossFSMController bossFSM)
  {
    this.sealProgress = sealProgress;
    this.bossFSM = bossFSM;
  }

  private void HandleCompletedSeal()
  {
    bossFSM.SwitchState(bossFSM.entranceState);
  }
}