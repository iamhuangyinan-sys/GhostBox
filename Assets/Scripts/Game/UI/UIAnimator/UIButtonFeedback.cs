using UnityEngine;
using UnityEngine.EventSystems; // 引入事件系统命名空间
using DG.Tweening; // 引入 DOTween 命名空间

// 这个脚本需要挂载到一个带有 AudioSource 组件的对象上，如果没有会自动添加
[RequireComponent(typeof(AudioSource))]
public class UIButtonFeedback : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("动效目标")]
    [Tooltip("动画作用的目标，默认为本对象。可以指定为子对象，例如按钮的图标。")]
    [SerializeField] private RectTransform targetTransform;

    [Header("缩放动效")]
    [SerializeField] private bool enableScaleEffect = true;
    [Tooltip("鼠标悬停时放大的倍数")]
    [SerializeField] private float hoverScale = 1.05f;
    [Tooltip("鼠标点击时缩小的倍数")]
    [SerializeField] private float clickScale = 0.95f;
    [Tooltip("动画播放的时长")]
    [SerializeField] private float animationDuration = 0.1f;

    [Header("音效反馈")]
    [SerializeField] private bool enableSoundEffect = true;
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;

    private AudioSource audioSource;
    private Vector3 originalScale;
    private bool isPointerOver = false; // 标记指针是否在按钮上

    void Awake()
    {
        // 如果没有指定目标，则默认为自身
        if (targetTransform == null)
        {
            targetTransform = GetComponent<RectTransform>();
        }

        // 初始化 AudioSource
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false; // 禁止在唤醒时播放

        // 记录原始缩放大小
        originalScale = targetTransform.localScale;
    }

    /// <summary>
    /// 当指针按下时调用
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!enableScaleEffect) return;

        // 播放点击缩放动画
        targetTransform.DOKill(); // 先停止之前的动画，防止冲突
        targetTransform.DOScale(originalScale * clickScale, animationDuration).SetEase(Ease.OutCubic);

        // 播放点击音效
        PlaySound(clickSound);
    }

    /// <summary>
    /// 当指针抬起时调用（无论是在按钮上还是在按钮外抬起）
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!enableScaleEffect) return;

        // 根据指针是否还在按钮上，恢复到悬停状态或原始状态
        Vector3 targetScale = isPointerOver ? originalScale * hoverScale : originalScale;

        targetTransform.DOKill();
        targetTransform.DOScale(targetScale, animationDuration).SetEase(Ease.OutCubic);
    }

    /// <summary>
    /// 当指针进入按钮范围时调用
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;
        if (!enableScaleEffect) return;

        // 播放悬停缩放动画
        targetTransform.DOKill();
        targetTransform.DOScale(originalScale * hoverScale, animationDuration).SetEase(Ease.OutCubic);

        // 播放悬停音效
        PlaySound(hoverSound);
    }

    /// <summary>
    /// 当指针离开按钮范围时调用
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;
        if (!enableScaleEffect) return;

        // 恢复到原始大小
        targetTransform.DOKill();
        targetTransform.DOScale(originalScale, animationDuration).SetEase(Ease.OutCubic);
    }

    private void PlaySound(AudioClip clip)
    {
        if (enableSoundEffect && clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
