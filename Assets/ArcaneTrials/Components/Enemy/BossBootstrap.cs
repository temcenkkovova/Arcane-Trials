using UnityEngine;

public class BossBootstrap : MonoBehaviour
{

  private BossFSMController bossFSMController;
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
    bossFSMController = GetComponent<BossFSMController>();
  }

  void Start()
  {
    if (bossFSMController == null) return;
    bossFSMController.Init(Dormant, enemyTarget);
  }


}