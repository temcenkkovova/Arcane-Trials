using UnityEngine;
[CreateAssetMenu(menuName = "Vfx/Weapon")]
public class WeaponVFXConfig : ScriptableObject
{
  public AudioClip[] shootClips;

  public float volume = 1f;
  public float pitchMin = 0.95f;
  public float pitchMax = 1.05f;
  public SlashVFX particle;
}