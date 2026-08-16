using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class CompletedSealProgressUI : MonoBehaviour
{
  public ArenaSealProgress sealProgress;
  public Image bossImage;

  [ColorPalette]
  public Color ColorOptions;

  [Header("Completion Animation")]
  [SerializeField, Min(0.01f)] private float colorDuration = 0.45f;
  [SerializeField, Min(0f)] private float punchScale = 0.25f;
  [SerializeField, Min(0.01f)] private float punchDuration = 0.65f;
  [SerializeField, Min(0f)] private float shakeAngle = 7f;
  [SerializeField, Min(1f)] private float pulseScale = 1.08f;
  [SerializeField, Min(0.01f)] private float pulseDuration = 0.7f;


  [SerializeField] private ParticleSystem[] completionParticles;

  private RectTransform bossRect;
  private Vector3 initialScale;
  private Sequence completionSequence;
  private Tween pulseTween;
  private bool isCompleted;

  private void Awake()
  {
    if (bossImage == null) return;

    bossRect = bossImage.rectTransform;
    initialScale = bossRect.localScale;
  }

  private void OnEnable()
  {
    if (sealProgress == null) return;
    sealProgress.OnCompletedProgress += HandleCompletedProgress;
  }

  private void OnDisable()
  {
    if (sealProgress != null)
      sealProgress.OnCompletedProgress -= HandleCompletedProgress;

    KillTweens();
  }

  private void HandleCompletedProgress()
  {
    if (isCompleted || bossImage == null) return;
    isCompleted = true;

    bossRect ??= bossImage.rectTransform;
    if (initialScale == Vector3.zero)
      initialScale = bossRect.localScale;

    KillTweens();
    PlayParticles();

    bossRect.localScale = initialScale;
    bossRect.localRotation = Quaternion.identity;

    completionSequence = DOTween.Sequence()
      .SetUpdate(true)
      .SetLink(gameObject)
      .Append(bossImage.DOColor(ColorOptions, colorDuration).SetEase(Ease.OutCubic))
      .Join(bossRect.DOPunchScale(Vector3.one * punchScale, punchDuration, 8, 0.45f))
      .Join(bossRect.DOPunchRotation(new Vector3(0f, 0f, shakeAngle), punchDuration, 10, 0.5f))
      .OnComplete(StartPulse);
  }

  private void StartPulse()
  {
    bossRect.localScale = initialScale;
    pulseTween = bossRect
      .DOScale(initialScale * pulseScale, pulseDuration)
      .SetEase(Ease.InOutSine)
      .SetLoops(-1, LoopType.Yoyo)
      .SetUpdate(true)
      .SetLink(gameObject);
  }

  private void PlayParticles()
  {
    if (completionParticles == null) return;

    foreach (ParticleSystem particles in completionParticles)
    {
      if (particles != null)
        particles.Play(true);
    }
  }

  private void KillTweens()
  {
    completionSequence?.Kill();
    pulseTween?.Kill();
    completionSequence = null;
    pulseTween = null;

    if (bossImage != null)
      bossImage.DOKill();

    if (bossRect != null)
      bossRect.DOKill();
  }
}
