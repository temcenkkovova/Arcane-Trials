using UnityEngine;
[CreateAssetMenu(menuName = "Combat/Attack")]
public class AttackConfig : ScriptableObject
{
  public float damage;
  public float speed;

  public float critChance;
  public float critMultiplier;


}