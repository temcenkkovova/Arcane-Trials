
using UnityEngine;
public class CombatState : IPlayerState
{

  private PlayerAnimationsController playerAnimations;

  private PlayerComponent player;
  private PlayerFSMController playerFSM;
  private PlayerAttackManager attackManager;

  public CombatState(PlayerAnimationsController playerAnimations, PlayerFSMController playerFSM, PlayerComponent player, PlayerInputController playerInput, PlayerAttackManager attackManager)
  {

    this.playerAnimations = playerAnimations;
    this.player = player;
    this.playerFSM = playerFSM;
    this.attackManager = attackManager;
  }
  public void Update()
  {

  }
  public void Enter()
  {
    playerAnimations.OnFinishAttack += FinishAttack;
    TryAttack();
  }
  public void Exit()
  {
    playerAnimations.OnFinishAttack -= FinishAttack;
  }

  private void TryAttack()
  {
    attackManager.ManageAttack();
  }
  private void FinishAttack()
  {

    playerFSM.SwitchState(player.Locomotion);
  }

}