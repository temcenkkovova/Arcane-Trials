using UnityEngine;

public class LocomotionState : IPlayerState
{

  private PlayerMovement movement;
  private PlayerRotation playerRotation;
  private PlayerInputController playerInput;

  public LocomotionState(PlayerMovement movement, PlayerInputController playerInput, PlayerRotation playerRotation)
  {
    this.movement = movement;
    this.playerInput = playerInput;
    this.playerRotation = playerRotation;
  }
  public void Enter()
  {

  }
  public void Update()
  {
    movement.Move(playerInput.MoveInput);
    playerRotation.RotateByMouse(playerInput.MousePosition);
  }
  public void Exit()
  {

  }

}