using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 控制鬼魂的移动。现在附身/解附身逻辑由InputManager通过调用其公共方法来触发。
/// </summary>
public class GhostMove : MonoBehaviour
{
    [Header("移动设置")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float moveDistance = 10f;

    [Header("附身设置")]
    [Tooltip("鬼魂可以附身的范围")]
    [SerializeField] private float possessRadius = 2f;
    [SerializeField] private LayerMask boxLayer;

    // 内部状态变量
    private Vector3 targetPosition;
    private Vector3 Pos;
    private bool isMoving = false;
    // 不再需要 currentPossessedBox 变量，因为状态由InputManager管理

    #region 地图与移动逻辑 (与你原来的一样，保持不变)
    public struct Point { public int id, boxindex, placeindex; public Point(int ID, int b, int p) { id = ID; boxindex = b; placeindex = p; } };
    Point[,] level1 = { { new Point(0, 0, 0), new Point(0, 0, 0), new Point(0, 0, 0), new Point(0, 0, 0), new Point(0, 0, 0), new Point(0, 0, 0), new Point(0, 0, 0) }, { new Point(0, 0, 0), new Point(3, 0, 2), new Point(2, 1, 0), new Point(2, 2, 0), new Point(1, 0, 0), new Point(3, 0, 1), new Point(0, 0, 0) }, { new Point(0, 0, 0), new Point(0, 0, 0), new Point(0, 0, 0), new Point(0, 0, 0), new Point(1, 0, 0), new Point(0, 0, 0), new Point(0, 0, 0) } };
    private bool isCouldMove(Vector3 nextPos) { int x = ((int)nextPos.x) / 10; int y = ((int)nextPos.z) / 10; if (x < 0 || x >= level1.GetLength(0) || y < 0 || y >= level1.GetLength(1)) return false; return level1[x, y].id != 0; }
    private void HandleGhostMovement() { if (!isMoving) { Vector3 moveDir = Vector3.zero; if (Input.GetKeyDown(KeyCode.W)) moveDir = new Vector3(-moveDistance, 0, 0); else if (Input.GetKeyDown(KeyCode.S)) moveDir = new Vector3(moveDistance, 0, 0); else if (Input.GetKeyDown(KeyCode.A)) moveDir = new Vector3(0, 0, -moveDistance); else if (Input.GetKeyDown(KeyCode.D)) moveDir = new Vector3(0, 0, moveDistance); if (moveDir != Vector3.zero) { targetPosition = transform.position + moveDir; if (isCouldMove(targetPosition)) isMoving = true; } } if (isMoving) { transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime); if (Vector3.Distance(transform.position, targetPosition) < 0.01f) { transform.position = targetPosition; isMoving = false; } } }
    #endregion

    void Start()
    {
        targetPosition = transform.position;
        // 【关键】在游戏开始时，向InputManager注册自己
        if (InputManager.Instance != null)
        {
            InputManager.Instance.RegisterGhost(this);
        }
    }

    void Update()
    {
        // 只处理移动逻辑。不再检测E键。
        HandleGhostMovement();
    }

    #region 公开的附身/解附身方法

    /// <summary>
    /// 尝试附身一个箱子（由InputManager调用）。
    /// </summary>
    public void TryToPossess()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, possessRadius, boxLayer);
        BoxController closestBox = colliders
            .Select(col => col.GetComponent<BoxController>())
            .Where(box => box != null)
            .OrderBy(box => Vector3.Distance(transform.position, box.transform.position))
            .FirstOrDefault();

        if (closestBox != null)
        {
            // 【核心】调用箱子的方法，让它变为受控状态
            closestBox.SetControlled(true);

            // 【关键】通知InputManager，状态已改变
            InputManager.Instance.SetPossessedBox(closestBox);

            // 附身后禁用鬼魂对象
            gameObject.SetActive(false);
            Debug.Log("成功附身到: " + closestBox.name);
        }
        else
        {
            Debug.Log("附近没有可以附身的箱子。");
        }
    }

    /// <summary>
    /// 从当前箱子解除附身（由InputManager调用）。
    /// </summary>
    public void UnpossessBox()
    {
        // 从InputManager获取当前附身的箱子
        BoxController boxToUnpossess = InputManager.Instance.possessedBox; // 这是个小改动，确保我们操作的是正确的箱子
        if (boxToUnpossess == null) return;

        // 【核心】调用箱子的方法，让它变回不受控状态
        boxToUnpossess.SetControlled(false);

        // 将鬼魂重新显示在箱子旁边
        transform.position = boxToUnpossess.transform.position + new Vector3(0, 0, -moveDistance);
        targetPosition = transform.position; // 更新目标位置
        gameObject.SetActive(true);

        // 【关键】通知InputManager，状态已清空
        InputManager.Instance.ClearPossessedBox();

        Debug.Log("从 " + boxToUnpossess.name + " 解除附身。");
    }
    #endregion
}