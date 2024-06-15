using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    /// <summary>
    /// 关卡初始阳光数量
    /// </summary>
    public int sunStartNum;


    /// <summary>
    /// 关卡掉落阳光的速度
    /// </summary>
    public float sunFullSpeed;


    /// <summary>
    /// 关卡开始的准备时间
    /// </summary>
    public float startWaitTime;

    /// <summary>
    /// 关卡波数列表
    /// </summary>
    public List<WaveData> waveList = new List<WaveData>();

}
