using UnityEngine;
using System.Collections;

public class EnemyFSMController : MonoBehaviour
{

  private IEnemyState currentState;
  private EnemyBootstrap enemyBootstrap;
  private EnemyHealth enemyHealth;
  public ChaseState chaseState { get; private set; }
  public DeadState deadState { get; private set; }

  void Awake()
  {
    enemyBootstrap = GetComponent<EnemyBootstrap>();
    enemyHealth = GetComponent<EnemyHealth>();
  }

  void OnEnable()
  {

    if (enemyHealth == null) return;
    enemyHealth.OnDead += HandleDeadAction;
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
}