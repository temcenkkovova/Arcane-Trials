
using UnityEngine;

public class EnemyBootstrap : MonoBehaviour
{
  private EnemyConfig config;
  private EnemyHealth enemyHealth;
  private ChaseState chaseState;
  public ChaseState Chase => chaseState;
  private IdleState idleState;
  public IdleState Idle => idleState;
  private DeadState deadState;
  public DeadState Dead => deadState;
  private EnemyFSMController enemyFSM;
  private EnemyTargetController enemyTargetController;
  private EnemyMovement movement;
  private EnemyAnimationsController enemyAnimations;

  void Awake()
  {
    enemyHealth = GetComponent<EnemyHealth>();
    enemyFSM = GetComponent<EnemyFSMController>();
    enemyTargetController = GetComponent<EnemyTargetController>();
    movement = GetComponent<EnemyMovement>();
    enemyAnimations = GetComponent<EnemyAnimationsController>();
  }

  void Start()
  {
    chaseState = new ChaseState();
    idleState = new IdleState(enemyFSM, enemyTargetController);
    deadState = new DeadState(movement, enemyFSM, enemyAnimations);
    enemyFSM.InitDefaultState(chaseState);
  }

  public void Init(EnemyConfig config)
  {
    this.config = config;
    enemyHealth.Init(config.health);
  }
}


