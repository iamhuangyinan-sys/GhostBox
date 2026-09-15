using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanel : BasePanel
{
    public override void Hide()
    {
        MapMgr.Instance.isPause = false;
    }

    public override void Show()
    {
        MapMgr.Instance.isPause = true;
    }

    protected override void ClickButton(string btnName)
    {
        switch (btnName)
        {
            case "ConBtn":
                UIMgr.Instance.HidePanel<MenuPanel>();
                break;
            case "ReturnBtn":
                MapMgr.Instance.isGaming = false;
                SceneMgr.Instance.LoadScene("Levels");
                UIMgr.Instance.HidePanel<MenuPanel>();
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
