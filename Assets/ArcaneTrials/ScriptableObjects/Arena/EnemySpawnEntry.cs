using UnityEngine;

[CreateAssetMenu(menuName = "Arena/EnemySpawn")]
public class EnemySpawnEntry : ScriptableObject
{
  public EnemyConfig enemyConfig;
  public float weight;
  public float count;
}