using UnityEngine;
public class DodgeState : IPlayerState
{
  private Vector3 dodgeDirection;
  private PlayerMovement movement;
  private PlayerDodge playerDodge;
  private PlayerAnimationsController playerAnimations;
  private PlayerComponent player;
  private PlayerFSMController playerFSM;
  public DodgeState(PlayerMovement movement, PlayerDodge playerDodge, PlayerAnimationsController playerAnimations, PlayerFSMController playerFSM, PlayerComponent player)
  { this.movement = movement; this.playerDodge = playerDodge; this.playerAnimations = playerAnimations; this.player = player; this.playerFSM = playerFSM; }
  public void Update() { }
  public void Enter()
  {
    playerAnimations.OnFinishDodge += SwitchOnLocomotionState;
    playerDodge.OnFinishDash += SwitchOnLocomotionState;
    dodgeDirection = movement.CurrentMoveDirection;
    if (dodgeDirection.sqrMagnitude < 0.01f) dodgeDirection = movement.transform.forward;
    dodgeDirection.Normalize();
    playerAnimations.HandleStartDodge();
    playerDodge.StartDodge(dodgeDirection);
  }
  public void Exit()
  {
    playerDodge.OnFinishDash -= SwitchOnLocomotionState;
    playerAnimations.OnFinishDodge -= SwitchOnLocomotionState;
  }
  private void SwitchOnLocomotionState() => playerFSM.SwitchState(player.Locomotion);
}
