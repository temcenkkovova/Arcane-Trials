using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
  public float MaxHealth { get; private set; }

  [ShowInInspector, ReadOnly]
  public float CurrentHealth { get; private set; }
  public event Action<float> OnHealthChanged;
  private bool isDead;
  public event Action OnDead;
  public HitFeedback hitFeedback;

  [Button]
  private void TestDamage()
  {
    TakeDamage(10);
  }


  void Awake()
  {
    hitFeedback = GetComponent<HitFeedback>();
  }
  public void Init(float maxHealth)
  {
    MaxHealth = maxHealth;
    CurrentHealth = MaxHealth;
    OnHealthChanged?.Invoke(CurrentHealth);
  }

  public void TakeDamage(float damage)
  {
    if (isDead) return;
    float healthAfter = CurrentHealth - damage;
    if (healthAfter <= 0)
    {
      CurrentHealth = 0;
      OnHealthChanged?.Invoke(CurrentHealth);
      hitFeedback.PlayHit();
      Die();
    }
    else
    {

      CurrentHealth = healthAfter;
      hitFeedback.PlayHit();
      OnHealthChanged?.Invoke(CurrentHealth);
    }
  }
  protected virtual void Die()
  {
    isDead = true;
    OnDead?.Invoke();
  }
}