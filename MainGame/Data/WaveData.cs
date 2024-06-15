using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "ScriptableObjects/WaveData", order = 1)]
public class WaveData :ScriptableObject
{
    /// <summary>
    /// 这波僵尸总数
    /// </summary>
    public int zombieNum;
    
    /// <summary>
    /// x表示僵尸的ID y表示僵尸的占比
    /// </summary>
    public List<Vector2> zombieList;

    /// <summary>
    /// 这波结束需要等待的时间
    /// </summary>
    public float waveWaitTime;



}
