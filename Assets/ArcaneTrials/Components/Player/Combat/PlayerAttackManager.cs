using System;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
  public bool IsAttacking { get; private set; }
  [NonSerialized] public Attack attack;
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

    attack.AttackAction();
    playerAnimationsController.PlayAttackAnimation();


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