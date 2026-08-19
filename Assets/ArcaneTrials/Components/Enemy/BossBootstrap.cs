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
  private EntranceState entranceState;
  public EntranceState Entrance => entranceState;
  private BossChaseState chaseState;
  public BossChaseState Chase => chaseState;
  private EnemyTargetController enemyTarget;
  private ArenaSealProgress arenaSealProgress;
  private BossEntrance bossEntrance;
  private ArenaBossConfig bossConfig;
  private EnemyMovement movement;
  private EnemyAnimationsController enemyAnimationsController;

  public void Init(EnemyTargetController enemyTarget, ArenaSealProgress arenaSealProgress, ArenaBossConfig bossConfig)
  {
    this.enemyTarget = enemyTarget;
    this.arenaSealProgress = arenaSealProgress;
    this.bossConfig = bossConfig;
  }

  void Awake()
  {
    bossFSM = GetComponent<BossFSMController>();
    bossEntrance = GetComponent<BossEntrance>();
    movement = GetComponent<EnemyMovement>();
    enemyAnimationsController = GetComponent<EnemyAnimationsController>();


  }

  void Start()
  {
    if (bossFSM == null) return;
    dormantState = new DormantState(arenaSealProgress, bossFSM);
    entranceState = new EntranceState(bossFSM, bossEntrance, movement, enemyAnimationsController);
    chaseState = new BossChaseState();
    bossFSM.Init(Dormant, enemyTarget, Chase, Entrance);
    bossEntrance.Init(bossConfig.bossEntrancePosition);
    movement.Init(bossConfig.moveSpeed, enemyTarget);
  }


}