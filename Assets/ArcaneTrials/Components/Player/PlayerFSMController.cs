using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerFSMController : MonoBehaviour
{
  [ShowInInspector, ReadOnly]
  private string CurrentState => currentState?.GetType().Name ?? "None";
  private IPlayerState currentState;
  private PlayerInputController playerInputController;
  private PlayerComponent playerComponent;
  private PlayerAttackManager playerAttackManager;

  void Awake()
  {
    playerInputController = GetComponent<PlayerInputController>();
    playerComponent = GetComponent<PlayerComponent>();
    playerAttackManager = GetComponent<PlayerAttackManager>();
  }
  private void OnEnable()
  {
    playerInputController.OnAttack += HandleAttackAction;
    GameStateController.Instance.OnGameStateChanged += HandleGameStateChange;
  }

  private void OnDisable()
  {
    playerInputController.OnAttack -= HandleAttackAction;
    GameStateController.Instance.OnGameStateChanged -= HandleGameStateChange;
  }


  public void InitState(IPlayerState locomotionState)
  {
    SwitchState(locomotionState);
  }
  void Update()
  {
    currentState.Update();
  }

  public void SwitchState(IPlayerState newState)
  {

    if (currentState == newState) return;
    currentState?.Exit();
    currentState = newState;
    currentState?.Enter();
  }

  private void HandleAttackAction()
  {
    if (playerAttackManager.attack.CanAttack())
    {
      SwitchState(playerComponent.Combat);
    }
    else
    {
      Debug.Log("You can`t attack now");
      return;
    }

  }
  private void HandleGameStateChange(GameState state)
  {
    bool newState = state == GameState.Gameplay;

    enabled = newState;


  }
}