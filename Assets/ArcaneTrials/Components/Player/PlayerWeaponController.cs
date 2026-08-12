using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
  public WeaponConfig weaponConfig;
  public Transform weaponGrid;
  public GameObject currentSwordPrefab;
  private Attack attack;
  private AttackEffects attackEffects;
  private PlayerAudio playerAudio;

  public WeaponStats WeaponStats { get; private set; }

  void Awake()
  {
    attack = GetComponent<Attack>();
    attackEffects = GetComponent<AttackEffects>();
    playerAudio = GetComponent<PlayerAudio>();
  }

  void Start()
  {
    WeaponStats = new WeaponStats(weaponConfig);
    SwordHitBox swordHitBox = currentSwordPrefab.GetComponent<SwordHitBox>();
    attack.Init(WeaponStats, swordHitBox);
    attackEffects.Init(weaponConfig.weaponVFX);
    playerAudio.InitWeaponConfig(weaponConfig.weaponVFX);
  }


  private void ChangeWeaponVisual()
  {
    foreach (Transform child in weaponGrid)
      Destroy(child.gameObject);

    //GameObject currentSwordPrefab = Instantiate(weaponConfig.nextPrefab, weaponPosition);
    //SwordHitBox swordHitBox = currentSwordPrefab.GetComponent<SwordHitBox>();
    //attack.SetNewHitBox(swordHitBox);
  }
}