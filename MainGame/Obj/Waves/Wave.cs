using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Wave 
{
    public readonly WaveData data;

    private int[] zombies = new int[] { };

    public Wave(WaveData data)
    {
        this.data = data;

        zombies = GenerateRandomXValues(data.zombieNum, data.zombieList);
    }

    public async Task StartWaveAsync(List<ZombieSpawner> spawners)
    {
        for (int i = 0; i < zombies.Length; i++)
        {
            int num = Random.Range(0, spawners.Count);

            await spawners[num].StartSpawningZombiesAsync(GetZombieName(zombies[i]));

            // 随机等待时间
            float delay = Random.Range(4f, 8f); // 你可以根据需要调整最小和最大等待时间
            await Task.Delay((int)(delay * 1000)); // Task.Delay 以毫秒为单位
        }
    }


    private string GetZombieName(int id)
    {
        switch (id)
        {
            case 0:
                return "NormalZombie";
            case 1:
                break;
        }
        return "NormalZombie";
    }

    public int[] GenerateRandomXValues(int num, List<Vector2> vectorList)
    {
        int[] result = new int[num];

        // 计算所有 y 值的和
        float totalY = vectorList.Sum(v => v.y);

        // 创建一个累积概率数组，用于生成随机数
        float[] cumulativeProbabilities = new float[vectorList.Count];
        float cumulative = 0;
        for (int i = 0; i < vectorList.Count; i++)
        {
            cumulative += vectorList[i].y / totalY;
            cumulativeProbabilities[i] = cumulative;
        }

        System.Random random = new System.Random();

        // 生成随机数，并根据概率选择对应的 x 值
        for (int i = 0; i < num; i++)
        {
            float rand = (float)random.NextDouble();
            for (int j = 0; j < cumulativeProbabilities.Length; j++)
            {
                if (rand <= cumulativeProbabilities[j])
                {
                    result[i] = (int)vectorList[j].x;
                    break;
                }
            }
        }



        for (int i = 0; i < result.Length; i++)
        {
            Debug.Log(result[i]);
        }


        return result;
    }
}
