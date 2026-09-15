using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMgr : Singleton<LevelMgr>
{
    private List<Node[,]> levels = new List<Node[,]>();

    private LevelMgr()
    {
        // 第1关
        Node[,] map = new Node[7, 5];
        map[0, 0] = new Node(E_nodeType.wall);
        map[1, 0] = new Node(E_nodeType.wall);
        map[2, 0] = new Node(E_nodeType.wall);
        map[3, 0] = new Node(E_nodeType.wall);
        map[4, 0] = new Node(E_nodeType.wall);
        map[5, 0] = new Node(E_nodeType.wall);
        map[6, 0] = new Node(E_nodeType.wall);

        map[0, 1] = new Node(E_nodeType.wall);
        map[1, 1] = new Node(E_nodeType.walk);
        map[2, 1] = new Node(E_nodeType.walk);
        map[3, 1] = new Node(E_nodeType.walk);
        map[4, 1] = new Node(E_nodeType.walk);
        map[5, 1] = new Node(E_nodeType.walk);
        map[6, 1] = new Node(E_nodeType.wall);

        map[0, 2] = new Node(E_nodeType.wall);
        map[1, 2] = new Node(E_nodeType.ghost);
        map[2, 2] = new Node(E_nodeType.walk);
        map[3, 2] = new Node(E_nodeType.box, 1, E_boxType.normal);
        map[4, 2] = new Node(E_nodeType.walk);
        map[5, 2] = new Node(E_nodeType.target, 1);
        map[6, 2] = new Node(E_nodeType.wall);

        map[0, 3] = new Node(E_nodeType.wall);
        map[1, 3] = new Node(E_nodeType.walk);
        map[2, 3] = new Node(E_nodeType.walk);
        map[3, 3] = new Node(E_nodeType.walk);
        map[4, 3] = new Node(E_nodeType.walk);
        map[5, 3] = new Node(E_nodeType.walk);
        map[6, 3] = new Node(E_nodeType.wall);

        map[0, 4] = new Node(E_nodeType.wall);
        map[1, 4] = new Node(E_nodeType.wall);
        map[2, 4] = new Node(E_nodeType.wall);
        map[3, 4] = new Node(E_nodeType.wall);
        map[4, 4] = new Node(E_nodeType.wall);
        map[5, 4] = new Node(E_nodeType.wall);
        map[6, 4] = new Node(E_nodeType.wall);

        levels.Add(map);

        // 第2关
        map = new Node[9, 4];
        map[0, 0] = new Node(E_nodeType.wall);
        map[1, 0] = new Node(E_nodeType.wall);
        map[2, 0] = new Node(E_nodeType.wall);
        map[3, 0] = new Node(E_nodeType.wall);
        map[4, 0] = new Node(E_nodeType.wall);
        map[5, 0] = new Node(E_nodeType.wall);
        map[6, 0] = new Node(E_nodeType.wall);
        map[7, 0] = new Node(E_nodeType.wall);
        map[8, 0] = new Node(E_nodeType.wall);

        map[0, 1] = new Node(E_nodeType.wall);
        map[1, 1] = new Node(E_nodeType.ghost);
        map[2, 1] = new Node(E_nodeType.box, 1, E_boxType.normal);
        map[3, 1] = new Node(E_nodeType.box, 3, E_boxType.normal);
        map[4, 1] = new Node(E_nodeType.box, 2, E_boxType.normal);
        map[5, 1] = new Node(E_nodeType.target, 1);
        map[6, 1] = new Node(E_nodeType.target, 2);
        map[7, 1] = new Node(E_nodeType.target, 3);
        map[8, 1] = new Node(E_nodeType.wall);

        map[0, 2] = new Node(E_nodeType.wall);
        map[1, 2] = new Node(E_nodeType.wall);
        map[2, 2] = new Node(E_nodeType.wall);
        map[3, 2] = new Node(E_nodeType.wall);
        map[4, 2] = new Node(E_nodeType.walk);
        map[5, 2] = new Node(E_nodeType.wall);
        map[6, 2] = new Node(E_nodeType.wall);
        map[7, 2] = new Node(E_nodeType.wall);
        map[8, 2] = new Node(E_nodeType.wall);

        map[0, 3] = new Node(E_nodeType.wall);
        map[1, 3] = new Node(E_nodeType.wall);
        map[2, 3] = new Node(E_nodeType.wall);
        map[3, 3] = new Node(E_nodeType.wall);
        map[4, 3] = new Node(E_nodeType.wall);
        map[5, 3] = new Node(E_nodeType.wall);
        map[6, 3] = new Node(E_nodeType.wall);
        map[7, 3] = new Node(E_nodeType.wall);
        map[8, 3] = new Node(E_nodeType.wall);

        levels.Add(map);

        // 第3关
        map = new Node[9, 4];
        map[0, 0] = new Node(E_nodeType.wall);
        map[1, 0] = new Node(E_nodeType.wall);
        map[2, 0] = new Node(E_nodeType.wall);
        map[3, 0] = new Node(E_nodeType.wall);
        map[4, 0] = new Node(E_nodeType.wall);
        map[5, 0] = new Node(E_nodeType.wall);
        map[6, 0] = new Node(E_nodeType.wall);
        map[7, 0] = new Node(E_nodeType.wall);
        map[8, 0] = new Node(E_nodeType.wall);

        map[0, 1] = new Node(E_nodeType.wall);
        map[1, 1] = new Node(E_nodeType.box, 1, E_boxType.normal);
        map[2, 1] = new Node(E_nodeType.box, 2, E_boxType.normal);
        map[3, 1] = new Node(E_nodeType.wall);
        map[4, 1] = new Node(E_nodeType.walk);
        map[5, 1] = new Node(E_nodeType.target, 3);
        map[6, 1] = new Node(E_nodeType.target, 2);
        map[7, 1] = new Node(E_nodeType.target, 1);
        map[8, 1] = new Node(E_nodeType.wall);

        map[0, 2] = new Node(E_nodeType.wall);
        map[1, 2] = new Node(E_nodeType.ghost);
        map[2, 2] = new Node(E_nodeType.box, 3, E_boxType.normal);
        map[3, 2] = new Node(E_nodeType.walk);
        map[4, 2] = new Node(E_nodeType.walk);
        map[5, 2] = new Node(E_nodeType.wall);
        map[6, 2] = new Node(E_nodeType.wall);
        map[7, 2] = new Node(E_nodeType.wall);
        map[8, 2] = new Node(E_nodeType.wall);

        map[0, 3] = new Node(E_nodeType.wall);
        map[1, 3] = new Node(E_nodeType.wall);
        map[2, 3] = new Node(E_nodeType.wall);
        map[3, 3] = new Node(E_nodeType.wall);
        map[4, 3] = new Node(E_nodeType.wall);
        map[5, 3] = new Node(E_nodeType.wall);
        map[6, 3] = new Node(E_nodeType.wall);
        map[7, 3] = new Node(E_nodeType.wall);
        map[8, 3] = new Node(E_nodeType.wall);

        levels.Add(map);

        // 第4关
        map = new Node[7, 6];
        map[0, 0] = new Node(E_nodeType.wall);
        map[1, 0] = new Node(E_nodeType.wall);
        map[2, 0] = new Node(E_nodeType.wall);
        map[3, 0] = new Node(E_nodeType.wall);
        map[4, 0] = new Node(E_nodeType.wall);
        map[5, 0] = new Node(E_nodeType.wall);
        map[6, 0] = new Node(E_nodeType.wall);

        map[0, 1] = new Node(E_nodeType.wall);
        map[1, 1] = new Node(E_nodeType.ghost);
        map[2, 1] = new Node(E_nodeType.wall);
        map[3, 1] = new Node(E_nodeType.walk);
        map[4, 1] = new Node(E_nodeType.walk);
        map[5, 1] = new Node(E_nodeType.target, 2);
        map[6, 1] = new Node(E_nodeType.wall);

        map[0, 2] = new Node(E_nodeType.wall);
        map[1, 2] = new Node(E_nodeType.walk);
        map[2, 2] = new Node(E_nodeType.walk);
        map[3, 2] = new Node(E_nodeType.box, 2, E_boxType.refuse);
        map[4, 2] = new Node(E_nodeType.wall);
        map[5, 2] = new Node(E_nodeType.target, 1);
        map[6, 2] = new Node(E_nodeType.wall);

        map[0, 3] = new Node(E_nodeType.wall);
        map[1, 3] = new Node(E_nodeType.wall);
        map[2, 3] = new Node(E_nodeType.walk);
        map[3, 3] = new Node(E_nodeType.walk);
        map[4, 3] = new Node(E_nodeType.box, 1, E_boxType.normal);
        map[5, 3] = new Node(E_nodeType.walk);
        map[6, 3] = new Node(E_nodeType.wall);

        map[0, 4] = new Node(E_nodeType.wall);
        map[1, 4] = new Node(E_nodeType.wall);
        map[2, 4] = new Node(E_nodeType.wall);
        map[3, 4] = new Node(E_nodeType.wall);
        map[4, 4] = new Node(E_nodeType.walk);
        map[5, 4] = new Node(E_nodeType.walk);
        map[6, 4] = new Node(E_nodeType.wall);

        map[0, 5] = new Node(E_nodeType.wall);
        map[1, 5] = new Node(E_nodeType.wall);
        map[2, 5] = new Node(E_nodeType.wall);
        map[3, 5] = new Node(E_nodeType.wall);
        map[4, 5] = new Node(E_nodeType.wall);
        map[5, 5] = new Node(E_nodeType.wall);
        map[6, 5] = new Node(E_nodeType.wall);

        levels.Add(map);

        // 第5关
        map = new Node[6, 5];
        map[0, 0] = new Node(E_nodeType.wall);
        map[1, 0] = new Node(E_nodeType.wall);
        map[2, 0] = new Node(E_nodeType.wall);
        map[3, 0] = new Node(E_nodeType.wall);
        map[4, 0] = new Node(E_nodeType.wall);
        map[5, 0] = new Node(E_nodeType.wall);

        map[0, 1] = new Node(E_nodeType.wall);
        map[1, 1] = new Node(E_nodeType.box, 2, E_boxType.normal);
        map[2, 1] = new Node(E_nodeType.box, 1, E_boxType.refuse);
        map[3, 1] = new Node(E_nodeType.walk);
        map[4, 1] = new Node(E_nodeType.target, 1);
        map[5, 1] = new Node(E_nodeType.wall);

        map[0, 2] = new Node(E_nodeType.wall);
        map[1, 2] = new Node(E_nodeType.wall);
        map[2, 2] = new Node(E_nodeType.wall);
        map[3, 2] = new Node(E_nodeType.walk);
        map[4, 2] = new Node(E_nodeType.walk);
        map[5, 2] = new Node(E_nodeType.wall);

        map[0, 3] = new Node(E_nodeType.wall);
        map[1, 3] = new Node(E_nodeType.wall);
        map[2, 3] = new Node(E_nodeType.wall);
        map[3, 3] = new Node(E_nodeType.target, 2);
        map[4, 3] = new Node(E_nodeType.ghost);
        map[5, 3] = new Node(E_nodeType.wall);

        map[0, 4] = new Node(E_nodeType.wall);
        map[1, 4] = new Node(E_nodeType.wall);
        map[2, 4] = new Node(E_nodeType.wall);
        map[3, 4] = new Node(E_nodeType.wall);
        map[4, 4] = new Node(E_nodeType.wall);
        map[5, 4] = new Node(E_nodeType.wall);

        levels.Add(map);

        // 第6关
        map = new Node[7, 6];
        map[0, 0] = new Node(E_nodeType.wall);
        map[1, 0] = new Node(E_nodeType.wall);
        map[2, 0] = new Node(E_nodeType.wall);
        map[3, 0] = new Node(E_nodeType.wall);
        map[4, 0] = new Node(E_nodeType.wall);
        map[5, 0] = new Node(E_nodeType.wall);
        map[6, 0] = new Node(E_nodeType.wall);

        map[0, 1] = new Node(E_nodeType.wall);
        map[1, 1] = new Node(E_nodeType.walk);
        map[2, 1] = new Node(E_nodeType.walk);
        map[3, 1] = new Node(E_nodeType.ghost);
        map[4, 1] = new Node(E_nodeType.walk);
        map[5, 1] = new Node(E_nodeType.walk);
        map[6, 1] = new Node(E_nodeType.wall);

        map[0, 2] = new Node(E_nodeType.wall);
        map[1, 2] = new Node(E_nodeType.walk);
        map[2, 2] = new Node(E_nodeType.box, 1, E_boxType.normal);
        map[3, 2] = new Node(E_nodeType.box, 3, E_boxType.normal);
        map[4, 2] = new Node(E_nodeType.box, 2, E_boxType.normal);
        map[5, 2] = new Node(E_nodeType.walk);
        map[6, 2] = new Node(E_nodeType.wall);

        map[0, 3] = new Node(E_nodeType.wall);
        map[1, 3] = new Node(E_nodeType.wall);
        map[2, 3] = new Node(E_nodeType.wall);
        map[3, 3] = new Node(E_nodeType.walk);
        map[4, 3] = new Node(E_nodeType.wall);
        map[5, 3] = new Node(E_nodeType.wall);
        map[6, 3] = new Node(E_nodeType.wall);

        map[0, 4] = new Node(E_nodeType.wall);
        map[1, 4] = new Node(E_nodeType.target, 3);
        map[2, 4] = new Node(E_nodeType.walk);
        map[3, 4] = new Node(E_nodeType.target, 2);
        map[4, 4] = new Node(E_nodeType.walk);
        map[5, 4] = new Node(E_nodeType.target, 1);
        map[6, 4] = new Node(E_nodeType.wall);

        map[0, 5] = new Node(E_nodeType.wall);
        map[1, 5] = new Node(E_nodeType.wall);
        map[2, 5] = new Node(E_nodeType.wall);
        map[3, 5] = new Node(E_nodeType.wall);
        map[4, 5] = new Node(E_nodeType.wall);
        map[5, 5] = new Node(E_nodeType.wall);
        map[6, 5] = new Node(E_nodeType.wall);

        levels.Add(map);

        // 第7关
        map = new Node[7, 6];
        map[0, 0] = new Node(E_nodeType.wall);
        map[1, 0] = new Node(E_nodeType.wall);
        map[2, 0] = new Node(E_nodeType.wall);
        map[3, 0] = new Node(E_nodeType.wall);
        map[4, 0] = new Node(E_nodeType.wall);
        map[5, 0] = new Node(E_nodeType.wall);
        map[6, 0] = new Node(E_nodeType.wall);

        map[0, 1] = new Node(E_nodeType.wall);
        map[1, 1] = new Node(E_nodeType.walk);
        map[2, 1] = new Node(E_nodeType.walk);
        map[3, 1] = new Node(E_nodeType.ghost);
        map[4, 1] = new Node(E_nodeType.walk);
        map[5, 1] = new Node(E_nodeType.walk);
        map[6, 1] = new Node(E_nodeType.wall);

        map[0, 2] = new Node(E_nodeType.wall);
        map[1, 2] = new Node(E_nodeType.walk);
        map[2, 2] = new Node(E_nodeType.box, 1, E_boxType.refuse);
        map[3, 2] = new Node(E_nodeType.box, 3, E_boxType.normal);
        map[4, 2] = new Node(E_nodeType.box, 2, E_boxType.normal);
        map[5, 2] = new Node(E_nodeType.walk);
        map[6, 2] = new Node(E_nodeType.wall);

        map[0, 3] = new Node(E_nodeType.wall);
        map[1, 3] = new Node(E_nodeType.wall);
        map[2, 3] = new Node(E_nodeType.wall);
        map[3, 3] = new Node(E_nodeType.walk);
        map[4, 3] = new Node(E_nodeType.wall);
        map[5, 3] = new Node(E_nodeType.wall);
        map[6, 3] = new Node(E_nodeType.wall);

        map[0, 4] = new Node(E_nodeType.wall);
        map[1, 4] = new Node(E_nodeType.target, 3);
        map[2, 4] = new Node(E_nodeType.walk);
        map[3, 4] = new Node(E_nodeType.target, 2);
        map[4, 4] = new Node(E_nodeType.walk);
        map[5, 4] = new Node(E_nodeType.target, 1);
        map[6, 4] = new Node(E_nodeType.wall);

        map[0, 5] = new Node(E_nodeType.wall);
        map[1, 5] = new Node(E_nodeType.wall);
        map[2, 5] = new Node(E_nodeType.wall);
        map[3, 5] = new Node(E_nodeType.wall);
        map[4, 5] = new Node(E_nodeType.wall);
        map[5, 5] = new Node(E_nodeType.wall);
        map[6, 5] = new Node(E_nodeType.wall);

        levels.Add(map);

        // 第8关
        map = new Node[7, 5];
        map[0, 0] = new Node(E_nodeType.wall);
        map[1, 0] = new Node(E_nodeType.wall);
        map[2, 0] = new Node(E_nodeType.wall);
        map[3, 0] = new Node(E_nodeType.wall);
        map[4, 0] = new Node(E_nodeType.wall);
        map[5, 0] = new Node(E_nodeType.wall);
        map[6, 0] = new Node(E_nodeType.wall);

        map[0, 1] = new Node(E_nodeType.wall);
        map[1, 1] = new Node(E_nodeType.walk);
        map[2, 1] = new Node(E_nodeType.walk);
        map[3, 1] = new Node(E_nodeType.walk);
        map[4, 1] = new Node(E_nodeType.walk);
        map[5, 1] = new Node(E_nodeType.walk);
        map[6, 1] = new Node(E_nodeType.wall);

        map[0, 2] = new Node(E_nodeType.wall);
        map[1, 2] = new Node(E_nodeType.ghost);
        map[2, 2] = new Node(E_nodeType.walk);
        map[3, 2] = new Node(E_nodeType.box, 1, E_boxType.refuse);
        map[4, 2] = new Node(E_nodeType.walk);
        map[5, 2] = new Node(E_nodeType.target, 2);
        map[6, 2] = new Node(E_nodeType.wall);

        map[0, 3] = new Node(E_nodeType.wall);
        map[1, 3] = new Node(E_nodeType.walk);
        map[2, 3] = new Node(E_nodeType.walk);
        map[3, 3] = new Node(E_nodeType.box, 2, E_boxType.curse);
        map[4, 3] = new Node(E_nodeType.walk);
        map[5, 3] = new Node(E_nodeType.target, 1);
        map[6, 3] = new Node(E_nodeType.wall);

        map[0, 4] = new Node(E_nodeType.wall);
        map[1, 4] = new Node(E_nodeType.wall);
        map[2, 4] = new Node(E_nodeType.wall);
        map[3, 4] = new Node(E_nodeType.wall);
        map[4, 4] = new Node(E_nodeType.wall);
        map[5, 4] = new Node(E_nodeType.wall);
        map[6, 4] = new Node(E_nodeType.wall);

        levels.Add(map);

        // 第9关
        map = new Node[6, 7];
        map[0, 0] = new Node(E_nodeType.wall);
        map[1, 0] = new Node(E_nodeType.wall);
        map[2, 0] = new Node(E_nodeType.wall);
        map[3, 0] = new Node(E_nodeType.wall);
        map[4, 0] = new Node(E_nodeType.wall);
        map[5, 0] = new Node(E_nodeType.wall);

        map[0, 1] = new Node(E_nodeType.wall);
        map[1, 1] = new Node(E_nodeType.wall);
        map[2, 1] = new Node(E_nodeType.walk);
        map[3, 1] = new Node(E_nodeType.target, 3);
        map[4, 1] = new Node(E_nodeType.walk);
        map[5, 1] = new Node(E_nodeType.wall);

        map[0, 2] = new Node(E_nodeType.wall);
        map[1, 2] = new Node(E_nodeType.wall);
        map[2, 2] = new Node(E_nodeType.box, 3, E_boxType.refuse);
        map[3, 2] = new Node(E_nodeType.target, 1);
        map[4, 2] = new Node(E_nodeType.target, 2);
        map[5, 2] = new Node(E_nodeType.wall);

        map[0, 3] = new Node(E_nodeType.wall);
        map[1, 3] = new Node(E_nodeType.wall);
        map[2, 3] = new Node(E_nodeType.walk);
        map[3, 3] = new Node(E_nodeType.walk);
        map[4, 3] = new Node(E_nodeType.wall);
        map[5, 3] = new Node(E_nodeType.wall);

        map[0, 4] = new Node(E_nodeType.wall);
        map[1, 4] = new Node(E_nodeType.walk);
        map[2, 4] = new Node(E_nodeType.box, 2, E_boxType.curse);
        map[3, 4] = new Node(E_nodeType.walk);
        map[4, 4] = new Node(E_nodeType.wall);
        map[5, 4] = new Node(E_nodeType.wall);

        map[0, 5] = new Node(E_nodeType.wall);
        map[1, 5] = new Node(E_nodeType.ghost);
        map[2, 5] = new Node(E_nodeType.box, 1, E_boxType.normal);
        map[3, 5] = new Node(E_nodeType.walk);
        map[4, 5] = new Node(E_nodeType.wall);
        map[5, 5] = new Node(E_nodeType.wall);

        map[0, 6] = new Node(E_nodeType.wall);
        map[1, 6] = new Node(E_nodeType.wall);
        map[2, 6] = new Node(E_nodeType.wall);
        map[3, 6] = new Node(E_nodeType.wall);
        map[4, 6] = new Node(E_nodeType.wall);
        map[5, 6] = new Node(E_nodeType.wall);

        levels.Add(map);

        // 第10关
        map = new Node[9, 4];
        map[0, 0] = new Node(E_nodeType.wall);
        map[1, 0] = new Node(E_nodeType.wall);
        map[2, 0] = new Node(E_nodeType.wall);
        map[3, 0] = new Node(E_nodeType.wall);
        map[4, 0] = new Node(E_nodeType.wall);
        map[5, 0] = new Node(E_nodeType.wall);
        map[6, 0] = new Node(E_nodeType.wall);
        map[7, 0] = new Node(E_nodeType.wall);
        map[8, 0] = new Node(E_nodeType.wall);

        map[0, 1] = new Node(E_nodeType.wall);
        map[1, 1] = new Node(E_nodeType.ghost);
        map[2, 1] = new Node(E_nodeType.box, 2, E_boxType.normal);
        map[3, 1] = new Node(E_nodeType.box, 3, E_boxType.refuse);
        map[4, 1] = new Node(E_nodeType.box, 1, E_boxType.normal);
        map[5, 1] = new Node(E_nodeType.target, 2);
        map[6, 1] = new Node(E_nodeType.target, 1);
        map[7, 1] = new Node(E_nodeType.target, 3);
        map[8, 1] = new Node(E_nodeType.wall);

        map[0, 2] = new Node(E_nodeType.wall);
        map[1, 2] = new Node(E_nodeType.wall);
        map[2, 2] = new Node(E_nodeType.wall);
        map[3, 2] = new Node(E_nodeType.wall);
        map[4, 2] = new Node(E_nodeType.walk);
        map[5, 2] = new Node(E_nodeType.wall);
        map[6, 2] = new Node(E_nodeType.wall);
        map[7, 2] = new Node(E_nodeType.wall);
        map[8, 2] = new Node(E_nodeType.wall);

        map[0, 3] = new Node(E_nodeType.wall);
        map[1, 3] = new Node(E_nodeType.wall);
        map[2, 3] = new Node(E_nodeType.wall);
        map[3, 3] = new Node(E_nodeType.wall);
        map[4, 3] = new Node(E_nodeType.wall);
        map[5, 3] = new Node(E_nodeType.wall);
        map[6, 3] = new Node(E_nodeType.wall);
        map[7, 3] = new Node(E_nodeType.wall);
        map[8, 3] = new Node(E_nodeType.wall);

        levels.Add(map);

        // 第11关
        map = new Node[9, 7];
        map[0, 0] = new Node(E_nodeType.wall);
        map[1, 0] = new Node(E_nodeType.wall);
        map[2, 0] = new Node(E_nodeType.wall);
        map[3, 0] = new Node(E_nodeType.wall);
        map[4, 0] = new Node(E_nodeType.wall);
        map[5, 0] = new Node(E_nodeType.wall);
        map[6, 0] = new Node(E_nodeType.wall);
        map[7, 0] = new Node(E_nodeType.wall);
        map[8, 0] = new Node(E_nodeType.wall);

        map[0, 1] = new Node(E_nodeType.wall);
        map[1, 1] = new Node(E_nodeType.walk);
        map[2, 1] = new Node(E_nodeType.walk);
        map[3, 1] = new Node(E_nodeType.ghost);
        map[4, 1] = new Node(E_nodeType.walk);
        map[5, 1] = new Node(E_nodeType.box, 1, E_boxType.normal);
        map[6, 1] = new Node(E_nodeType.walk);
        map[7, 1] = new Node(E_nodeType.walk);
        map[8, 1] = new Node(E_nodeType.wall);

        map[0, 2] = new Node(E_nodeType.wall);
        map[1, 2] = new Node(E_nodeType.walk);
        map[2, 2] = new Node(E_nodeType.wall);
        map[3, 2] = new Node(E_nodeType.wall);
        map[4, 2] = new Node(E_nodeType.wall);
        map[5, 2] = new Node(E_nodeType.walk);
        map[6, 2] = new Node(E_nodeType.wall);
        map[7, 2] = new Node(E_nodeType.wall);
        map[8, 2] = new Node(E_nodeType.wall);

        map[0, 3] = new Node(E_nodeType.wall);
        map[1, 3] = new Node(E_nodeType.walk);
        map[2, 3] = new Node(E_nodeType.walk);
        map[3, 3] = new Node(E_nodeType.box, 3, E_boxType.refuse);
        map[4, 3] = new Node(E_nodeType.walk);
        map[5, 3] = new Node(E_nodeType.box, 2, E_boxType.curse);
        map[6, 3] = new Node(E_nodeType.walk);
        map[7, 3] = new Node(E_nodeType.target, 1);
        map[8, 3] = new Node(E_nodeType.wall);

        map[0, 4] = new Node(E_nodeType.wall);
        map[1, 4] = new Node(E_nodeType.wall);
        map[2, 4] = new Node(E_nodeType.wall);
        map[3, 4] = new Node(E_nodeType.walk);
        map[4, 4] = new Node(E_nodeType.wall);
        map[5, 4] = new Node(E_nodeType.walk);
        map[6, 4] = new Node(E_nodeType.wall);
        map[7, 4] = new Node(E_nodeType.wall);
        map[8, 4] = new Node(E_nodeType.wall);

        map[0, 5] = new Node(E_nodeType.wall);
        map[1, 5] = new Node(E_nodeType.target, 3);
        map[2, 5] = new Node(E_nodeType.walk);
        map[3, 5] = new Node(E_nodeType.walk);
        map[4, 5] = new Node(E_nodeType.walk);
        map[5, 5] = new Node(E_nodeType.walk);
        map[6, 5] = new Node(E_nodeType.walk);
        map[7, 5] = new Node(E_nodeType.target, 2);
        map[8, 5] = new Node(E_nodeType.wall);

        map[0, 6] = new Node(E_nodeType.wall);
        map[1, 6] = new Node(E_nodeType.wall);
        map[2, 6] = new Node(E_nodeType.wall);
        map[3, 6] = new Node(E_nodeType.wall);
        map[4, 6] = new Node(E_nodeType.wall);
        map[5, 6] = new Node(E_nodeType.wall);
        map[6, 6] = new Node(E_nodeType.wall);
        map[7, 6] = new Node(E_nodeType.wall);
        map[8, 6] = new Node(E_nodeType.wall);

        levels.Add(map);

        // 第12关
        map = new Node[7, 6];
        map[0, 0] = new Node(E_nodeType.wall);
        map[1, 0] = new Node(E_nodeType.wall);
        map[2, 0] = new Node(E_nodeType.wall);
        map[3, 0] = new Node(E_nodeType.wall);
        map[4, 0] = new Node(E_nodeType.wall);
        map[5, 0] = new Node(E_nodeType.wall);
        map[6, 0] = new Node(E_nodeType.wall);

        map[0, 1] = new Node(E_nodeType.wall);
        map[1, 1] = new Node(E_nodeType.spike);
        map[2, 1] = new Node(E_nodeType.box, -1, E_boxType.normal);
        map[3, 1] = new Node(E_nodeType.target, 1);
        map[4, 1] = new Node(E_nodeType.walk);
        map[5, 1] = new Node(E_nodeType.wall);
        map[6, 1] = new Node(E_nodeType.wall);

        map[0, 2] = new Node(E_nodeType.wall);
        map[1, 2] = new Node(E_nodeType.wall);
        map[2, 2] = new Node(E_nodeType.wall);
        map[3, 2] = new Node(E_nodeType.walk);
        map[4, 2] = new Node(E_nodeType.walk);
        map[5, 2] = new Node(E_nodeType.walk);
        map[6, 2] = new Node(E_nodeType.wall);

        map[0, 3] = new Node(E_nodeType.wall);
        map[1, 3] = new Node(E_nodeType.ghost);
        map[2, 3] = new Node(E_nodeType.walk);
        map[3, 3] = new Node(E_nodeType.walk);
        map[4, 3] = new Node(E_nodeType.box, 1, E_boxType.refuse);
        map[5, 3] = new Node(E_nodeType.wall);
        map[6, 3] = new Node(E_nodeType.wall);

        map[0, 4] = new Node(E_nodeType.wall);
        map[1, 4] = new Node(E_nodeType.wall);
        map[2, 4] = new Node(E_nodeType.wall);
        map[3, 4] = new Node(E_nodeType.wall);
        map[4, 4] = new Node(E_nodeType.box, -1, E_boxType.curse);
        map[5, 4] = new Node(E_nodeType.wall);
        map[6, 4] = new Node(E_nodeType.wall);

        map[0, 5] = new Node(E_nodeType.wall);
        map[1, 5] = new Node(E_nodeType.wall);
        map[2, 5] = new Node(E_nodeType.wall);
        map[3, 5] = new Node(E_nodeType.wall);
        map[4, 5] = new Node(E_nodeType.wall);
        map[5, 5] = new Node(E_nodeType.wall);
        map[6, 5] = new Node(E_nodeType.wall);

        levels.Add(map);

        // 第13关
        map = new Node[9, 7];
        map[0, 0] = new Node(E_nodeType.wall);
        map[1, 0] = new Node(E_nodeType.wall);
        map[2, 0] = new Node(E_nodeType.wall);
        map[3, 0] = new Node(E_nodeType.wall);
        map[4, 0] = new Node(E_nodeType.wall);
        map[5, 0] = new Node(E_nodeType.wall);
        map[6, 0] = new Node(E_nodeType.wall);
        map[7, 0] = new Node(E_nodeType.wall);
        map[8, 0] = new Node(E_nodeType.wall);

        map[0, 1] = new Node(E_nodeType.wall);
        map[1, 1] = new Node(E_nodeType.walk);
        map[2, 1] = new Node(E_nodeType.walk);
        map[3, 1] = new Node(E_nodeType.ghost);
        map[4, 1] = new Node(E_nodeType.walk);
        map[5, 1] = new Node(E_nodeType.box, 1, E_boxType.normal);
        map[6, 1] = new Node(E_nodeType.spike);
        map[7, 1] = new Node(E_nodeType.wall);
        map[8, 1] = new Node(E_nodeType.wall);

        map[0, 2] = new Node(E_nodeType.wall);
        map[1, 2] = new Node(E_nodeType.walk);
        map[2, 2] = new Node(E_nodeType.wall);
        map[3, 2] = new Node(E_nodeType.wall);
        map[4, 2] = new Node(E_nodeType.wall);
        map[5, 2] = new Node(E_nodeType.walk);
        map[6, 2] = new Node(E_nodeType.wall);
        map[7, 2] = new Node(E_nodeType.wall);
        map[8, 2] = new Node(E_nodeType.wall);

        map[0, 3] = new Node(E_nodeType.wall);
        map[1, 3] = new Node(E_nodeType.walk);
        map[2, 3] = new Node(E_nodeType.walk);
        map[3, 3] = new Node(E_nodeType.box, -1, E_boxType.curse);
        map[4, 3] = new Node(E_nodeType.walk);
        map[5, 3] = new Node(E_nodeType.box, 2, E_boxType.refuse);
        map[6, 3] = new Node(E_nodeType.walk);
        map[7, 3] = new Node(E_nodeType.target, 1);
        map[8, 3] = new Node(E_nodeType.wall);

        map[0, 4] = new Node(E_nodeType.wall);
        map[1, 4] = new Node(E_nodeType.wall);
        map[2, 4] = new Node(E_nodeType.wall);
        map[3, 4] = new Node(E_nodeType.walk);
        map[4, 4] = new Node(E_nodeType.wall);
        map[5, 4] = new Node(E_nodeType.walk);
        map[6, 4] = new Node(E_nodeType.wall);
        map[7, 4] = new Node(E_nodeType.wall);
        map[8, 4] = new Node(E_nodeType.wall);

        map[0, 5] = new Node(E_nodeType.wall);
        map[1, 5] = new Node(E_nodeType.target, 2);
        map[2, 5] = new Node(E_nodeType.walk);
        map[3, 5] = new Node(E_nodeType.walk);
        map[4, 5] = new Node(E_nodeType.walk);
        map[5, 5] = new Node(E_nodeType.walk);
        map[6, 5] = new Node(E_nodeType.walk);
        map[7, 5] = new Node(E_nodeType.wall);
        map[8, 5] = new Node(E_nodeType.wall);

        map[0, 6] = new Node(E_nodeType.wall);
        map[1, 6] = new Node(E_nodeType.wall);
        map[2, 6] = new Node(E_nodeType.wall);
        map[3, 6] = new Node(E_nodeType.wall);
        map[4, 6] = new Node(E_nodeType.wall);
        map[5, 6] = new Node(E_nodeType.wall);
        map[6, 6] = new Node(E_nodeType.wall);
        map[7, 6] = new Node(E_nodeType.wall);
        map[8, 6] = new Node(E_nodeType.wall);

        levels.Add(map);

        // 第14关
        map = new Node[8, 8];
        map[0, 0] = new Node(E_nodeType.wall);
        map[1, 0] = new Node(E_nodeType.wall);
        map[2, 0] = new Node(E_nodeType.wall);
        map[3, 0] = new Node(E_nodeType.wall);
        map[4, 0] = new Node(E_nodeType.wall);
        map[5, 0] = new Node(E_nodeType.wall);
        map[6, 0] = new Node(E_nodeType.wall);
        map[7, 0] = new Node(E_nodeType.wall);

        map[0, 1] = new Node(E_nodeType.wall);
        map[1, 1] = new Node(E_nodeType.wall);
        map[2, 1] = new Node(E_nodeType.wall);
        map[3, 1] = new Node(E_nodeType.target, 1);
        map[4, 1] = new Node(E_nodeType.wall);
        map[5, 1] = new Node(E_nodeType.wall);
        map[6, 1] = new Node(E_nodeType.wall);
        map[7, 1] = new Node(E_nodeType.wall);

        map[0, 2] = new Node(E_nodeType.wall);
        map[1, 2] = new Node(E_nodeType.wall);
        map[2, 2] = new Node(E_nodeType.wall);
        map[3, 2] = new Node(E_nodeType.walk);
        map[4, 2] = new Node(E_nodeType.wall);
        map[5, 2] = new Node(E_nodeType.wall);
        map[6, 2] = new Node(E_nodeType.wall);
        map[7, 2] = new Node(E_nodeType.wall);

        map[0, 3] = new Node(E_nodeType.wall);
        map[1, 3] = new Node(E_nodeType.wall);
        map[2, 3] = new Node(E_nodeType.wall);
        map[3, 3] = new Node(E_nodeType.box, 3, E_boxType.normal);
        map[4, 3] = new Node(E_nodeType.box, 2, E_boxType.refuse);
        map[5, 3] = new Node(E_nodeType.walk);
        map[6, 3] = new Node(E_nodeType.target, 2);
        map[7, 3] = new Node(E_nodeType.wall);

        map[0, 4] = new Node(E_nodeType.wall);
        map[1, 4] = new Node(E_nodeType.target, 3);
        map[2, 4] = new Node(E_nodeType.walk);
        map[3, 4] = new Node(E_nodeType.box, -1, E_boxType.curse);
        map[4, 4] = new Node(E_nodeType.box, 1, E_boxType.normal);
        map[5, 4] = new Node(E_nodeType.wall);
        map[6, 4] = new Node(E_nodeType.wall);
        map[7, 4] = new Node(E_nodeType.wall);

        map[0, 5] = new Node(E_nodeType.wall);
        map[1, 5] = new Node(E_nodeType.wall);
        map[2, 5] = new Node(E_nodeType.wall);
        map[3, 5] = new Node(E_nodeType.wall);
        map[4, 5] = new Node(E_nodeType.ghost);
        map[5, 5] = new Node(E_nodeType.wall);
        map[6, 5] = new Node(E_nodeType.wall);
        map[7, 5] = new Node(E_nodeType.wall);

        map[0, 6] = new Node(E_nodeType.wall);
        map[1, 6] = new Node(E_nodeType.wall);
        map[2, 6] = new Node(E_nodeType.wall);
        map[3, 6] = new Node(E_nodeType.wall);
        map[4, 6] = new Node(E_nodeType.spike);
        map[5, 6] = new Node(E_nodeType.wall);
        map[6, 6] = new Node(E_nodeType.wall);
        map[7, 6] = new Node(E_nodeType.wall);

        map[0, 7] = new Node(E_nodeType.wall);
        map[1, 7] = new Node(E_nodeType.wall);
        map[2, 7] = new Node(E_nodeType.wall);
        map[3, 7] = new Node(E_nodeType.wall);
        map[4, 7] = new Node(E_nodeType.wall);
        map[5, 7] = new Node(E_nodeType.wall);
        map[6, 7] = new Node(E_nodeType.wall);
        map[7, 7] = new Node(E_nodeType.wall);

        levels.Add(map);

        // 第15关
        map = new Node[7, 6];
        map[0, 0] = new Node(E_nodeType.wall);
        map[1, 0] = new Node(E_nodeType.wall);
        map[2, 0] = new Node(E_nodeType.wall);
        map[3, 0] = new Node(E_nodeType.wall);
        map[4, 0] = new Node(E_nodeType.wall);
        map[5, 0] = new Node(E_nodeType.wall);
        map[6, 0] = new Node(E_nodeType.wall);

        map[0, 1] = new Node(E_nodeType.wall);
        map[1, 1] = new Node(E_nodeType.box, -1, E_boxType.refuse);
        map[2, 1] = new Node(E_nodeType.box, -1, E_boxType.refuse);
        map[3, 1] = new Node(E_nodeType.spike);
        map[4, 1] = new Node(E_nodeType.box, -1, E_boxType.refuse);
        map[5, 1] = new Node(E_nodeType.box, -1, E_boxType.refuse);
        map[6, 1] = new Node(E_nodeType.wall);

        map[0, 2] = new Node(E_nodeType.wall);
        map[1, 2] = new Node(E_nodeType.box, -1, E_boxType.normal);
        map[2, 2] = new Node(E_nodeType.ghost);
        map[3, 2] = new Node(E_nodeType.box, -1, E_boxType.normal);
        map[4, 2] = new Node(E_nodeType.box, -1, E_boxType.refuse);
        map[5, 2] = new Node(E_nodeType.box, -1, E_boxType.refuse);
        map[6, 2] = new Node(E_nodeType.wall);

        map[0, 3] = new Node(E_nodeType.wall);
        map[1, 3] = new Node(E_nodeType.box, -1, E_boxType.refuse);
        map[2, 3] = new Node(E_nodeType.box, -1, E_boxType.refuse);
        map[3, 3] = new Node(E_nodeType.box, -1, E_boxType.refuse);
        map[4, 3] = new Node(E_nodeType.box, -1, E_boxType.curse);
        map[5, 3] = new Node(E_nodeType.box, -1, E_boxType.refuse);
        map[6, 3] = new Node(E_nodeType.wall);

        map[0, 4] = new Node(E_nodeType.wall);
        map[1, 4] = new Node(E_nodeType.spike);
        map[2, 4] = new Node(E_nodeType.target, 1);
        map[3, 4] = new Node(E_nodeType.walk);
        map[4, 4] = new Node(E_nodeType.box, -1, E_boxType.refuse);
        map[5, 4] = new Node(E_nodeType.box, 1, E_boxType.normal);
        map[6, 4] = new Node(E_nodeType.wall);

        map[0, 5] = new Node(E_nodeType.wall);
        map[1, 5] = new Node(E_nodeType.wall);
        map[2, 5] = new Node(E_nodeType.wall);
        map[3, 5] = new Node(E_nodeType.wall);
        map[4, 5] = new Node(E_nodeType.wall);
        map[5, 5] = new Node(E_nodeType.wall);
        map[6, 5] = new Node(E_nodeType.wall);

        levels.Add(map);
    }

    // 初始化关卡 level从1开始
    public void InitLevel(int level)
    {
        MapMgr.Instance.Init(levels[level - 1]);
    }

}
