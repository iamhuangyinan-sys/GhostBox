using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音乐管理器
/// </summary>
public class MusicMgr : Singleton<MusicMgr>
{
    private MusicMgr() { }

    //背景音乐组件
    private AudioSource BGM = null;
    //背景音乐音量
    private float BGMvolume = 0.5f;

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="name">音乐名称</param>
    public void PlayBGM(string name)
    {
        if (BGM == null)
        {
            GameObject obj = new GameObject("BGM");
            GameObject.DontDestroyOnLoad(obj);
            BGM = obj.AddComponent<AudioSource>();
        }

        ResourcesMgr.Instance.LoadAsync<AudioClip>("Music/" + name, (clip) =>
        {
            BGM.clip = clip;
            BGM.loop = true;
            BGM.volume = BGMvolume;
            BGM.Play();
        });
    }

    /// <summary>
    /// 停止背景音乐
    /// </summary>
    public void StopBGM()
    { 
        if (BGM != null)
        {
            BGM.Stop();
        }
    }

    /// <summary>
    /// 暂停背景音乐
    /// </summary>
    public void PauseBGM()
    {
        if (BGM != null)
        {
            BGM.Pause();
        }
    }

    /// <summary>
    /// 设置背景音乐音量
    /// </summary>
    /// <param name="volume">音量的值</param>
    public void SetBGMVolume(float volume)
    {
        BGMvolume = volume;
        if (BGM != null)
        {
            BGM.volume = volume;
        }
    }
}
