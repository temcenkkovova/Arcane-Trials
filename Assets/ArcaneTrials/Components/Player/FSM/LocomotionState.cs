using UnityEngine;

public class LocomotionState : IPlayerState
{

  private PlayerMovement movement;
  private PlayerRotation playerRotation;
  private PlayerInputController playerInput;
  private PlayerFSMController playerFSM;

  private PlayerComponent player;

  public LocomotionState(PlayerMovement movement, PlayerInputController playerInput, PlayerRotation playerRotation, PlayerFSMController playerFSM, PlayerComponent player)
  {
    this.movement = movement;
    this.playerInput = playerInput;
    this.playerRotation = playerRotation;
    this.playerFSM = playerFSM;
    this.player = player;

  }
  public void Enter()
  {
    playerInput.OnRoll += SwitchOnDodgeState;
  }
  public void Update()
  {
    movement.Move(playerInput.MoveInput);
    playerRotation.RotateByMouse(playerInput.MousePosition);
  }
  public void Exit()
  {
    playerInput.OnRoll -= SwitchOnDodgeState;
  }


  public void SwitchOnDodgeState()
  {

    playerFSM.SwitchState(player.Dodge);

  }
}