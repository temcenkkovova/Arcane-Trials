using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
  private WeaponVFXConfig config;

  public void InitWeaponConfig(WeaponVFXConfig config)
  {
    this.config = config;
  }

  public void PlayShootAudio()
  {
    if (config == null) return;

    {
      var clip = config.shootClips[Random.Range(0, config.shootClips.Length)];
      float pitch = Random.Range(config.pitchMin, config.pitchMax);
      AudioService.Instance.PlayAt(transform.position, clip, config.volume, pitch);
    }
  }
}