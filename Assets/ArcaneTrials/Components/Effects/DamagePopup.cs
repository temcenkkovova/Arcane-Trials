using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
  [SerializeField] private TMP_Text damageText;
  [SerializeField] private CanvasGroup canvasGroup;

  public void Show(float damage, Color color)
  {
    damageText.color = color;
    damageText.text = $"-{Mathf.RoundToInt(damage)}";

    transform.DOKill();
    canvasGroup.DOKill();

    canvasGroup.alpha = 1f;

    Vector3 startPosition = transform.position;
    Vector3 endPosition = startPosition + Vector3.up * 1.5f;

    Sequence sequence = DOTween.Sequence();

    sequence.Join(
      transform
        .DOMove(endPosition, 0.5f)
        .SetEase(Ease.OutQuad)
    );

    sequence.Join(
      canvasGroup
        .DOFade(0f, 0.35f)
        .SetDelay(0.15f)
        .SetEase(Ease.InQuad)
    );

    sequence.Join(
      transform.DOPunchScale(
        Vector3.one * 0.15f,
        0.25f,
        5,
        0.5f
      )
    );

    sequence
      .OnComplete(() => Destroy(gameObject))
      .SetLink(gameObject);
  }
}