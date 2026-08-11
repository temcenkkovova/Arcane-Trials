using UnityEngine;

public class PlayerDodge : MonoBehaviour
{
  private CharacterController characterController;
  [SerializeField] private float dodgeSpeed = 5f;

  void Awake()
  {
    characterController = GetComponent<CharacterController>();
  }

  public void Dodge(Vector3 direction)
  {
    characterController.Move(direction * dodgeSpeed * Time.deltaTime);
  }
}