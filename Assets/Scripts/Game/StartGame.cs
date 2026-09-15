using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    void Start()
    {
        UIMgr.Instance.ShowPanel<BeginPanel>();
        MusicMgr.Instance.PlayBGM("主面板音乐");
    }
}
