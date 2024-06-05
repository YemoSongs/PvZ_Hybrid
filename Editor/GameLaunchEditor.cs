﻿using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameLaunch))]
public class GameLaunchEditor : Editor
{
    // 存储对目标脚本的引用
    private GameLaunch gameLaunch;

    // 定义语言选项
    private string[] languageOptions = new string[] { "Chinese", "English" };


    private void OnEnable()
    {
        // 获取目标脚本的引用
        gameLaunch = (GameLaunch)target;
    }
    public override void OnInspectorGUI()
    {
        // 开始检查GUI的更改
        EditorGUI.BeginChangeCheck();

        // 自定义Inspector界面
        EditorGUILayout.LabelField("GameLaunch——游戏入口", EditorStyles.boldLabel);

        //设置是否是调试模式
        gameLaunch.isDebug = EditorGUILayout.Toggle("Debug", gameLaunch.isDebug);


        // 本地化选项
        int selectedLanguageIndex = System.Array.IndexOf(languageOptions, gameLaunch.language);
        if (selectedLanguageIndex == -1) selectedLanguageIndex = 0; // 如果找不到，默认选择第一个

        selectedLanguageIndex = EditorGUILayout.Popup("选择语言", selectedLanguageIndex, languageOptions);
        gameLaunch.language = languageOptions[selectedLanguageIndex];

        // 创建输入字段
        gameLaunch.musicValue = EditorGUILayout.Slider("背景音乐大小",gameLaunch.musicValue,0,1);
        gameLaunch.soundValue = EditorGUILayout.Slider("音效大小",gameLaunch.soundValue,0,1);



        // 检查是否有任何字段发生更改
        if (EditorGUI.EndChangeCheck())
        {
            // 标记对象已更改
            EditorUtility.SetDirty(gameLaunch);

            if(EditorApplication.isPlaying)
            {
                MusicMgr.Instance.ChangeBKMusicValue(gameLaunch.musicValue);

                MusicMgr.Instance.ChangeSoundValue(gameLaunch.soundValue);

                LocalizationManager.Instance.SetLanguage(gameLaunch.language);

                ABResMgr.Instance.isDebug = gameLaunch.isDebug;
            }

        }
    }



}
