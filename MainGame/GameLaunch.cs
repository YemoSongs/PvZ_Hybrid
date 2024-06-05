﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameLaunch : SingletonMono<GameLaunch>
{


    public bool isDebug = true;

    public string language = "English"; // 默认语言;

    public float musicValue;
    public float soundValue;




    void Start()
    {


        //设置运行模式
        ABResMgr.Instance.isDebug = isDebug;


        //设置本地化为中文
        LocalizationManager.Instance.LoadLocalizedText(language);

        //打开开始界面
        UIMgr.Instance.ShowPanel<BeginPanel>();

        //播放背景音乐
        MusicMgr.Instance.PlayBKMusic("MainTheme");

        //调整音乐音效大小
        MusicMgr.Instance.ChangeBKMusicValue(musicValue);

        MusicMgr.Instance.ChangeSoundValue(soundValue);

        print("[GameLaunch]>>>>游戏开始");
    }
}
