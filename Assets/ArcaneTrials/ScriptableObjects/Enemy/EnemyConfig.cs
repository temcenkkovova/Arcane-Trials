using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy")]
public class EnemyConfig : ScriptableObject
{
  [BoxGroup("Stats")]
  [MinValue(1)]
  public float health;
  [BoxGroup("Stats")]
  [MinValue(1)]
  public float moveSpeed;
  [BoxGroup("Reward")]

  public RewardsConfig rewards;
  [BoxGroup("Visual")]

  public EnemyBootstrap prefab;
  [BoxGroup("Weapon")]
  public WeaponConfig weaponConfig;
  public EnemyAudioConfig enemyAudioConfig;



}