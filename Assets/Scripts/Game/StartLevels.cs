using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevels : MonoBehaviour
{
    void Start()
    {
        UIMgr.Instance.ShowPanel<LevelsPanel>();
        MapMgr.Instance.isGaming = false;
    }
}
