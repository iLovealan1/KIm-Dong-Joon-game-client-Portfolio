using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class DataManager
{
    private Dictionary<int, AssetData> dicAssetDatas;
    private const string ASSET_DATA_PATH = "Data/asset_data";


    public void LoadAssetDatas()
    {
        var asset = Resources.Load<TextAsset>(ASSET_DATA_PATH);
        var json = asset.text;
        this.dicAssetDatas = JsonConvert.DeserializeObject<AssetData[]>(json).ToDictionary(x => x.id);
    }

    public List<AssetData> GetAssetDatas()
    {
        //테스트용 역직렬화로 선언해야 함
        this.dicAssetDatas = new Dictionary<int, AssetData>();
        AssetData testAssetData = null;
        this.dicAssetDatas.Add(1, testAssetData);
        return this.dicAssetDatas.Values.ToList();
    }
}
