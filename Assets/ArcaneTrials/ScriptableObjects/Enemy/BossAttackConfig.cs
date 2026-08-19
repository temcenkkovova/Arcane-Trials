using UnityEngine;
[CreateAssetMenu(menuName = "BossAttack")]
public class BossAttackConfig : ScriptableObject
{
  public float damage;
  public float scaleDamage; // If player is in the center of circle apply more damage;
  public float attackCooldawn;
  public float attackAreaRadius;


}