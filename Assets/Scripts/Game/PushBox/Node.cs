using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_nodeType
{
    wall, // 墙
    walk, // 空白
    box, // 箱子
    ghost, // 鬼魂
    target, // 目标点
    boxOnTarget, // 箱子在目标点
    spike, // 尖刺
}

public enum E_boxType
{
    none, // 不是箱子
    normal, // 普通箱子
    refuse, // 拒绝箱子
    curse, // 诅咒箱子
    
}

public class Node
{
    public E_nodeType type;
    public int targetIndex; // 目标点标识 -1为无
    public E_boxType boxType; // 箱子类型

    public Node(E_nodeType type, int targetIndex = -1, E_boxType boxType = E_boxType.none)
    {
        this.type = type;
        this.targetIndex = targetIndex;
        this.boxType = boxType;
    }
}
