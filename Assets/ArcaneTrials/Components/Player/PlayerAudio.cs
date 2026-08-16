using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
  private WeaponVFXConfig config;
  private PlayerHealth playerHealth;
  public PlayerAudioConfig playerAudioConfig;
  private float previousHealth;

  void Awake()
  {
    playerHealth = GetComponent<PlayerHealth>();

  }
  void Start()
  {
    previousHealth = playerHealth.CurrentHealth;
  }
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
  void OnEnable()
  {
    if (playerHealth == null) return;
    playerHealth.OnHealthChanged += PlayDamagedAudio;

  }
  void OnDisable()
  {
    if (playerHealth == null) return;
    playerHealth.OnHealthChanged -= PlayDamagedAudio;
    previousHealth = playerHealth.CurrentHealth;
  }
  private void PlayDamagedAudio(float value)
  {

    if (playerAudioConfig == null) return;
    if (previousHealth < playerHealth.CurrentHealth) return;
    var clip = playerAudioConfig.hitClips[Random.Range(0, playerAudioConfig.hitClips.Length)];
    float pitch = Random.Range(playerAudioConfig.pitchMin, playerAudioConfig.pitchMax);
    AudioService.Instance.PlayAt(transform.position, clip, playerAudioConfig.volume, pitch);
    previousHealth = playerHealth.CurrentHealth;

  }
}