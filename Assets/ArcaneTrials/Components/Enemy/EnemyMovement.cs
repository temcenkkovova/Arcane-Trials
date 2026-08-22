using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
  private Vector3 moveDirection;
  private Rigidbody rb;
  private float moveSpeed;
  private EnemyTargetController target;

  [SerializeField] private float rotateSpeed = 360f;

  public float CurrentEnemySpeedPercent { get; private set; }

  private void Awake()
  {
    rb = GetComponent<Rigidbody>();

    rb.constraints |=
        RigidbodyConstraints.FreezePositionY |
        RigidbodyConstraints.FreezeRotationX |
        RigidbodyConstraints.FreezeRotationZ;

    rb.angularVelocity = Vector3.zero;
  }

  public void Init(float moveSpeed, EnemyTargetController target)
  {
    this.moveSpeed = moveSpeed;
    this.target = target;
  }



  public void StopMove()
  {
    moveDirection = Vector3.zero;
    rb.linearVelocity = Vector3.zero;
    rb.angularVelocity = Vector3.zero;
    CurrentEnemySpeedPercent = 0f;
  }

  public void SetDirection(Vector3 direction)
  {

    direction.y = 0f;
    moveDirection = direction.normalized;
  }

  private void FixedUpdate()
  {

    if (moveSpeed <= 0)
    {
      moveDirection.y = 0f;
      rb.linearVelocity = moveDirection;
      return;
    }


    Move();
    RotateTowardsTarget();
  }

  private void Move()
  {
    Vector3 velocity = moveDirection * moveSpeed;
    velocity.y = 0f;

    rb.linearVelocity = velocity;

    CurrentEnemySpeedPercent =
        moveDirection.sqrMagnitude > 0.01f ? 1f : 0f;
  }

  private void RotateTowardsTarget()
  {
    if (moveDirection.sqrMagnitude < 0.001f)
      return;

    Quaternion targetRotation =
        Quaternion.LookRotation(moveDirection, Vector3.up);

    Quaternion newRotation = Quaternion.RotateTowards(
        rb.rotation,
        targetRotation,
        rotateSpeed * Time.fixedDeltaTime);

    rb.MoveRotation(newRotation);
  }
}
