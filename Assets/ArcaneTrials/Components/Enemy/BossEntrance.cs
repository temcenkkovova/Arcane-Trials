using System;
using System.Collections;
using UnityEngine;

public class BossEntrance : MonoBehaviour
{
  [SerializeField, Min(0.1f)] private float jumpDistance = 2f;
  [SerializeField, Min(0.1f)] private float jumpHeight = 1f;
  [SerializeField, Min(0.1f)] private float jumpDuration = 2f;

  private Vector3 bossEntrancePosition;
  private Vector3 entranceDirection;
  private EnemyMovement enemyMovement;
  private EnemyAnimationsController enemyAnimationsController;
  private Rigidbody rb;
  private Collider bossCollider;


  public bool IsArrivedOnPosition { get; private set; }
  public bool IsJumping { get; private set; }
  public event Action OnPosition;

  private void Awake()
  {
    enemyMovement = GetComponent<EnemyMovement>();
    enemyAnimationsController = GetComponent<EnemyAnimationsController>();
    rb = GetComponent<Rigidbody>();
    bossCollider = GetComponent<CapsuleCollider>();

  }
  void OnEnable()
  {
    if (bossCollider == null) return;
    bossCollider.isTrigger = true;
  }

  public void Init(Vector3 position)
  {
    bossEntrancePosition = position;
  }

  public void Entrance()
  {
    if (enemyMovement == null || IsJumping || IsArrivedOnPosition)
      return;

    entranceDirection = bossEntrancePosition - rb.position;
    entranceDirection.y = 0f;

    if (entranceDirection.sqrMagnitude <= 0.04f)
    {
      JumpOnPosition();
      return;
    }

    enemyMovement.SetDirection(entranceDirection);
  }


  public void JumpOnPosition()
  {
    if (IsJumping || IsArrivedOnPosition)
      return;

    IsJumping = true;
    enemyMovement.StopMove();
    enemyAnimationsController.PlayJumpAnimation();
    StartCoroutine(JumpRoutine());
  }

  private IEnumerator JumpRoutine()
  {
    Vector3 horizontalDirection = entranceDirection;
    horizontalDirection.y = 0f;

    if (horizontalDirection.sqrMagnitude < 0.001f)
      horizontalDirection = transform.forward;

    horizontalDirection.Normalize();

    Vector3 startPosition = rb.position;
    Vector3 targetPosition = startPosition + horizontalDirection * jumpDistance;

    rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
    rb.isKinematic = true;

    float elapsed = 0f;
    while (elapsed < jumpDuration)
    {
      yield return new WaitForFixedUpdate();
      elapsed += Time.fixedDeltaTime;

      float progress = Mathf.Clamp01(elapsed / jumpDuration);
      Vector3 position = Vector3.Lerp(startPosition, targetPosition, progress);
      position.y += 4f * jumpHeight * progress * (1f - progress);
      rb.MovePosition(position);
    }

    rb.position = targetPosition;
    IsJumping = false;
    IsArrivedOnPosition = true;
    OnPosition?.Invoke();
    if (bossCollider != null)
      bossCollider.isTrigger = false;


  }

}
