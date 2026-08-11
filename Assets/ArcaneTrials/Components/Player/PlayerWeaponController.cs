using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
  public WeaponConfig weaponConfig;
  public Transform weaponGrid;

  public WeaponStats WeaponStats { get; private set; }

  void Start()
  {
    WeaponStats = new WeaponStats(weaponConfig);
  }


  private void ChangeWeaponVisual()
  {
    foreach (Transform child in weaponGrid)
      Destroy(child.gameObject);

    //GameObject weapon = Instantiate(weaponConfig.nextPrefab, weaponPosition);
  }
}