using UnityEngine;
using DG.Tweening;

// 定义动效类型的枚举
public enum AnimationType
{
    None,
    Fade,
    Scale,
    SlideFromLeft,
    SlideFromRight,
    SlideFromTop,
    SlideFromBottom
}

public class UIPanelAnimator : MonoBehaviour
{
    [Header("入场动画")]
    [SerializeField] private AnimationType inAnimationType = AnimationType.None;
    [SerializeField] private float inDuration = 0.5f;
    [SerializeField] private float inDelay = 0f;
    [SerializeField] private Ease inEase = Ease.OutCubic;

    [Header("出场动画")]
    [SerializeField] private AnimationType outAnimationType = AnimationType.None;
    [SerializeField] private float outDuration = 0.5f;
    [SerializeField] private float outDelay = 0f;
    [SerializeField] private Ease outEase = Ease.InCubic;

    [Header("设置")]
    [SerializeField] private bool playOnEnable = true; // 是否在激活时自动播放入场动画

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 startAnchoredPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        // 如果没有CanvasGroup，自动添加一个
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        startAnchoredPosition = rectTransform.anchoredPosition;
    }

    void OnEnable()
    {
        if (playOnEnable)
        {
            AnimateIn();
        }
    }

    // 公开方法，用于外部调用播放入场动画
    public void AnimateIn()
    {
        PlayAnimation(inAnimationType, inDuration, inDelay, inEase, true);
    }

    // 公开方法，用于外部调用播放出场动画
    public void AnimateOut()
    {
        PlayAnimation(outAnimationType, outDuration, outDelay, outEase, false);
    }

    private void PlayAnimation(AnimationType type, float duration, float delay, Ease ease, bool isEnter)
    {
        // 先杀死之前可能存在的相同对象的动画，防止冲突
        transform.DOKill();

        switch (type)
        {
            case AnimationType.Fade:
                canvasGroup.alpha = isEnter ? 0 : 1;
                canvasGroup.DOFade(isEnter ? 1 : 0, duration).SetDelay(delay).SetEase(ease);
                break;

            case AnimationType.Scale:
                transform.localScale = isEnter ? Vector3.zero : Vector3.one;
                transform.DOScale(isEnter ? 1 : 0, duration).SetDelay(delay).SetEase(ease);
                break;

            case AnimationType.SlideFromLeft:
                rectTransform.anchoredPosition = isEnter ? new Vector2(-Screen.width, startAnchoredPosition.y) : startAnchoredPosition;
                rectTransform.DOAnchorPos(isEnter ? startAnchoredPosition : new Vector2(-Screen.width, startAnchoredPosition.y), duration).SetDelay(delay).SetEase(ease);
                break;

            case AnimationType.SlideFromRight:
                rectTransform.anchoredPosition = isEnter ? new Vector2(Screen.width, startAnchoredPosition.y) : startAnchoredPosition;
                rectTransform.DOAnchorPos(isEnter ? startAnchoredPosition : new Vector2(Screen.width, startAnchoredPosition.y), duration).SetDelay(delay).SetEase(ease);
                break;

            case AnimationType.SlideFromTop:
                rectTransform.anchoredPosition = isEnter ? new Vector2(startAnchoredPosition.x, Screen.height) : startAnchoredPosition;
                rectTransform.DOAnchorPos(isEnter ? startAnchoredPosition : new Vector2(startAnchoredPosition.x, Screen.height), duration).SetDelay(delay).SetEase(ease);
                break;

            case AnimationType.SlideFromBottom:
                rectTransform.anchoredPosition = isEnter ? new Vector2(startAnchoredPosition.x, -Screen.height) : startAnchoredPosition;
                rectTransform.DOAnchorPos(isEnter ? startAnchoredPosition : new Vector2(startAnchoredPosition.x, -Screen.height), duration).SetDelay(delay).SetEase(ease);
                break;
        }

        // 如果是出场动画，通常在动画结束后禁用对象
        if (!isEnter)
        {
            DOVirtual.DelayedCall(duration + delay, () => gameObject.SetActive(false));
        }
    }
}
