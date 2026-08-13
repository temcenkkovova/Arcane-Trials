using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
  private Vector3 moveDirection;
  private Rigidbody rb;
  private EnemyConfig enemyConfig;
  private float rotateSpeed = 5f;
  public float CurrentEnemySpeedPercent { get; private set; }
  void Awake()
  {
    rb = GetComponent<Rigidbody>();

  }

  public void Init(EnemyConfig enemyConfig)
  {
    this.enemyConfig = enemyConfig;
  }
  public void StopMove()
  {
    moveDirection = Vector3.zero;
    rb.linearVelocity = moveDirection;
  }
  public void SetDirection(Vector3 direction)
  {
    moveDirection = direction;
  }
  private void FixedUpdate()
  {
    if (enemyConfig)
    {
      rb.linearVelocity = moveDirection * enemyConfig.moveSpeed;
      CurrentEnemySpeedPercent = moveDirection.sqrMagnitude > 0.01f ? 1f : 0f;
      Vector3 rotateDir = moveDirection;
      rotateDir.y = 0f;
      if (rotateDir.sqrMagnitude < 0.01f) return;
      Quaternion targetRotation = Quaternion.LookRotation(rotateDir);
      transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.fixedDeltaTime);
    }

  }
}