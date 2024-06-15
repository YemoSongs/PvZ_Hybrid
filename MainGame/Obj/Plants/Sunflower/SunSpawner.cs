using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSpawner : MonoBehaviour
{

    public float minTargetYPosition = -16f; // 阳光下落的最小目标Y位置
    public float maxTargetYPosition = 10f; // 阳光下落的最大目标Y位置



    public void StartFullSun(float spawnInterval)
    {
        StartCoroutine(SpawnSun(spawnInterval));
    }


    private IEnumerator SpawnSun(float spawnInterval)
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            GenerateSun();
        }
    }

    private void GenerateSun()
    {
        // 获取屏幕上方的随机位置
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect * 2;
        float randomX = Random.Range(-screenWidth / 2, screenWidth / 2);
        Vector3 spawnPosition = gameObject.transform.position + new Vector3(randomX, 0, 0);

        // 在随机位置生成阳光
        ABResMgr.Instance.LoadResAsync<GameObject>("bullet", "Sun", (res) =>
        {
            GameObject sun = GameObject.Instantiate(res, spawnPosition, Quaternion.identity);
            sun.GetComponent<Sun>().isFalling = true;


            // 设置阳光下落的目标位置为随机高度
            float randomTargetY = Random.Range(minTargetYPosition, maxTargetYPosition);
            sun.GetComponent<Sun>().targetYPosition = randomTargetY;
        });

    }

}
