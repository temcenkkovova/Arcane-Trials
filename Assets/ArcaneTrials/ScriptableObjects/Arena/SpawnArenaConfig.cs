using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Arena/Spawn")]
public class SpawnArenaConfig : ScriptableObject
{
  public List<EnemyConfig> Enemies;
  public int MaxEnemies;
  public float RespawnTime;
}