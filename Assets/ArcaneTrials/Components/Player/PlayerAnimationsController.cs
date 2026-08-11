using System;
using UnityEngine;

public class PlayerAnimationsController : MonoBehaviour
{
  public Animator animator;

  private PlayerMovement movement;
  public event Action OnFinishDodge;

  void Awake()
  {

    movement = GetComponent<PlayerMovement>();

  }

  void Start()
  {
    if (movement == null) return;
    movement.OnMoveDirectionChanged += HandleMoveChanged;
  }

  private void OnDisable()
  {
    if (movement == null) return;
    movement.OnMoveDirectionChanged -= HandleMoveChanged;
  }

  public void HandleMoveChanged(Vector3 localDirection)
  {

    animator.SetFloat("MoveX", localDirection.x, 0.1f, Time.deltaTime); // For make smooth threshold .
    animator.SetFloat("MoveY", localDirection.z, 0.1f, Time.deltaTime);

  }
  public void HandleStartDodge()
  {
    animator.SetTrigger("Roll");
  }
  public void HandleFinishDodge()
  {
    Debug.Log("He");
    OnFinishDodge?.Invoke();
  }
}