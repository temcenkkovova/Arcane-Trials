using DG.Tweening;
using UnityEngine;

public class SlashVFX : MonoBehaviour
{
  [SerializeField] private float lifeTime = 0.5f;


  private void Start()
  {
    transform.localScale = Vector3.zero;

    transform
        .DOScale(1f, lifeTime)
        .SetEase(Ease.OutQuad)
        .OnComplete(() =>
        {
          Destroy(gameObject);
        });
  }
}