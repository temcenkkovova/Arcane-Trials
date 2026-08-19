using DG.Tweening;
using UnityEngine;

public class BossAttackArea : MonoBehaviour
{

  [Header("Visuals")]
  [SerializeField] private Transform fill;
  [SerializeField] private Transform ring;
  [SerializeField] private Transform pulseRing;

  [SerializeField] private Renderer fillRenderer;
  [SerializeField] private Renderer ringRenderer;
  [SerializeField] private Renderer pulseRenderer;
  private Material fillMaterial;
  private Material ringMaterial;
  private Material pulseMaterial;

  private Vector3 fillFinalScale;
  private Vector3 ringInitialScale;
  private Vector3 pulseInitialScale;

  private Sequence warningSequence;
  private Sequence pulseSequence;

  [Header("Colors")]
  [SerializeField]
  private Color fillColor =
      new Color(1f, 0f, 0f, 0.35f);

  [SerializeField]
  private Color ringColor =
      new Color(1f, 0.05f, 0f, 0.9f);

  [Header("Animation")]
  [SerializeField] private float pulseScale = 1.2f;
  [SerializeField] private float impactScale = 1.15f;
  [SerializeField] private float disappearDuration = 0.2f;

  private Transform target;
  private float Radius => config.attackAreaRadius;
  private float Damage => config.damage;
  private BossAttackConfig config;
  private void Awake()
  {
    // renderer.material создаёт отдельную копию материала
    // для конкретного экземпляра зоны.
    fillMaterial = fillRenderer.material;
    ringMaterial = ringRenderer.material;
    pulseMaterial = pulseRenderer.material;

    fillFinalScale = fill.localScale;
    ringInitialScale = ring.localScale;
    pulseInitialScale = pulseRing.localScale;
  }
  public void Init(BossAttackConfig config, Transform target)
  {
    this.target = target;
    this.config = config;

    SetRadius(config.attackAreaRadius);
    PlayWarning(1);
    transform.localScale =
        new Vector3(Radius * 2f, 1f, Radius * 2f);
  }
  private void PlayWarning(float duration)
  {
    KillTweens();

    fill.gameObject.SetActive(true);
    ring.gameObject.SetActive(true);
    pulseRing.gameObject.SetActive(true);

    fill.localScale = Vector3.zero;
    ring.localScale = ringInitialScale;
    pulseRing.localScale = pulseInitialScale;

    SetMaterialColor(fillMaterial, fillColor);
    SetMaterialColor(ringMaterial, ringColor);

    Color pulseColor = ringColor;
    pulseColor.a = 0.45f;
    SetMaterialColor(pulseMaterial, pulseColor);

    // Заполнение зоны до момента удара.
    warningSequence = DOTween.Sequence();

    warningSequence.Append(
        fill.DOScale(fillFinalScale, duration)
            .SetEase(Ease.Linear)
    );

    // Небольшое дрожание кольца перед ударом.
    warningSequence.Join(
        ring.DOScale(
                ringInitialScale * 1.04f,
                0.12f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
    );

    // Расходящееся внешнее кольцо.
    pulseSequence = DOTween.Sequence();

    pulseSequence.Append(
        pulseRing.DOScale(
                pulseInitialScale * pulseScale,
                0.6f)
            .SetEase(Ease.OutQuad)
    );

    pulseSequence.Join(
        pulseMaterial.DOFade(0f, 0.6f)
    );

    pulseSequence.AppendCallback(() =>
    {
      pulseRing.localScale = pulseInitialScale;

      Color color = ringColor;
      color.a = 0.45f;
      SetMaterialColor(pulseMaterial, color);
    });

    pulseSequence.SetLoops(-1, LoopType.Restart);
  }
  private void SetRadius(float radius)
  {
    float diameter = radius * 2f;
    transform.localScale =
        new Vector3(diameter, diameter, diameter);
  }

  public void ApplyDamage()
  {

    if (target == null)
      return;

    Vector3 offset = target.position - transform.position;
    offset.y = 0f;

    if (offset.sqrMagnitude <= Radius * Radius)
    {
      if (target.TryGetComponent(out PlayerHealth health))
        health.TakeDamage(Damage);
    }

    Destroy(gameObject);
  }
  private void PlayImpact()
  {
    KillTweens();

    Sequence impactSequence = DOTween.Sequence();

    impactSequence.Append(
        ring.DOScale(
                ringInitialScale * impactScale,
                0.1f)
            .SetEase(Ease.OutQuad)
    );

    impactSequence.Join(
        fill.DOScale(
                fillFinalScale * impactScale,
                0.1f)
            .SetEase(Ease.OutQuad)
    );

    impactSequence.Append(
        fillMaterial.DOFade(0f, disappearDuration)
    );

    impactSequence.Join(
        ringMaterial.DOFade(0f, disappearDuration)
    );

    impactSequence.Join(
        pulseMaterial.DOFade(0f, disappearDuration)
    );

    impactSequence.OnComplete(() =>
    {
      Destroy(gameObject);
    });
  }

  private void SetMaterialColor(
      Material material,
      Color color)
  {
    material.color = color;
  }

  private void KillTweens()
  {
    warningSequence?.Kill();
    pulseSequence?.Kill();

    fill?.DOKill();
    ring?.DOKill();
    pulseRing?.DOKill();

    fillMaterial?.DOKill();
    ringMaterial?.DOKill();
    pulseMaterial?.DOKill();
  }

  private void OnDestroy()
  {
    KillTweens();

    if (fillMaterial != null)
      Destroy(fillMaterial);

    if (ringMaterial != null)
      Destroy(ringMaterial);

    if (pulseMaterial != null)
      Destroy(pulseMaterial);
  }
}