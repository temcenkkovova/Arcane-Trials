using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyPopupManager : MonoBehaviour
{
  public DamagePopup damagePopupPrefab;
  private EnemyHealth enemyHealth;

  public Transform popupPos;

  [ColorPalette]
  public Color ColorOptions;
  public Color color;
  void Awake()
  {
    enemyHealth = GetComponent<EnemyHealth>();

    if (enemyHealth == null) return;
    enemyHealth.OnDamaged += HandleShowPopup;
  }
  void OnDisable()
  {
    if (enemyHealth == null) return;
    enemyHealth.OnDamaged -= HandleShowPopup;

  }

  public void HandleShowPopup(float value)
  {


    DamagePopup popup = Instantiate(damagePopupPrefab, popupPos.position, Camera.main.transform.rotation);
    popup.Show(value, color);

  }
}