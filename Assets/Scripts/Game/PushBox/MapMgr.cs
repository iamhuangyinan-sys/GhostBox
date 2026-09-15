using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public enum E_playerState
{
    ghost, // 幽灵状态
    on, // 待机
    inside, // 附身
}

public class MapMgr : SingletonAutoMono<MapMgr>
{
    private Node[,] nodes; // 当前地图
    private Node[,] originalNodes; // 原始地图
    private GameObject[,] mapObjs; // 地图对象
    private GameObject[,] originalMapObjs; // 原始地图对象
    public int mapW;
    public int mapH;

    [SerializeField] private int playerX; // 玩家坐标
    [SerializeField] private int playerY;
    private int originalPlayerX; // 原始玩家坐标
    private int originalPlayerY;
    [SerializeField] private E_playerState playerState = E_playerState.ghost; // 玩家状态

    [SerializeField] private int boxOnNum; // 当前地图上已经达到目标点的箱子数量
    [SerializeField] private int targetNum; // 地图上的目标点数量

    private Dictionary<Vector2Int, int> TargetDic = null; // 目标点坐标及其数字标记的字典
    private List<Vector2Int> spikes = null; // 尖刺坐标的列表

    private Dictionary<KeyCode, Vector2Int> playerMoveDic = null; // 移动方向的字典
    [SerializeField] private GameObject playerObj; // 玩家对象
    private GameObject ghostObj; // 幽灵对象
    private Transform levelTrans;
    private Transform groundTrans;
    private Node playerBoxNode = null; // 玩家所附身的箱子节点

    private bool isAnim = false; // 是否正在动画中
    private bool isWin = false; // 是否已经胜利
    public bool isPause = false; // 是否已经暂停
    public bool isGaming = false; // 是否正在游戏中


    public void Init(Node[,] nodes) // 初始化
    {
        levelTrans = GameObject.Find("Level").transform;
        groundTrans = GameObject.Find("Ground").transform;

        // 获取地图宽高
        mapW = nodes.GetLength(0);
        mapH = nodes.GetLength(1);

        // 初始化移动方向的字典
        if (playerMoveDic == null)
        {
            playerMoveDic = new Dictionary<KeyCode, Vector2Int>();
            playerMoveDic[KeyCode.W] = new Vector2Int(0, 1);
            playerMoveDic[KeyCode.A] = new Vector2Int(-1, 0);
            playerMoveDic[KeyCode.S] = new Vector2Int(0, -1);
            playerMoveDic[KeyCode.D] = new Vector2Int(1, 0);
        }

        this.nodes = nodes;
        originalNodes = (Node[,])nodes.Clone(); // 复制一份原始地图
        mapObjs = new GameObject[mapW, mapH];
        originalMapObjs = new GameObject[mapW, mapH];

        InitMap(); // 初始化地图

        // 初始化玩家坐标
        for (int i = 0; i < mapW; i++)
        {
            for (int j = 0; j < mapH; j++)
            {
                if (nodes[i, j].type == E_nodeType.ghost)
                {
                    playerX = i;
                    playerY = j;
                    originalPlayerX = i;
                    originalPlayerY = j;

                    nodes[i, j].type = E_nodeType.walk;
                    break;
                }
            }
        }

        // 初始化目标点数量
        targetNum = 0;
        for (int i = 0; i < mapW; i++)
        {
            for (int j = 0; j < mapH; j++)
            {
                if (nodes[i, j].type == E_nodeType.target)
                {
                    targetNum++;
                }
            }
        }
        boxOnNum = 0; // 初始化当前地图上已经达到目标点的箱子数量

        // 初始化目标点坐标及其数字标记的字典
        TargetDic = new Dictionary<Vector2Int, int>();
        for (int i = 0; i < mapW; i++)
        {
            for (int j = 0; j < mapH; j++)
            {
                if (nodes[i, j].type == E_nodeType.target)
                {
                    TargetDic.Add(new Vector2Int(i, j), nodes[i, j].targetIndex);
                    nodes[i, j] = new Node(E_nodeType.walk);
                }
            }
        }

        // 初始化尖刺坐标的列表
        spikes = new List<Vector2Int>();
        for (int i = 0; i < mapW; i++)
        {
            for (int j = 0; j < mapH; j++)
            {
                if (nodes[i, j].type == E_nodeType.spike)
                {
                    spikes.Add(new Vector2Int(i, j));
                    nodes[i, j] = new Node(E_nodeType.walk);
                }
            }
        }

        playerState = E_playerState.ghost; // 初始化玩家状态
        isAnim = false;
        isWin = false;
        isPause = false;
        isGaming = true;

    }

