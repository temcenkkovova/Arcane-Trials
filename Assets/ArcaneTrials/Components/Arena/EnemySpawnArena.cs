

using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnArena : MonoBehaviour
{
  public SpawnArenaConfig spawnArenaConfig;
  public Collider spawnAreaCollider;
  public Transform playerTr;
  public EnemyTargetController enemyTargetController;
  private ArenaSealProgress arenaProgress;
  private ArenaAudio arenaAudio;
  private List<EnemyEntryState> enemyEntries = new List<EnemyEntryState>();
  private int totalLive;

  void Awake()
  {
    arenaAudio = GetComponent<ArenaAudio>();
    arenaProgress = GetComponent<ArenaSealProgress>();
  }
  void Start()
  {
    totalLive = 0;
    arenaProgress.Init(spawnArenaConfig);
    arenaAudio.Init(spawnArenaConfig.bossConfig);

    foreach (var item in spawnArenaConfig.Enemies)
    {
      enemyEntries.Add(new EnemyEntryState(item));
    }
    SpawnEnemies();
  }
  void Update()
  {
    if (arenaProgress.isBossSpawn) return;
    if (totalLive < spawnArenaConfig.MaxEnemies)
    {
      SpawnEnemy(GetEnemyEntry());
    }
  }
  private void SpawnEnemies()
  {
    if (spawnArenaConfig.Enemies == null) return;
    EnemyEntryState entry = GetEnemyEntry();
    if (entry != null)
    {
      SpawnEnemy(entry);
    }
    SpawnBoss(spawnArenaConfig.bossConfig);
  }

  private void SpawnEnemy(EnemyEntryState entryEnemy)
  {
    EnemyConfig enemyConfig = entryEnemy.entry.enemyConfig;
    EnemyBootstrap enemy = Instantiate(enemyConfig.prefab, GetRandomPosition(enemyConfig.
    prefab.transform.position), transform.rotation);
    enemy.Init(enemyConfig, enemyTargetController, arenaProgress, entryEnemy, this);
    entryEnemy.RegisterSpawn();
    totalLive++;
  }
  public void HandleEnemyDeath()
  {
    totalLive--;
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

  private void SpawnBoss(ArenaBossConfig bossConfig)
  {
    BossBootstrap boss = Instantiate(bossConfig.bossPrefab, bossConfig.spawnPos, bossConfig.spawnBossRotation);
    Debug.Log(Quaternion.identity);
    boss.Init(enemyTargetController, arenaProgress, bossConfig);
  }

  public EnemyEntryState GetEnemyEntry()
  {
    float totalWeight = 0f;
    foreach (var item in enemyEntries)
    {
      if (item.CanSpawn)
        totalWeight += item.entry.weight;
    }

    if (totalWeight <= 0f) return null;

    float randomRange = Random.Range(0, totalWeight);

    foreach (var enemyEntry in enemyEntries)
    {
      if (!enemyEntry.CanSpawn)
        continue;

      randomRange -= enemyEntry.entry.weight;

      if (randomRange <= 0f)
        return enemyEntry;
    }


    return null;
  }


}