using DG.Tweening;
using TMPro;
using UnityEngine;

public class ShowPlayerHealth : MonoBehaviour
{
  public TMP_Text healthField;

  public RectTransform healthRect;
  private Vector3 initialScale;
  private Tween pulseTween;
  public float pulseScale = 0.2f;
  [SerializeField, Min(0.01f)] private float pulseDuration = 0.5f;
  private float previousHealth;



  public PlayerHealth playerHealth;


  void Awake()
  {
    initialScale = healthRect.localScale;
  }
  void OnEnable()
  {

    playerHealth.OnHealthChanged += ShowHealth;
    previousHealth = playerHealth.CurrentHealth;

  }
  void Start()
  {
    ShowHealthWithoutAnimation(playerHealth.CurrentHealth);
  }

  private void ShowHealth(float health)
  {
    healthField.text = Mathf.CeilToInt(health).ToString();
    bool receivedDamage = health < previousHealth;
    previousHealth = health;

    if (receivedDamage)
      PlayDamagePulse();
  }
  void OnDisable()
  {

    healthRect.DOKill();
    playerHealth.OnHealthChanged -= ShowHealth;
    healthRect.localScale = initialScale;
  }

  private void ShowHealthWithoutAnimation(float health)
  {
    previousHealth = health;
    healthField.text = Mathf.CeilToInt(health).ToString();
  }
  private void PlayDamagePulse()
  {
    healthRect.DOKill();
    healthRect.localScale = initialScale;

    healthRect
      .DOPunchScale(
        Vector3.one * pulseScale,
        pulseDuration,
        vibrato: 4,
        elasticity: 0.5f)
      .SetUpdate(true)
      .SetLink(gameObject);
  }

}