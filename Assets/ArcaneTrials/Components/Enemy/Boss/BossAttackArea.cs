using UnityEngine;

public class BossAttackArea : MonoBehaviour
{
  [SerializeField] private Renderer warningRenderer;

  private Transform target;
  private float Radius => config.attackAreaRadius;
  private float Damage => config.damage;
  private BossAttackConfig config;

  public void Init(BossAttackConfig config, Transform target)
  {
    this.target = target;
    this.config = config;


    transform.localScale =
        new Vector3(Radius * 2f, 1f, Radius * 2f);
  }

  public void ApplyDamage()
  {

    if (target == null)
      return;

    Vector3 offset = target.position - transform.position;
    offset.y = 0f;

    if (offset.sqrMagnitude <= Radius * Radius)
    {
      if (target.TryGetComponent(out PlayerHealth health))
        health.TakeDamage(Damage);
    }

    Destroy(gameObject);
  }
}