
using UnityEngine;

public class EnemyBootstrap : MonoBehaviour
{
  private EnemyConfig config;
  private EnemyHealth enemyHealth;

  void Awake()
  {
    enemyHealth = GetComponent<EnemyHealth>();
  }


  public void Init(EnemyConfig config)
  {
    this.config = config;
    enemyHealth.Init(config.health);
  }
}


