public class EntranceState : IEnemyState
{
  private ArenaSealProgress sealProgress;
  private BossFSMController bossFSM;
  public void Update()
  {

  }

  public void Enter()
  {

  }
  public void Exit()
  {

  }

  public EntranceState(ArenaSealProgress sealProgress, BossFSMController bossFSM)
  {
    this.sealProgress = sealProgress;
    this.bossFSM = bossFSM;
  }


}