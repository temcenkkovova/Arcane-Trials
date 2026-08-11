
using UnityEngine;
public class CombatState : IPlayerState
{

  private PlayerAnimationsController playerAnimations;

  private PlayerComponent player;
  private PlayerFSMController playerFSM;

  public CombatState(PlayerAnimationsController playerAnimations, PlayerFSMController playerFSM, PlayerComponent player, PlayerInputController playerInput)
  {

    this.playerAnimations = playerAnimations;
    this.player = player;
    this.playerFSM = playerFSM;
  }
  public void Update()
  {

  }
  public void Enter()
  {
    playerAnimations.OnFinishAttack += FinishAttack;
    Attack();
  }
  public void Exit()
  {
    playerAnimations.OnFinishAttack -= FinishAttack;
  }

  private void Attack()
  {
    playerAnimations.PlayAttackAnimation();
  }
  private void FinishAttack()
  {

    playerFSM.SwitchState(player.Locomotion);
  }

}