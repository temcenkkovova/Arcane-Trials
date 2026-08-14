using System;
using UnityEngine;

public class EnemyTargetController : MonoBehaviour
{
  public PlayerHealth playerHealth;
  public Transform targetTr { get; private set; }
  public event Action OnTargetClear;
  public event Action OnTarget;

  void Awake()
  {

    SetTarget(playerHealth.transform);
  }


  public void SetTarget(Transform tr)
  {
    if (targetTr == tr) return;
    targetTr = tr;
    OnTarget?.Invoke();

  }
  public void ClearTarget()
  {
    if (targetTr == null) return;
    targetTr = null;
    OnTargetClear?.Invoke();
  }

  void OnDisable()
  {
    playerHealth.OnDead -= ClearTarget;
  }
  void OnEnable()
  {
    playerHealth.OnDead += ClearTarget;
  }
}