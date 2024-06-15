using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 关卡管理器
/// </summary>
public class LevelMgr : BaseManager<LevelMgr>
{

    public List<ZombieSpawner> spwns;

    public Level level;

    public int nowSunNum;

    public bool GenerateWaved = false; 
    public bool isWaitOver = false;


    private LevelMgr()
    {

    }


    public void StartLevel(Level lvl)
    {
        level = lvl;
        level.StartLevel();
        MonoMgr.Instance.AddUpdateListener(LevelUpdate);
        ChangeSunNum(lvl.GetStartSunNum());


        ABResMgr.Instance.LoadResAsync<GameObject>("plant", "SunSpawner", (res) =>
        {
            GameObject sunSpawner = GameObject.Instantiate(res.gameObject,Vector3.up*30,Quaternion.identity);
            sunSpawner.GetComponent<SunSpawner>().StartFullSun(lvl.data.sunFullSpeed);
        });

    }



    public void ChangeSunNum(int num)
    {
        nowSunNum += num;
        UIMgr.Instance.GetPanel<PlantCardsPanel>((panel) =>
        {
            panel.sunNum.text = nowSunNum.ToString();
        });
    }



    private void LevelUpdate()
    {
        if (!GenerateWaved && isWaitOver)
        {
            level.NextWave();
        }
    }

    public void LevelOver()
    {
        GenerateWaved = true;
        MonoMgr.Instance.RemoveUpdateListener(LevelUpdate);
        Debug.Log("level结束！！！！");
    }


}
