using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

public class BossFSMController : MonoBehaviour
{
  [ShowInInspector, ReadOnly]
  private string CurrentState => currentState?.GetType().Name ?? "None";
  private IEnemyState currentState;
  private BossBootstrap enemyBootstrap;
  private EnemyHealth enemyHealth;
  private EnemyMovement enemyMovement;
  public ChaseState chaseState { get; private set; }
  public DeadState deadState { get; private set; }
  public EntranceState entranceState { get; private set; }
  private EnemyTargetController targetController;


  void Awake()
  {
    enemyBootstrap = GetComponent<BossBootstrap>();
    enemyHealth = GetComponent<EnemyHealth>();
    enemyMovement = GetComponent<EnemyMovement>();

  }

  void OnEnable()
  {

    if (enemyHealth == null) return;
    enemyHealth.OnDead += HandleDeadAction;
    GameStateController.Instance.OnGameStateChanged += HandleGameStateChange;

  }
  void Update()
  {
    currentState?.Update();
  }
  public void SwitchState(IEnemyState newState)
  {

    if (currentState == newState) return;
    currentState?.Exit();
    currentState = newState;
    currentState?.Enter();
  }

  public void Init(IEnemyState defaultState, EnemyTargetController targetController)
  {
    SwitchState(defaultState);
    this.targetController = targetController;
    targetController.OnTargetClear += HandleClearTarget;
  }


  public void HandleDeadAction()
  {
    SwitchState(enemyBootstrap.Dead);
    StartDestroyCoroutine();
  }

  void OnDisable()
  {
    if (enemyHealth == null) return;
    enemyHealth.OnDead -= HandleDeadAction;
    GameStateController.Instance.OnGameStateChanged -= HandleGameStateChange;
    targetController.OnTargetClear -= HandleClearTarget;
  }

  public void StartDestroyCoroutine()
  {
    StartCoroutine(destroyCoroutine());
  }
  private IEnumerator destroyCoroutine()
  {
    yield return new WaitForSeconds(3f);
    Destroy(gameObject);
  }
  private void HandleClearTarget()
  {
    SwitchState(enemyBootstrap.Idle);
  }
  private void HandleGameStateChange(GameState state)
  {
    bool newState = state == GameState.Gameplay;

    enabled = newState;



  }
}