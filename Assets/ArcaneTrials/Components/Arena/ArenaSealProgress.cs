using System;
using UnityEngine;

public class ArenaSealProgress : MonoBehaviour
{
  public float requiredKills;
  private float currentKillsAmount;
  public event Action<float> OnCurrentKillsChanged;
  private bool isBossSpawn = false;
  public event Action OnCompletedProgress;

  public void Init(float amount)
  {
    requiredKills = amount;
  }

  public void AddKillToProgress()
  {
    if (isBossSpawn) return;
    Debug.Log("Arena Seal count changed");
    currentKillsAmount++;
    OnCurrentKillsChanged?.Invoke(currentKillsAmount);
    if (currentKillsAmount == requiredKills)
      SpawnBoss();
  }

  private void SpawnBoss()
  {
    isBossSpawn = true;
    OnCompletedProgress?.Invoke();
    Debug.Log("Call boss");
  }
}