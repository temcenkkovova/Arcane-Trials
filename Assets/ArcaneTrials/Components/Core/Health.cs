using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
  // public float MaxHealth { get; private set; }
  public float MaxHealth;
  public float CurrentHealth { get; private set; }
  public event Action<float> OnHealthChanged;
  private bool isDead;
  public event Action OnDead;

  public void Init(float maxHealth)
  {
    MaxHealth = maxHealth;
    CurrentHealth = MaxHealth;
  }

  public void TakeDamage(float damage)
  {
    if (isDead) return;
    float healthAfter = CurrentHealth - damage;
    if (healthAfter <= 0)
    {
      CurrentHealth = 0;
      OnHealthChanged?.Invoke(CurrentHealth);
      isDead = true;
      OnDead?.Invoke();
    }
    else
    {
      CurrentHealth = healthAfter;
      OnHealthChanged?.Invoke(CurrentHealth);
    }
  }
}