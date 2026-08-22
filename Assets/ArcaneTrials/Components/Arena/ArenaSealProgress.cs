
using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class ArenaSealProgress : MonoBehaviour
{
  public float requiredKills;
  private float currentKillsAmount;
  public event Action<float> OnCurrentKillsChanged;
  public bool isBossSpawn { get; private set; } = false;
  public event Action OnCompletedProgress;


  [Button]
  private void TestKillProgress()
  {
    AddKillToProgress();
  }

  public void Init(SpawnArenaConfig arenaConfig)
  {
    requiredKills = arenaConfig.MaxEnemies;

  }

  public void AddKillToProgress()
  {
    if (isBossSpawn) return;

    currentKillsAmount++;
    OnCurrentKillsChanged?.Invoke(currentKillsAmount);



    if (currentKillsAmount == requiredKills)
      SpawnBoss();
  }

  private void SpawnBoss()
  {
    isBossSpawn = true;
    OnCompletedProgress?.Invoke();

  }
}