    private void InitMap()
    {
        GameObject temp = null;
        for (int i = 0; i < mapW; i++)
        {
            for (int j = 0; j < mapH; j++)
            {
                // 初始化地图对象
                if (nodes[i, j].type == E_nodeType.wall)
                {
                    temp = Instantiate(ResourcesMgr.Instance.Load<GameObject>("Prefab/Game/Wall"), new Vector3(i, 0, j), Quaternion.identity);
                    temp.transform.SetParent(levelTrans);
                }
                else if (nodes[i, j].type == E_nodeType.box)
                {
                    if (nodes[i, j].boxType == E_boxType.normal)
                    {
                        temp = Instantiate(ResourcesMgr.Instance.Load<GameObject>("Prefab/Game/NormalBox"), new Vector3(i, 0, j), Quaternion.identity);
                    }
                    else if (nodes[i, j].boxType == E_boxType.refuse)
                    {
                        temp = Instantiate(ResourcesMgr.Instance.Load<GameObject>("Prefab/Game/RefuseBox"), new Vector3(i, 0, j), Quaternion.identity);
                    }
                    else if (nodes[i, j].boxType == E_boxType.curse)
                    {
                        temp = Instantiate(ResourcesMgr.Instance.Load<GameObject>("Prefab/Game/CurseBox"), new Vector3(i, 0, j), Quaternion.identity);
                    }

                    if (nodes[i, j].targetIndex != -1)
                    {
                        Transform targetIndexTrans = temp.transform.Find("TargetIndex");
                        targetIndexTrans.gameObject.SetActive(true);
                        targetIndexTrans.GetComponent<TMP_Text>().text = nodes[i, j].targetIndex.ToString();
                        temp.transform.SetParent(levelTrans);
                    }

                    mapObjs[i, j] = temp;
                    originalMapObjs[i, j] = temp;
                }
                else if (nodes[i, j].type == E_nodeType.ghost)
                {
                    playerObj = Instantiate(ResourcesMgr.Instance.Load<GameObject>("Prefab/Game/Ghost"), new Vector3(i, 0, j), Quaternion.identity);
                    playerObj.transform.SetParent(levelTrans);
                    ghostObj = playerObj;
                }

                // 初始化地面对象
                if (nodes[i, j].type == E_nodeType.target)
                {
                    temp = Instantiate(ResourcesMgr.Instance.Load<GameObject>("Prefab/Ground/Target"), new Vector3(i, -1, j), Quaternion.identity);
                    Transform targetIndexTrans = temp.transform.Find("TargetIndex");
                    targetIndexTrans.gameObject.SetActive(true);
                    targetIndexTrans.GetComponent<TMP_Text>().text = nodes[i, j].targetIndex.ToString();
                    temp.transform.SetParent(groundTrans);
                }
                else if (nodes[i, j].type == E_nodeType.spike)
                {
                    temp = Instantiate(ResourcesMgr.Instance.Load<GameObject>("Prefab/Ground/Spike"), new Vector3(i, -1, j), Quaternion.identity);
                    temp.transform.SetParent(groundTrans);
                }
                else
                {
                    temp = Instantiate(ResourcesMgr.Instance.Load<GameObject>("Prefab/Ground/NormalGround"), new Vector3(i, -1, j), Quaternion.identity);
                    temp.transform.SetParent(groundTrans);
                }
            }
        }
    }

