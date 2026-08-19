using UnityEngine;

public class BossAttackManager : MonoBehaviour
{
  private BossAttackConfig attackConfig;

  [SerializeField] private BossAttackArea bossAttackAreaPrefab;
  private EnemyAnimationsController enemyAnimationsController;
  public bool isAttacking { get; private set; }
  private float lastAttackTime;

  private BossAttackArea spawnedAreaCircle;


  void Awake()
  {
    enemyAnimationsController = GetComponent<EnemyAnimationsController>();
  }

  public void Init(BossAttackConfig config)
  {
    attackConfig = config;
  }
  public void Attack(Transform targetTr)
  {

    if (targetTr == null) return;

    Vector3 attackPos = targetTr.position;
    attackPos.y = 0.06f;

    spawnedAreaCircle = Instantiate(bossAttackAreaPrefab, attackPos, Quaternion.identity);
    spawnedAreaCircle.Init(attackConfig, targetTr);
    isAttacking = true;
    enemyAnimationsController.PlayAttackAnimation();

  }

  public bool CanAttack()
  {
    return Time.time >= lastAttackTime + attackConfig.attackCooldawn;
  }

  public void HandleFinishAttack()
  {
    lastAttackTime = Time.time;
    isAttacking = false;
    spawnedAreaCircle.ApplyDamage();
  }
  void OnDisable()
  {
    spawnedAreaCircle = null;
    enemyAnimationsController.OnFinishAttack -= HandleFinishAttack;
  }

  void OnEnable()
  {
    spawnedAreaCircle = null;
    isAttacking = false;
    enemyAnimationsController.OnFinishAttack += HandleFinishAttack;
  }

}