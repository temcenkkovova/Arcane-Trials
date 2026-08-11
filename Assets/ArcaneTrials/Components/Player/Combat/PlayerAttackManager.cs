using UnityEngine;

public class AttackManager : MonoBehaviour
{
  public bool IsAttacking { get; private set; }
  private Attack attack;
  private PlayerAnimationsController playerAnimationsController;

  void Awake()
  {
    attack = GetComponent<Attack>();
    playerAnimationsController = GetComponent<PlayerAnimationsController>();
  }

  void Start()
  {
    playerAnimationsController.OnStartAttack += attack.EnableHitbox;
    playerAnimationsController.OnFinishAttack += attack.DisableHitbox;
  }

  public void ManageAttack()
  {
    if (attack.CanAttack())
    {
      attack.AttackAction();
    }
    else
    {
      Debug.Log("You can`t attack now");
    }
  }

  void OnEnable()
  {
    IsAttacking = false;
  }

  void OnDisable()
  {
    if (attack && playerAnimationsController)
    {
      playerAnimationsController.OnStartAttack -= attack.EnableHitbox;
      playerAnimationsController.OnFinishAttack -= attack.DisableHitbox;
    }

  }
}