    private void Update()
    {
        if (!isAnim && !isWin && !isPause)
        {
            // 检测方向键
            KeyDown(KeyCode.W);
            KeyDown(KeyCode.A);
            KeyDown(KeyCode.S);
            KeyDown(KeyCode.D);
            // 检测重来键
            if (Input.GetKeyDown(KeyCode.R))
            {
                ReStart();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                if (playerState == E_playerState.on)
                {
                    GoIn();
                }
                else if (playerState == E_playerState.inside)
                {
                    GoOut();
                }
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                //PrintNodesType();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseOrContinue();
        }

    }

    public void KeyDown(KeyCode keyCode)
    {
        if (Input.GetKeyDown(keyCode) && playerMoveDic.ContainsKey(keyCode))
        {
            Vector2Int movev2 = playerMoveDic[keyCode];
            //playerObj.transform.LookAt(new Vector3(playerX + movev2.x, 0, playerY + movev2.y));

            // 前方格子
            Node movetoNode1 = nodes[playerX + movev2.x, playerY + movev2.y];

            // 如果是幽灵状态 或 待机状态
            if (playerState == E_playerState.ghost || playerState == E_playerState.on)
            {
                // 不是墙
                if (movetoNode1.type != E_nodeType.wall)
                {
                    ghostObj.transform.position = new Vector3(playerX, 0, playerY);
                    playerX += movev2.x;
                    playerY += movev2.y;
                    if (movetoNode1.type == E_nodeType.box || movetoNode1.type == E_nodeType.boxOnTarget)
                    {
                        //Debug.Log("撞到箱子 进入待机状态");
                        playerState = E_playerState.on;

                        // 更新地图 ----------------------------------
                        playerObj.SetActive(false);
                    }
                    else
                    {
                        //Debug.Log("移动玩家");
                        playerState = E_playerState.ghost;

                        // 更新地图 ----------------------------------
                        playerObj = ghostObj;
                        playerObj.SetActive(true);
                        // 玩家移动
                        StartCoroutine(Move(playerObj, playerX, playerY));
                    }

                }
                else
                {
                    //Debug.Log("撞墙");
                }
            }
            // 如果是附身状态
            else
            {
                // 推箱子
                if (movetoNode1.type != E_nodeType.wall) // 不是墙
                {
                    // 前方第二个格子
                    Node movetoNode2 = nodes[playerX + movev2.x * 2, playerY + movev2.y * 2];
                    // 可以推箱子
                    if ((movetoNode1.type == E_nodeType.box || movetoNode1.type == E_nodeType.boxOnTarget) && // 是箱子
                        movetoNode2.type != E_nodeType.wall && // 第二个格子不是墙
                        (movetoNode2.type != E_nodeType.box && movetoNode2.type != E_nodeType.boxOnTarget)) // 第二个格子不是箱子
                    {
                        //Debug.Log("可以推动");
                        nodes[playerX + movev2.x, playerY + movev2.y] = new Node(E_nodeType.walk);
                        nodes[playerX + movev2.x * 2, playerY + movev2.y * 2] = new Node(E_nodeType.box, movetoNode1.targetIndex, movetoNode1.boxType); // 第二个格子变成箱子

                        // 更新地图 ----------------------------------
                        // 箱子移动
                        StartCoroutine(Move(mapObjs[playerX + movev2.x, playerY + movev2.y], playerX + movev2.x * 2, playerY + movev2.y * 2));
                        mapObjs[playerX + movev2.x * 2, playerY + movev2.y * 2] = mapObjs[playerX + movev2.x, playerY + movev2.y];
                        mapObjs[playerX + movev2.x, playerY + movev2.y] = null;
                        playerX += movev2.x;
                        playerY += movev2.y;
                        // 玩家移动
                        StartCoroutine(Move(playerObj, playerX, playerY));

                        // 如果推入目标点
                        if (TargetDic.ContainsKey(new Vector2Int(playerX + movev2.x, playerY + movev2.y)) &&
                            TargetDic[new Vector2Int(playerX + movev2.x, playerY + movev2.y)] == movetoNode1.targetIndex)
                        {
                            //Debug.Log("推入目标点");
                            boxOnNum++;
                            nodes[playerX + movev2.x, playerY + movev2.y].type = E_nodeType.boxOnTarget;

                            if (boxOnNum == targetNum)
                            {
                                //Debug.Log("通关");
                                UIMgr.Instance.ShowPanel<FinishPanel>();
                                isWin = true;
                            }

                        }
                        // 如果推出目标点
                        else if (movetoNode1.type == E_nodeType.boxOnTarget)
                        {
                            //Debug.Log("推出目标点");
                            boxOnNum--;
                            nodes[playerX + movev2.x * 2, playerY + movev2.y * 2].type = E_nodeType.box;
                        }

                        // 如果推入尖刺
                        if (spikes.Contains(new Vector2Int(playerX + movev2.x, playerY + movev2.y)))
                        {
                            //Debug.Log("推入尖刺");
                            nodes[playerX + movev2.x, playerY + movev2.y].type = E_nodeType.walk;
                            mapObjs[playerX + movev2.x, playerY + movev2.y].SetActive(false);
                            mapObjs[playerX + movev2.x, playerY + movev2.y] = null;
                        }

                        // 如果进入目标点
                        if (TargetDic.ContainsKey(new Vector2Int(playerX, playerY)) &&
                            TargetDic[new Vector2Int(playerX, playerY)] == playerBoxNode.targetIndex)
                        {
                            //Debug.Log("进入目标点");
                            boxOnNum++;
                            nodes[playerX, playerY].type = E_nodeType.boxOnTarget;
                            playerBoxNode.type = E_nodeType.boxOnTarget;

                            if (boxOnNum == targetNum)
                            {
                                //Debug.Log("通关");
                                UIMgr.Instance.ShowPanel<FinishPanel>();
                                isWin = true;
                            }

                        }
                        // 如果走出目标点
                        else if (playerBoxNode.type == E_nodeType.boxOnTarget)
                        {
                            //Debug.Log("走出目标点");
                            nodes[playerX - movev2.x, playerY - movev2.y] = new Node(E_nodeType.walk);
                            boxOnNum--;
                            playerBoxNode.type = E_nodeType.box;

                        }

                    }
                    //可以直接走
                    else if (movetoNode1.type == E_nodeType.walk)
                    {
                        //Debug.Log("可以直接走");
                        playerX += movev2.x;
                        playerY += movev2.y;

                        // 更新地图 ----------------------------------
                        // 玩家移动
                        StartCoroutine(Move(playerObj, playerX, playerY));

                        // 如果进入目标点
                        if (TargetDic.ContainsKey(new Vector2Int(playerX, playerY)) &&
                            TargetDic[new Vector2Int(playerX, playerY)] == playerBoxNode.targetIndex)
                        {
                            //Debug.Log("进入目标点");
                            boxOnNum++;
                            nodes[playerX, playerY].type = E_nodeType.boxOnTarget;
                            playerBoxNode.type = E_nodeType.boxOnTarget;

                            if (boxOnNum == targetNum)
                            {
                                //Debug.Log("通关");
                                UIMgr.Instance.ShowPanel<FinishPanel>();
                                isWin = true;
                            }

                            // 更新地图 ----------------------------------
                        }
                        // 如果走出目标点
                        else if (playerBoxNode.type == E_nodeType.boxOnTarget)
                        {
                            //Debug.Log("走出目标点");
                            nodes[playerX - movev2.x, playerY - movev2.y] = new Node(E_nodeType.walk);
                            boxOnNum--;
                            playerBoxNode.type = E_nodeType.box;

                            // 更新地图 ----------------------------------
                        }

                        // 如果走入尖刺
                        if (spikes.Contains(new Vector2Int(playerX, playerY)))
                        {
                            playerBoxNode = null;
                            playerState = E_playerState.ghost;
                            playerObj.SetActive(false);
                            playerObj = ghostObj;
                            playerObj.SetActive(true);
                            nodes[playerX, playerY] = new Node(E_nodeType.walk);
                            playerObj.transform.position = new Vector3(playerX, 0, playerY);
                        }
                    }
                    else
                    {
                        //Debug.Log("不能推动");
                    }
                }
                else
                {
                    //Debug.Log("撞墙");
                }
            }


        }
    }

    public void ReStart()
    {
        //Debug.Log("重新开始");

        // 重置玩家状态
        playerState = E_playerState.ghost;
        playerObj = ghostObj;
        playerObj.SetActive(true);
        playerBoxNode = null;

        // 重置节点地图
        nodes = (Node[,])originalNodes.Clone();

        // 重置箱子计数
        boxOnNum = 0;

        // 重置地图对象位置
        for (int i = 0; i < mapW; i++)
        {
            for (int j = 0; j < mapH; j++)
            {
                if (originalMapObjs[i, j] != null)
                {
                    // 重置箱子位置
                    originalMapObjs[i, j].transform.position = new Vector3(i, 0, j);
                    mapObjs[i, j] = originalMapObjs[i, j];
                    mapObjs[i, j].SetActive(true);
                }
                else
                {
                    mapObjs[i, j] = null;
                }
            }
        }

        for (int i = 0; i < mapW; i++)
        {
            for (int j = 0; j < mapH; j++)
            {
                if (nodes[i, j].type == E_nodeType.spike)
                {
                    nodes[i, j] = new Node(E_nodeType.walk);
                }
            }
        }

        //移动玩家到初始位置
        playerX = originalPlayerX;
        playerY = originalPlayerY;
        playerObj.transform.position = new Vector3(originalPlayerX, 0, originalPlayerY);

        // 重置目标点状态
        foreach (var target in TargetDic)
        {
            int x = target.Key.x;
            int y = target.Key.y;
            nodes[x, y] = new Node(E_nodeType.walk);
        }

        isAnim = false;
        isWin = false;
        isPause = false;
    }

    /// <summary>
    /// 附身
    /// </summary>
    private void GoIn()
    {
        if (nodes[playerX, playerY].boxType != E_boxType.refuse)
        {
            //Debug.Log("附身成功");
            playerState = E_playerState.inside;
            playerObj = mapObjs[playerX, playerY];
            playerBoxNode = nodes[playerX, playerY];
            nodes[playerX, playerY] = new Node(E_nodeType.walk);
        }
        else
        {
            //Debug.Log("不能附身");
        }
    }

    /// <summary>
    /// 脱出
    /// </summary>
    private void GoOut()
    {
        if (playerBoxNode.boxType != E_boxType.curse)
        {
            //Debug.Log("脱离成功");
            playerState = E_playerState.on;
            mapObjs[playerX, playerY] = playerObj;
            playerObj = ghostObj;
            nodes[playerX, playerY] = playerBoxNode;
            playerBoxNode = null;
        }
        else
        {
            //Debug.Log("不能脱离");
        }
    }

    private void PauseOrContinue()
    {
        if (isGaming)
        {
            if (isPause)
            {
                isPause = false;
                UIMgr.Instance.HidePanel<MenuPanel>();
            }
            else
            {
                isPause = true;
                UIMgr.Instance.ShowPanel<MenuPanel>();
            }
        }
    }

    private IEnumerator Move(GameObject obj, int x, int y)
    {
        isAnim = true;
        obj.transform.DOMove(new Vector3(x, 0, y), 0.1f);
        yield return new WaitForSeconds(0.1f);
        isAnim = false;
    }

#if UNITY_EDITOR
    private void PrintNodesType()
    {
        for (int i = 0; i < mapW; i++)
        {
            for (int j = 0; j < mapH; j++)
            {
                Debug.LogWarning(nodes[i, j].type);
            }
            Debug.LogWarning("-----------------------");
        }
    }
#endif

}
