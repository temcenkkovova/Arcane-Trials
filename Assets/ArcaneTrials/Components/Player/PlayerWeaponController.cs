using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
  public WeaponItem startWeapon;
  public Transform weaponPosition;
  public WeaponItem CurrentWeapon { get; private set; }

  void Start()
  {
    EquipWeapon(startWeapon);
  }


  public void EquipWeapon(WeaponItem weaponItem)
  {
    if (IsEquipped(weaponItem)) return;

    CurrentWeapon = weaponItem;
    foreach (Transform child in weaponPosition)
      Destroy(child.gameObject);

    GameObject weapon = Instantiate(startWeapon.weaponConfig.weaponPrefab, weaponPosition);
  }

  public bool IsEquipped(WeaponItem weapon)
  {
    return CurrentWeapon == weapon;
  }
}