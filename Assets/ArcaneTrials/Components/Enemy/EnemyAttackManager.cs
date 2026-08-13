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

  // I can add an bool state for checking is enemy attacking now , If it is attacking I can`t switch FSM state on chasing or idle
}