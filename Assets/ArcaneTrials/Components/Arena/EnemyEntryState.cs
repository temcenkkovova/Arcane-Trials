using UnityEngine;

public class EnemyEntryState
{
  public EnemySpawnEntry entry { get; private set; }
  public int CurrentAlive { get; private set; }
  public bool CanSpawn =>
   CurrentAlive < entry.count;

  public void RegisterSpawn()
  {
    CurrentAlive++;
  }

  public void RegisterDeath()
  {
    CurrentAlive = Mathf.Max(0, CurrentAlive - 1);
  }

  public EnemyEntryState(EnemySpawnEntry entry)
  {
    this.entry = entry;
  }
}