using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public static AssetManager Instance;
    public System.Action<float> onProgress;
    private Dictionary<string, GameObject> dicPrefabs = new Dictionary<string, GameObject>();

    private void Awake()
    {
        AssetManager.Instance = this;
    }

    public void LoadAllAssets(SceneArgs args)
    {
        List<AssetData> assetData = DataManager.Instance.GetAssetDatas();
        for (int i = 0; i < assetData.Count; i++)
        {
            //AssetData data = assetData[i];
            //var fullPath = string.Format("{0}/{1}", data.path, data.prefab_name);
            //Debug.Log(fullPath);
            //this.StartCoroutine(this.LoadAsync(fullPath));

            string path = string.Format("{0}/game_info.json", Application.persistentDataPath);
            this.StartCoroutine(this.LoadAsync(path, args));
        }
    }

    private int loadedAssetCount = 0;
    public IEnumerator LoadAsync(string path, SceneArgs args)
    {
        //var req = Resources.LoadAsync<GameObject>(path);
        //yield return req;
        //테스트
        //var arr = path.Split('/');
        //var key = arr[arr.Length - 1];
        //this.dicPrefabs.Add(key, (GameObject)req.asset);
        //++this.loadedAssetCount;
        //Debug.LogFormat("{0}/{1}\t{2}", this.loadedAssetCount, DataManager.Instance.GetAssetDatas().Count, path);
        if (args.nextSceneType == "SanctuaryScene")
        {
            // 인포 로딩**********************
            //생추어리 씬으로 전환시 필요한 인포 이닛 + 불러오기 + 온 프로그래스 임시 값입니다.
            Debug.Log("to sancturay");
            this.onProgress(0);
            yield return null;
            InfoManager.instance.LoadStatInfo();
            this.onProgress(0.5f);
            yield return null;
            if (!args.isFromTitle)
                InfoManager.instance.InitInventoryInfo();
            //var per = (float)this.loadedAssetCount / DataManager.Instance.GetAssetDatas().Count;
            //임시
            var per = 1;
            this.onProgress(per);
        }
        else if (args.nextSceneType == "DungeonScene")
        {
            //던전 씬으로 전환시 필요한 인포 이닛 + 불러오기 + 온 프로그래스 임시 값입니다.
            Debug.Log("to dungeon");
            this.onProgress(0);
            yield return null;
            InfoManager.instance.InitDungeonInfo();
            this.onProgress(0.3f);
            yield return null;
            InfoManager.instance.InitCharactorlisticsAndGunProficiency();
            this.onProgress(0.6f);
            yield return null;
            InfoManager.instance.InitDungeonGoldAmount();
            this.onProgress(0.9f);
            yield return null;
            InfoManager.instance.LoadStatInfo();
            InfoManager.instance.possessionAmountInfo.totalDungeonEther = 0;
            //var per = (float)this.loadedAssetCount / DataManager.Instance.GetAssetDatas().Count;
            //임시
            var per = 1;
            this.onProgress(per);
            //Debug.LogFormat("<color=red>dungeongold:{0}</color>",InfoManager.instance.possessionAmountInfo.dungeonGoldAmount);       
        }

    }

    /// <summary>
    /// get prefab by prefab_name
    /// </summary>
    /// <param name="key">prefab name</param>
    /// <returns></returns>
    public GameObject GetPrefab(string key)
    {
        return this.dicPrefabs[key];
    }
}
