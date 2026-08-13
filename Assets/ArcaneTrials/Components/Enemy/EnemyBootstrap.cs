
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
  private AttackState attackState;
  public AttackState Attack => attackState;
  private EnemyFSMController enemyFSM;
  private EnemyTargetController enemyTargetController;
  private EnemyMovement movement;
  private EnemyAnimationsController enemyAnimations;
  private EnemyAttackManager enemyAttackManager;

  void Awake()
  {
    enemyHealth = GetComponent<EnemyHealth>();
    enemyFSM = GetComponent<EnemyFSMController>();
    enemyTargetController = GetComponent<EnemyTargetController>();
    movement = GetComponent<EnemyMovement>();
    enemyAnimations = GetComponent<EnemyAnimationsController>();
    enemyAttackManager = GetComponent<EnemyAttackManager>();
  }

  void Start()
  {
    chaseState = new ChaseState(enemyFSM, enemyTargetController, enemyAnimations, this, movement);
    idleState = new IdleState(enemyFSM, enemyTargetController, enemyAnimations, this);
    deadState = new DeadState(movement, enemyFSM, enemyAnimations);
    attackState = new AttackState(movement, enemyFSM, enemyAnimations, enemyTargetController, this, enemyAttackManager);
    enemyFSM.InitDefaultState(chaseState);
    movement.Init(config);
  }

  public void Init(EnemyConfig config)
  {
    this.config = config;
    enemyHealth.Init(config.health);
  }
}


