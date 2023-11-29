using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class AtlasManager : MonoBehaviour
{
    public static AtlasManager instance;

    public List<SpriteAtlas> AtlasList;
    public Dictionary<string, SpriteAtlas> dicIconAtals = new Dictionary<string, SpriteAtlas>();

    public void Awake()
    {
        instance = this;

        foreach (var atlas in AtlasList)
        {
            var name = atlas.name.Replace("Atlas", "");
            this.dicIconAtals.Add(name, atlas);
        }
    }

    public SpriteAtlas GetAtlasByName(string name)
    {
        return this.dicIconAtals[name];
    }

}
