using UnityEngine;

[CreateAssetMenu(menuName = "Enemy")]
public class EnemyConfig : ScriptableObject
{
  public float health;
  public float moveSpeed;
  public RewardsConfig rewards;
  public EnemyBootstrap prefab;
  public WeaponConfig weaponConfig;

}