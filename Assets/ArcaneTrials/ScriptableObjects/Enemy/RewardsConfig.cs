using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Reward/Enemy")]
public class RewardsConfig : ScriptableObject
{

  [BoxGroup("Update")]
  [MinValue(1)]
  public float coins;
  [BoxGroup("Update")]
  [MinValue(1)]
  public float exp;
  [BoxGroup("Sustain")]
  [MinValue(0)]
  public float health;
  [BoxGroup("Sustain")]
  [MinValue(0)]
  public float mana;


}