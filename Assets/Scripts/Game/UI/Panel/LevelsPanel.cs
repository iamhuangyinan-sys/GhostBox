using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsPanel : BasePanel
{
    public override void Hide()
    {

    }

    public override void Show()
    {

    }

    protected override void ClickButton(string btnName)
    {
        if (btnName.Contains("LevelBtn"))
        {
            // 跳转到对应关卡
            LevelMgr.Instance.InitLevel(int.Parse(btnName.Replace("LevelBtn", "")));
            UIMgr.Instance.HidePanel<LevelsPanel>();
            UIMgr.Instance.ShowPanel<TipPanel>();
            MusicMgr.Instance.PlayBGM("关卡音乐");

            MapMgr.Instance.isGaming = true;
        }
    }
}
