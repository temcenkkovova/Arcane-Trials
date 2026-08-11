using UnityEngine;

public class PlayerFSMController : MonoBehaviour
{
  private IPlayerState currentState;



  public void InitState(IPlayerState locomotionState)
  {
    SwitchState(locomotionState);
  }
  void Update()
  {
    currentState.Update();
  }

  public void SwitchState(IPlayerState newState)
  {
    if (currentState == newState) return;
    currentState?.Exit();
    currentState = newState;
    currentState?.Enter();
  }
}