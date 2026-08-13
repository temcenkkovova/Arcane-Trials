using DG.Tweening;
using UnityEngine;

public class EnemyDeathFeedback : MonoBehaviour
{
  [SerializeField] private Transform visual;

  public void Play(System.Action onComplete)
  {
    Sequence sequence = DOTween.Sequence();

    sequence.Append(
        visual.DOScale(1.15f, 0.1f)
    );

    sequence.Append(
        visual.DOScale(0f, 0.3f)
    );

    sequence.OnComplete(() =>
    {
      onComplete?.Invoke();
    });
  }
}