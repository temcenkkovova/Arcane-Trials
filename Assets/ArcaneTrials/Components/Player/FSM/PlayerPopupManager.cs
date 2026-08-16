using UnityEngine;

public class PlayerPopupManager : MonoBehaviour
{
  public DamagePopup damagePopupPrefab;
  private PlayerHealth playerHealth;

  public Transform popupPos;

  void Awake()
  {
    playerHealth = GetComponent<PlayerHealth>();

    if (playerHealth == null) return;
    playerHealth.OnDamaged += HandleShowPopup;
  }
  void OnDisable()
  {
    if (playerHealth == null) return;
    playerHealth.OnDamaged -= HandleShowPopup;

  }

  public void HandleShowPopup(float value)
  {


    DamagePopup popup = Instantiate(damagePopupPrefab, popupPos.position, Camera.main.transform.rotation);
    popup.Show(value);

  }
}