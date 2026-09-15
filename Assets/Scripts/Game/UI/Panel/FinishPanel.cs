using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPanel : BasePanel
{
    public override void Hide()
    {
        
    }

    public override void Show()
    {
        
    }

    protected override void ClickButton(string btnName)
    {
        switch (btnName)
        {
            case "ReturnBtn":
                MapMgr.Instance.isGaming = false;
                SceneMgr.Instance.LoadScene("Levels");
                UIMgr.Instance.HidePanel<FinishPanel>();
                UIMgr.Instance.HidePanel<TipPanel>();
                MusicMgr.Instance.PlayBGM("主面板音乐");
                break;
            case "ExitBtn":
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                Application.Quit();
                break;
        }
    }
}
