using UnityEngine;


public class PlayerInputController : MonoBehaviour
{
  public GameInputActions inputActions;
  private PlayerMovement playerMovement;


  private void Awake()
  {
    inputActions = new GameInputActions();
    playerMovement = GetComponent<PlayerMovement>();
  }

  void Update()
  {
    Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
    if (playerMovement == null || inputActions == null) return;
    if (inputVector.sqrMagnitude > 0.1f)
      playerMovement.Move(inputVector);



  }

  private void OnEnable()
  {
    inputActions.Player.Enable();
  }

  private void OnDisable()
  {
    inputActions.Player.Disable();
  }
}