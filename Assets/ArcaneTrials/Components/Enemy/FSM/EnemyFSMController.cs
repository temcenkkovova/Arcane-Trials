using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

public class EnemyFSMController : MonoBehaviour
{
  [ShowInInspector, ReadOnly]
  private string CurrentState => currentState?.GetType().Name ?? "None";
  private IEnemyState currentState;
  private EnemyBootstrap enemyBootstrap;
  private EnemyHealth enemyHealth;
  private EnemyMovement enemyMovement;
  public ChaseState chaseState { get; private set; }
  public DeadState deadState { get; private set; }



  void Awake()
  {
    enemyBootstrap = GetComponent<EnemyBootstrap>();
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

  public void InitDefaultState(IEnemyState defaultState)
  {
    SwitchState(defaultState);
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

  private void HandleGameStateChange(GameState state)
  {
    bool newState = state == GameState.Gameplay;

    enabled = newState;



  }
}