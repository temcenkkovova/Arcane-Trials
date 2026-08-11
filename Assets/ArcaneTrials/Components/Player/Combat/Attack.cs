using UnityEngine;

public class Attack : MonoBehaviour
{

  private WeaponStats weaponStats;
  private float lastAttackTime;
  private SwordHitBox swordHitBox;
  public void Init(WeaponStats weaponStats, SwordHitBox hitBox)
  {
    this.weaponStats = weaponStats;
    swordHitBox = hitBox;
  }
  public virtual bool CanAttack()
  {

    return Time.time >= lastAttackTime + weaponStats.Speed;
  }

  public virtual void AttackAction()
  {
    ExecuteAttack();
    lastAttackTime = Time.time;
  }

  private void ExecuteAttack()
  {
    if (weaponStats == null)
    {
      Debug.Log("WeaponStats is messing");
      return;
    }
    swordHitBox.SetDamage(weaponStats.Damage);
  }

  public void SetNewHitBox(SwordHitBox newHitBox)
  {
    swordHitBox = newHitBox;
  }

  public void EnableHitbox()
  {

    swordHitBox.EnableSwordCollider();
  }
  public void DisableHitbox()
  {
    swordHitBox.DisableSwordCollider();
  }
}