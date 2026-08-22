
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
  private EnemyTargetController enemyTarget;
  private EnemyMovement movement;
  private EnemyAnimationsController enemyAnimations;
  private EnemyAttackManager enemyAttackManager;
  private EnemyDeathFeedback enemyDeathFeedback;
  private ArenaSealProgress arenaProgress;
  private EnemyEntryState enemyEntry;
  private EnemySpawnArena enemySpawnArena;

  void Awake()
  {
    enemyHealth = GetComponent<EnemyHealth>();
    enemyFSM = GetComponent<EnemyFSMController>();

    movement = GetComponent<EnemyMovement>();
    enemyAnimations = GetComponent<EnemyAnimationsController>();
    enemyAttackManager = GetComponent<EnemyAttackManager>();
    enemyDeathFeedback = GetComponent<EnemyDeathFeedback>();
  }

  void Start()
  {
    chaseState = new ChaseState(enemyFSM, enemyTarget, enemyAnimations, this, movement);
    idleState = new IdleState(enemyFSM, enemyTarget, enemyAnimations, this);
    deadState = new DeadState(movement, enemyFSM, enemyAnimations, enemyDeathFeedback);
    attackState = new AttackState(movement, enemyFSM, enemyAnimations, enemyTarget, this, enemyAttackManager);
    enemyFSM.Init(chaseState, enemyTarget);
    movement.Init(config.moveSpeed, enemyTarget);
  }

  public void Init(EnemyConfig config, EnemyTargetController enemyTarget, ArenaSealProgress arenaProgress, EnemyEntryState enemyEntry, EnemySpawnArena enemySpawnArena)
  {
    this.config = config;
    enemyHealth.Init(config.health);
    this.enemyTarget = enemyTarget;
    this.arenaProgress = arenaProgress;
    this.enemyEntry = enemyEntry;
    this.enemySpawnArena = enemySpawnArena;
    enemyHealth.OnDead += HandleEnemyDeath;
  }

  private void HandleEnemyDeath()
  {
    if (arenaProgress == null) return;
    arenaProgress.AddKillToProgress();
    enemyEntry.RegisterDeath();
    enemySpawnArena.HandleEnemyDeath();
  }
  void OnDisable()
  {
    if (arenaProgress == null) return;
    enemyHealth.OnDead -= HandleEnemyDeath;
  }
}


