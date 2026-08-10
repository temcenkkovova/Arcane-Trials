using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "PlayerConfig", order = 0)]
public class PlayerConfig : ScriptableObject
{
  public float baseSpeed;
  public float baseSpeedRotation;
  public float baseHealth;
  public float baseDamage;
  public float dashCooldawn;
}