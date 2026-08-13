
using UnityEngine;

public class EnemySpawnArena : MonoBehaviour
{
  public SpawnArenaConfig spawnArenaConfig;
  public BoxCollider spawnAreaCollider;
  public Transform playerTr;



  void Start()
  {
    SpawnEnemies();
  }



  private void SpawnEnemies()
  {
    if (spawnArenaConfig.Enemies == null) return;
    foreach (var item in spawnArenaConfig.Enemies)
    {
      SpawnEnemy(item);
    }
  }

  private void SpawnEnemy(EnemyConfig enemyConfig)
  {
    EnemyBootstrap enemy = Instantiate(enemyConfig.prefab, GetRandomPosition(enemyConfig.prefab.transform.position), transform.rotation);
    EnemyTargetController enemyTargetController = enemy.GetComponent<EnemyTargetController>();
    enemyTargetController.SetTarget(playerTr);
    enemy.Init(enemyConfig);
  }
  private Vector3 GetRandomPosition(Vector3 vector)
  {
    Bounds bounds = spawnAreaCollider.bounds;
    return new Vector3(
         Random.Range(bounds.min.x, bounds.max.x),
         vector.y,
         Random.Range(bounds.min.z, bounds.max.z)
     );
  }
}