using System;
using UnityEngine;

public class EnemyTargetController : MonoBehaviour
{
  public Transform targetTr { get; private set; }

  public event Action OnTargetClear;
  public event Action OnTarget;

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
    OnTarget?.Invoke();
  }
}