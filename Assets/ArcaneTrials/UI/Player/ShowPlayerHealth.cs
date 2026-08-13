using TMPro;
using UnityEngine;

public class ShowPlayerHealth : MonoBehaviour
{
  public TMP_Text healthField;


  public PlayerHealth playerHealth;


  void Awake()
  {

    playerHealth.OnHealthChanged += ShowHealth;
  }
  void Start()
  {
    ShowHealth(playerHealth.CurrentHealth);
  }

  private void ShowHealth(float health)
  {
    healthField.text = health.ToString();
  }
  void OnDisable()
  {
    playerHealth.OnHealthChanged -= ShowHealth;
  }
}