using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Arena/Spawn")]
public class SpawnArenaConfig : ScriptableObject
{
  public List<EnemySpawnEntry> Enemies;
  public int MaxEnemies;
  public float RespawnTime;
  public ArenaBossConfig bossConfig;

}