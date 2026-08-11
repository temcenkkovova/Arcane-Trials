using UnityEngine;

public class PlayerAnimationsController : MonoBehaviour
{
  public Animator animator;
  private PlayerInputController inputController;
  private PlayerMovement movement;

  void Awake()
  {
    inputController = GetComponent<PlayerInputController>();
    movement = GetComponent<PlayerMovement>();

  }

  void Start()
  {
    if (inputController == null) return;
    inputController.OnMoveChanged += HandleMoveChanged;
  }

  private void OnDisable()
  {
    if (inputController == null) return;
    inputController.OnMoveChanged -= HandleMoveChanged;
  }

  public void HandleMoveChanged(Vector2 input)
  {
    animator.SetFloat("Speed", movement.CurrentSpeedPercent, 0.15f, Time.deltaTime); // For make smooth threshold .
  }
}