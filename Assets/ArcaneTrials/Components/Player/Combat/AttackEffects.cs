using UnityEngine;
public class AttackEffects : MonoBehaviour
{

  private WeaponVFXConfig config;
  public Transform vfxPosition;
  public void Init(WeaponVFXConfig config)
  {
    this.config = config;
  }

  public void PlayAttackEffect()
  {
    Debug.Log(config.particle);
    SlashVFX slash = Instantiate(config.particle, vfxPosition.position, vfxPosition.rotation);
  }


}