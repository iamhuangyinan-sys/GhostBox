using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BeginPanel : BasePanel
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
            case "StartBtn":
                UIMgr.Instance.HidePanel<BeginPanel>();
                SceneMgr.Instance.LoadScene("Levels");
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
