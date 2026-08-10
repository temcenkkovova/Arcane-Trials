using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  private PlayerStats playerStats;
  private CharacterController characterController;

  public void Init(PlayerStats playerStats)
  {
    this.playerStats = playerStats;
  }

  void Start()
  {
    characterController = GetComponent<CharacterController>();
  }

  public void Move(Vector2 input)
  {

    Vector3 moveDir = new Vector3(input.x, 0, input.y);
    if (characterController == null) return;
    if (moveDir.sqrMagnitude > 1f)
      moveDir.Normalize();
    characterController.Move(moveDir * playerStats.MoveSpeed * Time.deltaTime);
  }

}
