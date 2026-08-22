using Unity.VisualScripting;
using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
  public WeaponConfig weaponConfig;
  //public Transform weaponGrid;
  public GameObject currentSwordPrefab;
  private Attack attack;


  public WeaponStats WeaponStats { get; private set; }

  void Awake()
  {
    attack = GetComponent<Attack>();
  }

  void Start()
  {
    WeaponStats = new WeaponStats(weaponConfig);
    SwordHitBox swordHitBox = currentSwordPrefab.GetComponent<SwordHitBox>();
    attack.Init(WeaponStats, swordHitBox);
    swordHitBox.DisableSwordCollider();
  }
}