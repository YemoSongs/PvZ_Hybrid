using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

//知识点
//字典
//协程
//AB包相关API
//委托
//lambda表达式
//单例模式基类——>观看Unity小框架视频 进行学习
public class ABMgr : SingletonAutoMono<ABMgr>
{
    //主包
    private AssetBundle mainAB = null;
    //主包依赖获取配置文件
    private AssetBundleManifest manifest = null;

    //选择存储 AB包的容器
    //AB包不能够重复加载 否则会报错
    //字典知识 用来存储 AB包对象
    private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();

    /// <summary>
    /// 获取AB包加载路径
    /// </summary>
    private string PathUrl
    {
        get
        {
            return Application.streamingAssetsPath + "/";
        }
    }

    /// <summary>
    /// 主包名 根据平台不同 报名不同
    /// </summary>
    private string MainName
    {
        get
        {
#if UNITY_IOS
            return "IOS";
#elif UNITY_ANDROID
            return "Android";
#else
            return "PC";
#endif
        }
    }

    /// <summary>
    /// 加载主包 和 配置文件
    /// 因为加载所有包是 都得判断 通过它才能得到依赖信息
    /// 所以写一个方法
    /// </summary>
    private void LoadMainAB()
    {
        if( mainAB == null )
        {
            mainAB = AssetBundle.LoadFromFile( PathUrl + MainName);
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
    }


    /// <summary>
    /// 泛型异步加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="callBack"></param>
    public void LoadResAsync<T>(string abName, string resName, UnityAction<T> callBack, bool isSync = false) where T:Object
    {
        StartCoroutine(ReallyLoadResAsync<T>(abName, resName, callBack, isSync));
    }
    //正儿八经的 协程函数
    private IEnumerator ReallyLoadResAsync<T>(string abName, string resName, UnityAction<T> callBack, bool isSync) where T : Object
    {
       

        ////加载主包
        //LoadMainAB();
        ////获取依赖包
        //string[] strs = manifest.GetAllDependencies(abName);
        //for (int i = 0; i < strs.Length; i++)
        //{
        //    //还没有加载过该AB包
        //    if (!abDic.ContainsKey(strs[i]))
        //    {
        //        //同步加载
        //        if(isSync)
        //        {
        //            AssetBundle ab = AssetBundle.LoadFromFile(PathUrl + strs[i]);
        //            abDic.Add(strs[i], ab);
        //        }
        //        //异步加载
        //        else
        //        {
        //            //一开始异步加载 就记录 如果此时的记录中的值 是null 那证明这个ab包正在被异步加载
        //            abDic.Add(strs[i], null);
        //            AssetBundleCreateRequest req = AssetBundle.LoadFromFileAsync(PathUrl + strs[i]);
        //            yield return req;
        //            //异步加载结束后 再替换之前的null  这时 不为null 就证明加载结束了
        //            abDic[strs[i]] = req.assetBundle;
        //        }
        //    }
        //    //就证明 字典中已经记录了一个AB包相关信息了
        //    else
        //    {
        //        //如果字典中记录的信息是null 那就证明正在加载中
        //        //我们只需要等待它加载结束 就可以继续执行后面的代码了
        //        while (abDic[strs[i]] == null)
        //        {
        //            //只要发现正在加载中 就不停的等待一帧 下一帧再进行判断
        //            yield return 0;
        //        }
        //    }
        //}


        yield return LoadDependencies(abName, isSync);

        //加载目标包
        if (!abDic.ContainsKey(abName))
        {
            //同步加载
            if (isSync)
            {
                AssetBundle ab = AssetBundle.LoadFromFile(PathUrl + abName);
                abDic.Add(abName, ab);
            }
            else
            {
                //一开始异步加载 就记录 如果此时的记录中的值 是null 那证明这个ab包正在被异步加载
                abDic.Add(abName, null);
                AssetBundleCreateRequest req = AssetBundle.LoadFromFileAsync(PathUrl + abName);
                yield return req;
                //异步加载结束后 再替换之前的null  这时 不为null 就证明加载结束了
                abDic[abName] = req.assetBundle;
            }
        }
        else
        {
            //如果字典中记录的信息是null 那就证明正在加载中
            //我们只需要等待它加载结束 就可以继续执行后面的代码了
            while (abDic[abName] == null)
            {
                //只要发现正在加载中 就不停的等待一帧 下一帧再进行判断
                yield return 0;
            }
        }

        //同步加载AB包中的资源
        if(isSync)
        {
            //即使是同步加载 也需要使用回调函数传给外部进行使用
            T res = abDic[abName].LoadAsset<T>(resName);
            callBack(res);
        }
        //异步加载包中资源
        else
        {
            AssetBundleRequest abq = abDic[abName].LoadAssetAsync<T>(resName);
            yield return abq;

            callBack(abq.asset as T);
        }
    }

    /// <summary>
    /// Type异步加载资源
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="type"></param>
    /// <param name="callBack"></param>
    public void LoadResAsync(string abName, string resName, System.Type type, UnityAction<Object> callBack, bool isSync = false)
    {
        StartCoroutine(ReallyLoadResAsync(abName, resName, type, callBack, isSync));
    }

    private IEnumerator ReallyLoadResAsync(string abName, string resName, System.Type type, UnityAction<Object> callBack, bool isSync)
    {
        ////加载主包
        //LoadMainAB();
        ////获取依赖包
        //string[] strs = manifest.GetAllDependencies(abName);
        //for (int i = 0; i < strs.Length; i++)
        //{
        //    //还没有加载过该AB包
        //    if (!abDic.ContainsKey(strs[i]))
        //    {
        //        //同步加载
        //        if (isSync)
        //        {
        //            AssetBundle ab = AssetBundle.LoadFromFile(PathUrl + strs[i]);
        //            abDic.Add(strs[i], ab);
        //        }
        //        //异步加载
        //        else
        //        {
        //            //一开始异步加载 就记录 如果此时的记录中的值 是null 那证明这个ab包正在被异步加载
        //            abDic.Add(strs[i], null);
        //            AssetBundleCreateRequest req = AssetBundle.LoadFromFileAsync(PathUrl + strs[i]);
        //            yield return req;
        //            //异步加载结束后 再替换之前的null  这时 不为null 就证明加载结束了
        //            abDic[strs[i]] = req.assetBundle;
        //        }
        //    }
        //    //就证明 字典中已经记录了一个AB包相关信息了
        //    else
        //    {
        //        //如果字典中记录的信息是null 那就证明正在加载中
        //        //我们只需要等待它加载结束 就可以继续执行后面的代码了
        //        while (abDic[strs[i]] == null)
        //        {
        //            //只要发现正在加载中 就不停的等待一帧 下一帧再进行判断
        //            yield return 0;
        //        }
        //    }
        //}


        yield return LoadDependencies(abName, isSync);

        //加载目标包
        if (!abDic.ContainsKey(abName))
        {
            //同步加载
            if (isSync)
            {
                AssetBundle ab = AssetBundle.LoadFromFile(PathUrl + abName);
                abDic.Add(abName, ab);
            }
            else
            {
                //一开始异步加载 就记录 如果此时的记录中的值 是null 那证明这个ab包正在被异步加载
                abDic.Add(abName, null);
                AssetBundleCreateRequest req = AssetBundle.LoadFromFileAsync(PathUrl + abName);
                yield return req;
                //异步加载结束后 再替换之前的null  这时 不为null 就证明加载结束了
                abDic[abName] = req.assetBundle;
            }
        }
        else
        {
            //如果字典中记录的信息是null 那就证明正在加载中
            //我们只需要等待它加载结束 就可以继续执行后面的代码了
            while (abDic[abName] == null)
            {
                //只要发现正在加载中 就不停的等待一帧 下一帧再进行判断
                yield return 0;
            }
        }

        if(isSync)
        {
            Object res = abDic[abName].LoadAsset(resName, type);
            callBack(res);
        }
        else
        {
            //异步加载包中资源
            AssetBundleRequest abq = abDic[abName].LoadAssetAsync(resName, type);
            yield return abq;

            callBack(abq.asset);
        }
        
    }

    /// <summary>
    /// 名字 异步加载 指定资源
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="callBack"></param>
    public void LoadResAsync(string abName, string resName, UnityAction<Object> callBack, bool isSync = false)
    {
        StartCoroutine(ReallyLoadResAsync(abName, resName, callBack, isSync));
    }

    private IEnumerator ReallyLoadResAsync(string abName, string resName, UnityAction<Object> callBack, bool isSync)
    {
        ////加载主包
        //LoadMainAB();
        ////获取依赖包
        //string[] strs = manifest.GetAllDependencies(abName);
        //for (int i = 0; i < strs.Length; i++)
        //{
        //    //还没有加载过该AB包
        //    if (!abDic.ContainsKey(strs[i]))
        //    {
        //        //同步加载
        //        if (isSync)
        //        {
        //            AssetBundle ab = AssetBundle.LoadFromFile(PathUrl + strs[i]);
        //            abDic.Add(strs[i], ab);
        //        }
        //        //异步加载
        //        else
        //        {
        //            //一开始异步加载 就记录 如果此时的记录中的值 是null 那证明这个ab包正在被异步加载
        //            abDic.Add(strs[i], null);
        //            AssetBundleCreateRequest req = AssetBundle.LoadFromFileAsync(PathUrl + strs[i]);
        //            yield return req;
        //            //异步加载结束后 再替换之前的null  这时 不为null 就证明加载结束了
        //            abDic[strs[i]] = req.assetBundle;
        //        }
        //    }
        //    //就证明 字典中已经记录了一个AB包相关信息了
        //    else
        //    {
        //        //如果字典中记录的信息是null 那就证明正在加载中
        //        //我们只需要等待它加载结束 就可以继续执行后面的代码了
        //        while (abDic[strs[i]] == null)
        //        {
        //            //只要发现正在加载中 就不停的等待一帧 下一帧再进行判断
        //            yield return 0;
        //        }
        //    }
        //}


        yield return LoadDependencies(abName, isSync);


        //加载目标包
        if (!abDic.ContainsKey(abName))
        {
            //同步加载
            if (isSync)
            {
                AssetBundle ab = AssetBundle.LoadFromFile(PathUrl + abName);
                abDic.Add(abName, ab);
            }
            else
            {
                //一开始异步加载 就记录 如果此时的记录中的值 是null 那证明这个ab包正在被异步加载
                abDic.Add(abName, null);
                AssetBundleCreateRequest req = AssetBundle.LoadFromFileAsync(PathUrl + abName);
                yield return req;
                //异步加载结束后 再替换之前的null  这时 不为null 就证明加载结束了
                abDic[abName] = req.assetBundle;
            }
        }
        else
        {
            //如果字典中记录的信息是null 那就证明正在加载中
            //我们只需要等待它加载结束 就可以继续执行后面的代码了
            while (abDic[abName] == null)
            {
                //只要发现正在加载中 就不停的等待一帧 下一帧再进行判断
                yield return 0;
            }
        }

        if(isSync)
        {
            Object obj = abDic[abName].LoadAsset(resName);
            callBack(obj);
        }
        else
        {

            //异步加载包中资源
            AssetBundleRequest abq = abDic[abName].LoadAssetAsync(resName);
            yield return abq;

            callBack(abq.asset);
        }

    }




    /// <summary>
    /// 加载依赖包
    /// </summary>
    /// <param name="abName">包名</param>
    /// <param name="isSync"></param>
    /// <returns></returns>
    private IEnumerator LoadDependencies(string abName, bool isSync)
    {
        LoadMainAB();
        string[] dependencies = manifest.GetAllDependencies(abName);

        foreach (string dep in dependencies)
        {
            if (!abDic.ContainsKey(dep))
            {
                if (isSync)
                {
                    AssetBundle ab = AssetBundle.LoadFromFile(PathUrl + dep);
                    abDic.Add(dep, ab);
                }
                else
                {
                    abDic.Add(dep, null);
                    AssetBundleCreateRequest req = AssetBundle.LoadFromFileAsync(PathUrl + dep);
                    yield return req;
                    abDic[dep] = req.assetBundle;
                }
            }
            else
            {
                while (abDic[dep] == null)
                {
                    yield return null;
                }
            }
        }
    }


    /// <summary>
    /// 异步加载场景资源
    /// </summary>
    /// <param name="bundlePath"></param>
    /// <param name="sceneName"></param>
    public void LoadSceneFromAssetBundle(string abName, string sceneName, UnityAction callBack = null)
    {
        StartCoroutine(LoadSceneFromAssetBundleCoroutine(abName, sceneName,callBack));
    }

    private IEnumerator LoadSceneFromAssetBundleCoroutine(string abName, string sceneName, UnityAction callBack = null)
    {

        if (!abDic.ContainsKey(abName))
        {

            abDic.Add(abName, null);
            AssetBundleCreateRequest bundleRequest = AssetBundle.LoadFromFileAsync(PathUrl + abName);
            yield return bundleRequest;
            
            AssetBundle bundle = bundleRequest.assetBundle;
            abDic[abName] = bundle;

            if (bundle == null)
            {
                Debug.LogError("Failed to load AssetBundle!");
                yield break;
            }

            if (bundle.isStreamedSceneAssetBundle)
            {

                yield return LoadDependencies(abName, false);

                string[] scenePaths = bundle.GetAllScenePaths();
                foreach (string scenePath in scenePaths)
                {
                    Debug.Log("Scene in AssetBundle: " + scenePath);
                }

                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
                while (!asyncLoad.isDone)
                {
                    yield return null;
                }

                callBack?.Invoke();
            }
            else
            {
                Debug.LogError("The AssetBundle does not contain streamed scenes.");
            }
        }
        else if (abDic.ContainsKey(abName))
        {

            if (abDic[abName].isStreamedSceneAssetBundle)
            {
                yield return LoadDependencies(abName, false);

                string[] scenePaths = abDic[abName].GetAllScenePaths();
                foreach (string scenePath in scenePaths)
                {
                    Debug.Log("Scene in AssetBundle: " + scenePath);
                }

                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
                while (!asyncLoad.isDone)
                {
                    yield return null;
                }

                callBack?.Invoke();
            }
            else
            {
                Debug.LogError("The AssetBundle does not contain streamed scenes.");
            }
        }
        else 
        {
            //如果字典中记录的信息是null 那就证明正在加载中
            //我们只需要等待它加载结束 就可以继续执行后面的代码了
            while (abDic[abName] == null)
            {
                //只要发现正在加载中 就不停的等待一帧 下一帧再进行判断
                yield return 0;
            }
        }
        
    }



    //卸载AB包的方法
    public void UnLoadAB(string name, UnityAction<bool> callBackResult)
    {
        if( abDic.ContainsKey(name) )
        {
            if (abDic[name] == null)
            {
                //代表正在异步加载 没有卸载成功
                callBackResult(false);
                return;
            }
            abDic[name].Unload(false);
            abDic.Remove(name);
            //卸载成功
            callBackResult(true);
        }
    }

    //清空AB包的方法
    public void ClearAB()
    {
        //由于AB包都是异步加载了 因此在清理之前 停止协同程序
        StopAllCoroutines();
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        //卸载主包
        mainAB = null;
    }
}
