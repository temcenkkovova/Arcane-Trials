using DG.Tweening;
using UnityEngine;

public class HitFeedback : MonoBehaviour
{
  [SerializeField] private Transform visual;



  public void PlayHit()
  {
    visual.DOKill();
    visual.localScale = Vector3.one;
    visual.DOPunchScale(Vector3.one * 0.15f, 0.15f, 6, 0.5f);
  }
}