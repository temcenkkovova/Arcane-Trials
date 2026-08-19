using UnityEngine;
public class BossTrigger : MonoBehaviour
{

  private void OnTriggerEnter(Collider other)
  {
    if (other.TryGetComponent(out BossEntrance bossEntrance))
      bossEntrance.JumpOnPosition();
  }
}
