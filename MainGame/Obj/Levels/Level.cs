using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Level 
{

    public readonly LevelData data;
    private List<Wave> waveList = new List<Wave>();

    private Wave nowWave;
    private int nowWaveIndex = 0;
    private int waveNum;

    public Level(LevelData data)
    {
        this.data = data;

        waveNum = data.waveList.Count;

    }


    public void StartLevel()
    {
        int timer = TimerMgr.Instance.CreateTimer(false, (int)(data.startWaitTime * 1000), () =>
        {
            CreatWave();
            LevelMgr.Instance.isWaitOver = true;
        },0);

        TimerMgr.Instance.StartTimer(timer);
    }


    public int GetStartSunNum()
    {
        return data.sunStartNum;
    }


    private void CreatWave()
    {

        for (int i = 0; i < waveNum; i++)
        {
            waveList.Add(new Wave(data.waveList[i]));
        }

    }

    public async void NextWave()
    {
        LevelMgr.Instance.GenerateWaved = true;

        nowWave = waveList[nowWaveIndex];

        await nowWave.StartWaveAsync(LevelMgr.Instance.spwns);

        await Task.Delay((int)(nowWave.data.waveWaitTime*1000));

        LevelMgr.Instance.GenerateWaved = false;

        nowWaveIndex++;

        if (nowWaveIndex >= waveNum)
        {
            LevelMgr.Instance.LevelOver();
        }

    }





}
