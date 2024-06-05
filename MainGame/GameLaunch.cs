﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameLaunch : MonoBehaviour
{

    public string language = "English"; // 默认语言;

    public float musicValue;
    public float soundValue;

    
    void Start()
    {
        //设置本地化为中文
        LocalizationManager.Instance.LoadLocalizedText(language);

        //打开开始界面
        UIMgr.Instance.ShowPanel<BeginPanel>();

        //播放背景音乐
        MusicMgr.Instance.PlayBKMusic("MainTheme");

        //调整音乐音效大小
        MusicMgr.Instance.ChangeBKMusicValue(musicValue);

        MusicMgr.Instance.ChangeSoundValue(soundValue);


    }
}
