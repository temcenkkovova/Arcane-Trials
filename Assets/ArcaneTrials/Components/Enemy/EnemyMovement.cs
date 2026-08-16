using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
  private Vector3 moveDirection;
  private Rigidbody rb;
  private EnemyConfig enemyConfig;
  private float rotateSpeed = 5f;

  public float CurrentEnemySpeedPercent { get; private set; }

  private void Awake()
  {
    rb = GetComponent<Rigidbody>();

    // Столкновения больше не могут наклонять или раскручивать противника.
    rb.constraints |= RigidbodyConstraints.FreezePositionY |
                      RigidbodyConstraints.FreezeRotationX |
                      RigidbodyConstraints.FreezeRotationZ;
    rb.angularVelocity = Vector3.zero;
  }

  public void Init(EnemyConfig enemyConfig)
  {
    this.enemyConfig = enemyConfig;
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
    if (enemyConfig == null) return;

    Vector3 velocity = moveDirection * enemyConfig.moveSpeed;
    velocity.y = 0f;
    rb.linearVelocity = velocity;
    rb.angularVelocity = Vector3.zero;

    CurrentEnemySpeedPercent = moveDirection.sqrMagnitude > 0.01f ? 1f : 0f;
    if (moveDirection.sqrMagnitude < 0.01f) return;

    Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
    Quaternion newRotation = Quaternion.Slerp(
      rb.rotation,
      targetRotation,
      rotateSpeed * Time.fixedDeltaTime);

    rb.MoveRotation(newRotation);
  }
}