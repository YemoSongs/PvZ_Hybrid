using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 僵尸生成器
/// </summary>
public class ZombieSpawner : MonoBehaviour
{
    
    public Transform spawnPoint; // 定义生成点，可以在Unity编辑器中设置


    public async Task StartSpawningZombiesAsync(string zombieName)
    {
        // 使用TaskCompletionSource来包装异步加载
        var tcs = new TaskCompletionSource<GameObject>();

        // 异步加载资源
        ABResMgr.Instance.LoadResAsync<GameObject>("zombie", zombieName, (res) =>
        {
            if (res != null)
            {
                tcs.SetResult(res);
            }
            else
            {
                tcs.SetException(new System.Exception("Failed to load zombie resource"));
            }
        });

        // 等待资源加载完成
        GameObject zombiePrefab = await tcs.Task;

        if(spawnPoint != null)
        {
            // 生成僵尸
            Instantiate(zombiePrefab, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Spawn point is null");
        }
  
    }

}
