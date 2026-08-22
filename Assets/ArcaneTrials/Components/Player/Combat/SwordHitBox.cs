using System.Collections.Generic;
using UnityEngine;

public class SwordHitBox : MonoBehaviour
{
  private float damage;
  public Collider swordCollider;

  private readonly HashSet<IDamageable> damagedTargets = new();
  private Transform attackOrigin;
  private float areaRadius;
  private float attackHalfAngle;
  private bool useAreaAttack;
  private string owner;

  private void Start()
  {
    // DisableSwordCollider();
  }

  public void SetDamage(float newDamage, string owner)
  {

    damage = newDamage;
    this.owner = owner;
  }

  public void ConfigureAreaAttack(Transform origin, float radius, float halfAngle)
  {
    attackOrigin = origin;
    areaRadius = Mathf.Max(0f, radius);
    attackHalfAngle = Mathf.Clamp(halfAngle, 0f, 180f);
    useAreaAttack = attackOrigin != null && areaRadius > 0f;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (useAreaAttack) return;
    Debug.Log("Other tag" + other.tag + "owner Tag" + owner);
    if (other.tag == owner) return;
    TryDamage(other);
  }

  public void EnableSwordCollider()
  {
    damagedTargets.Clear();

    if (useAreaAttack)
    {
      DamageTargetsInArea();
      return;
    }

    if (swordCollider != null)
      swordCollider.enabled = true;
  }

  public void DisableSwordCollider()
  {
    damagedTargets.Clear();

    if (swordCollider != null)
      swordCollider.enabled = false;
  }

  private void DamageTargetsInArea()
  {
    Vector3 forward = attackOrigin.forward;
    forward.y = 0f;
    forward.Normalize();

    Collider[] hits = Physics.OverlapSphere(
      attackOrigin.position,
      areaRadius,
      Physics.AllLayers,
      QueryTriggerInteraction.Collide);

    foreach (Collider hit in hits)
    {
      Vector3 directionToTarget = hit.bounds.center - attackOrigin.position;
      directionToTarget.y = 0f;

      if (directionToTarget.sqrMagnitude < 0.0001f) continue;
      if (Vector3.Angle(forward, directionToTarget) > attackHalfAngle) continue;

      TryDamage(hit);
    }
  }

  private void TryDamage(Collider targetCollider)
  {
    IDamageable damageable = targetCollider.GetComponentInParent<IDamageable>();
    if (damageable == null || damagedTargets.Contains(damageable)) return;

    Component damageableComponent = damageable as Component;
    if (damageableComponent == null) return;

    if (attackOrigin != null &&
        damageableComponent.transform.root == attackOrigin.root)
      return;

    damagedTargets.Add(damageable);
    damageable.TakeDamage(damage);
  }
}