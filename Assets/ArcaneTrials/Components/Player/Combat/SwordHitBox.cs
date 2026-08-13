using UnityEngine;

public class SwordHitBox : MonoBehaviour
{
  private float damage;
  public Collider swordCollider;
  private Collider currentDamageableTarget;


  void Start()
  {
    DisableSwordCollider();
  }

  public void SetDamage(float newDamage)
  {
    damage = newDamage;

  }

  private void OnTriggerEnter(Collider other)
  {
    if (currentDamageableTarget != null) return;
    IDamageable damageable = other.GetComponent<IDamageable>();
    if (damageable == null) return;
    damageable.TakeDamage(damage);
    currentDamageableTarget = other;

  }

  public void EnableSwordCollider()
  {
    swordCollider.enabled = true;
  }
  public void DisableSwordCollider()
  {
    currentDamageableTarget = null;
    swordCollider.enabled = false;
  }

}