using UnityEngine;

public class BossBootstrap : MonoBehaviour
{

  private BossFSMController bossFSM;
  private DeadState deadState;
  public DeadState Dead => deadState;
  private IdleState idleState;
  public IdleState Idle => idleState;

  private DormantState dormantState;
  public DormantState Dormant => dormantState;
  private EnemyTargetController enemyTarget;
  private ArenaSealProgress arenaSealProgress;

  public void Init(EnemyTargetController enemyTarget, ArenaSealProgress arenaSealProgress)
  {
    this.enemyTarget = enemyTarget;
    this.arenaSealProgress = arenaSealProgress;
  }

  void Awake()
  {
    bossFSM = GetComponent<BossFSMController>();
    dormantState = new DormantState(arenaSealProgress, bossFSM);
  }

  void Start()
  {
    if (bossFSM == null) return;
    bossFSM.Init(Dormant, enemyTarget);
  }


}