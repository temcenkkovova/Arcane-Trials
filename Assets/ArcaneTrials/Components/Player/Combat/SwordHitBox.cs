using UnityEngine;

public class SwordHitBox : MonoBehaviour
{
  private float damage;
  public Collider swordCollider;


  void Start()
  {
    DisableSwordCollider();
  }

  public void SetDamage(float newDamage)
  {
    damage = newDamage;

  }

  private void OnTriggerEnter(Collider other)
  {
  }

  public void EnableSwordCollider()
  {
    swordCollider.enabled = true;
  }
  public void DisableSwordCollider()
  {
    swordCollider.enabled = false;
  }

}