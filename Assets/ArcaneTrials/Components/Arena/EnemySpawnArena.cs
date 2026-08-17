
using UnityEngine;

public class EnemySpawnArena : MonoBehaviour
{
  public SpawnArenaConfig spawnArenaConfig;
  public BoxCollider spawnAreaCollider;
  public Transform playerTr;
  public EnemyTargetController enemyTargetController;
  private ArenaSealProgress arenaProgress;
  private ArenaAudio arenaAudio;

  void Awake()
  {
    arenaAudio = GetComponent<ArenaAudio>();
    arenaProgress = GetComponent<ArenaSealProgress>();
  }
  void Start()
  {
    SpawnEnemies();
    arenaProgress.Init(spawnArenaConfig);
    arenaAudio.Init(spawnArenaConfig.bossConfig);
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


    enemy.Init(enemyConfig, enemyTargetController, arenaProgress);
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