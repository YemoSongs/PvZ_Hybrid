using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 僵尸生成器
/// </summary>
public class ZombieSpawner : MonoBehaviour
{
    public List<GameObject> zombieList = new List<GameObject>();
    public Transform spawnPoint; // 定义生成点，可以在Unity编辑器中设置

    public void StartSpawningZombies(int num)
    {
        if (zombieList.Count > 0 && num > 0)
        {
            StartCoroutine(SpawnZombiesCoroutine(num));
        }
    }

    private IEnumerator SpawnZombiesCoroutine(int num)
    {
        for (int i = 0; i < num; i++)
        {
            int zombieIndex = Random.Range(0, zombieList.Count);
            Instantiate(zombieList[zombieIndex], spawnPoint.position, Quaternion.identity);

            // 随机等待一段时间再生成下一个僵尸
            float waitTime = Random.Range(2, 5); // 随机等待2到5秒
            yield return new WaitForSeconds(waitTime);
        }
    }
}
