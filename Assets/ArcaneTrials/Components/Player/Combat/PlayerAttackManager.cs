using System;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
  public bool IsAttacking { get; private set; }
  [NonSerialized] public Attack attack;
  private PlayerAnimationsController playerAnimationsController;
  private PlayerAudio playerAudio;

  void Awake()
  {
    attack = GetComponent<Attack>();
    playerAnimationsController = GetComponent<PlayerAnimationsController>();
    playerAudio = GetComponent<PlayerAudio>();
  }

  void Start()
  {

  }

  public void ManageAttack()
  {

    attack.AttackAction();
    playerAnimationsController.PlayAttackAnimation();
    playerAudio.PlayShootAudio();

  }

  void OnEnable()
  {
    IsAttacking = false;
    if (attack && playerAnimationsController)
      playerAnimationsController.OnStartAttack += attack.EnableHitbox;
    playerAnimationsController.OnFinishAttack += attack.DisableHitbox;
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