using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
  private EnemyAnimationsController enemyAnimationsController;
  private Attack attack;
  void Awake()
  {
    enemyAnimationsController = GetComponent<EnemyAnimationsController>();
    attack = GetComponent<Attack>();

  }
  public void ManageAttack()
  {
    if (!attack.CanAttack()) return;
    attack.AttackAction();
    enemyAnimationsController.PlayAttackAnimation();

  }
  public void HandleEnableHitBox()
  {
    attack.EnableHitbox();
  }
  public void HandleDisableHitBox()
  {
    attack.DisableHitbox();
  }

  void OnEnable()
  {
    if (enemyAnimationsController == null) return;
    enemyAnimationsController.OnStartAttack += HandleEnableHitBox;
    enemyAnimationsController.OnFinishAttack += HandleDisableHitBox;
  }
  void OnDisable()
  {
    if (enemyAnimationsController == null) return;
    enemyAnimationsController.OnStartAttack -= HandleEnableHitBox;
    enemyAnimationsController.OnFinishAttack -= HandleDisableHitBox;
  }
  // I can add an bool state for checking is enemy attacking now , If it is attacking I can`t switch FSM state on chasing or idle
}