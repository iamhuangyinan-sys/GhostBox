using UnityEngine;

public class BoxController : MonoBehaviour
{
    [Header("移动设置")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float moveDistance = 10f;

    [Header("状态")]
    [Tooltip("箱子是否正被玩家控制")]
    private bool isControlled = false;

    private Vector3 targetPosition;
    private bool isMoving = false;

    // --- 地图数据 ---
    // 为了让箱子也能检测墙壁，我们需要在这里也定义地图结构。
    // 在更复杂的项目中，推荐将地图数据放在一个单独的管理器脚本（如LevelManager）中，供所有需要它的对象读取。
    public struct Point
    {
        public int id; //0墙，1空格，2箱子，3结束点
        public int boxindex;
        public int placeindex;

        public Point(int ID, int BOXINDEX, int PLACEINDEX)
        {
            id = ID;
            boxindex = BOXINDEX;
            placeindex = PLACEINDEX;
        }
    };

    // 和 GhostMove.cs 中完全相同的地图数据
    Point[,] level1 =
    {
        { new Point(0,0,0), new Point(0,0,0), new Point(0,0,0),new Point(0,0,0), new Point(0,0,0),new Point(0,0,0), new Point(0,0,0) },
        { new Point(0,0,0), new Point(3,0,2), new Point(2,1,0), new Point(2,2,0), new Point(1,0,0), new Point(3,0,1), new Point(0,0,0) },
        { new Point(0,0,0),new Point(0,0,0),new Point(0,0,0), new Point(0,0,0), new Point(1,0,0),new Point(0,0,0), new Point(0,0,0) }
    };
    // --- 结束地图数据 ---


    void Start()
    {
        // 初始化目标位置为当前位置，防止游戏开始时发生位移
        targetPosition = transform.position;
    }

    void Update()
    {
        // 只有在被控制的状态下，才处理移动逻辑
        if (isControlled)
        {
            HandleBoxMovement();
        }
    }

    /// <summary>
    /// 公开接口：用于从外部（例如GhostMove脚本）设置箱子是否被控制。
    /// </summary>
    /// <param name="controlled">是否被控制</param>
    public void SetControlled(bool controlled)
    {
        isControlled = controlled;
    }

    /// <summary>
    /// 检查目标位置是否可以移动（不是墙）。
    /// </summary>
    private bool isCouldMove(Vector3 nextPos)
    {
        // 这个坐标转换逻辑需要和你的 GhostMove.cs 脚本以及场景设置完全一致
        int x = Mathf.RoundToInt(nextPos.x / 10f);
        int y = Mathf.RoundToInt(nextPos.z / 10f);

        // 边界检查，防止数组越界
        if (x < 0 || x >= level1.GetLength(0) || y < 0 || y >= level1.GetLength(1))
        {
            return false;
        }

        // 检查目标格子的ID是否为0（墙）
        return level1[x, y].id != 0;
    }

    /// <summary>
    /// 处理箱子的移动逻辑，与鬼魂的移动逻辑几乎完全相同。
    /// </summary>
    private void HandleBoxMovement()
    {
        // 1. 如果不在移动中，则检测玩家输入
        if (!isMoving)
        {
            Vector3 moveDirection = Vector3.zero;

            if (Input.GetKeyDown(KeyCode.W))
            {
                moveDirection = new Vector3(-moveDistance, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                moveDirection = new Vector3(moveDistance, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                moveDirection = new Vector3(0, 0, -moveDistance);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                moveDirection = new Vector3(0, 0, moveDistance);
            }

            // 如果有有效输入
            if (moveDirection != Vector3.zero)
            {
                // 设置目标位置
                targetPosition = transform.position + moveDirection;

                // 【已更新】在移动前，检查目标位置是否是墙
                if (isCouldMove(targetPosition))
                {
                    isMoving = true;
                }
            }
        }

        // 2. 如果正在移动，则平滑地移动到目标位置
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 当非常接近目标点时，直接将位置设置为目标点，并停止移动
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }
}
