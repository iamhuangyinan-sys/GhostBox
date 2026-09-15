using UnityEngine;

/// <summary>
/// 一个单例模式的全局输入管理器。
/// 它负责监听通用输入（如E键），并根据当前游戏状态调用相应的功能。
/// </summary>
public class InputManager : MonoBehaviour
{
    // --- 单例模式 ---
    public static InputManager Instance { get; private set; }

    // --- 状态引用 ---
    private GhostMove ghostController;
    public BoxController possessedBox;

    private void Awake()
    {
        // 实现单例模式，确保场景中只有一个InputManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // 如果你的游戏有多个场景，可以取消这行注释
        }
    }

    void Update()
    {
        // 始终监听 E 键的按下事件
        if (Input.GetKeyDown(KeyCode.E))
        {
            // 如果当前没有附身任何箱子，就尝试附身
            if (possessedBox == null)
            {
                // 确保鬼魂控制器存在
                if (ghostController != null && ghostController.gameObject.activeInHierarchy)
                {
                    ghostController.TryToPossess();
                }
            }
            // 如果已经附身了，就解除附身
            else
            {
                // 即使鬼魂对象被禁用了，我们依然可以通过它的引用来调用其方法
                if (ghostController != null)
                {
                    ghostController.UnpossessBox();
                }
            }
        }
    }

    #region 公开接口

    /// <summary>
    /// 注册鬼魂控制器。
    /// </summary>
    public void RegisterGhost(GhostMove ghost)
    {
        this.ghostController = ghost;
    }

    /// <summary>
    /// 当鬼魂成功附身一个箱子时，调用此方法来更新状态。
    /// </summary>
    public void SetPossessedBox(BoxController box)
    {
        this.possessedBox = box;
    }

    /// <summary>
    /// 当鬼魂解除附身时，调用此方法来清空状态。
    /// </summary>
    public void ClearPossessedBox()
    {
        this.possessedBox = null;
    }

    #endregion
}
