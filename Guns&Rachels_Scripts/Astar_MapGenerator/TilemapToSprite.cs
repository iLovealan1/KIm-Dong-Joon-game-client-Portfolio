using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapToSprite : MonoBehaviour
{
    [SerializeField] private GameObject TargetObj;

    private List<Tilemap> tilemapList;
    public int pixelPerUnit = 100;

    private void ExtrackTileMapObject()
    {
        this.DisableChildrenWithoutTilemapRecursively(this.TargetObj.transform);
        this.tilemapList = this.TargetObj.GetComponentsInChildren<Tilemap>().ToList<Tilemap>();
        this.MakePNG();
    }

    void MakePNG()
    {
        this.tilemapList.ForEach(tilemap => { tilemap.gameObject.SetActive(false); });

        Debug.Log("Start");
        for (int i = 0; i < tilemapList.Count; i++)
        {
            this.RefreshComponent();
            this.tilemapList[i].gameObject.SetActive(true);
            Vector3Int size = this.tilemapList[i].cellBounds.size;

            // Create a render texture
            RenderTexture renderTexture = new RenderTexture(size.x * this.pixelPerUnit, size.y * this.pixelPerUnit, 32);

            // Create a tilemap renderer
            TilemapRenderer tilemapRenderer = this.gameObject.AddComponent<TilemapRenderer>();
            tilemapRenderer.sortOrder = TilemapRenderer.SortOrder.TopRight;
            tilemapRenderer.mode = TilemapRenderer.Mode.Individual;
            tilemapRenderer.sortingLayerName = "Default"; 
            Tilemap tilemapComponent = tilemapRenderer.GetComponent<Tilemap>();
            tilemapComponent.ClearAllTiles();
            foreach (var pos in this.tilemapList[i].cellBounds.allPositionsWithin)
            {
                Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                if (!this.tilemapList[i].HasTile(localPlace)) continue;

                Vector3 place = this.tilemapList[i].CellToWorld(localPlace);
                tilemapComponent.SetTile(localPlace, this.tilemapList[i].GetTile(localPlace));
            }

            // Render to the render texture
            tilemapComponent.RefreshAllTiles();
            Camera camera = Camera.main;
            camera.targetTexture = renderTexture;
            camera.orthographicSize = Mathf.Max(size.x, size.y) / 2f;
            camera.aspect = (float)size.x / size.y;
            camera.Render();
            camera.targetTexture = null;

            // Create a sprite
            Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            RenderTexture.active = renderTexture;
            texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture.Apply();
            RenderTexture.active = null;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), this.pixelPerUnit);

            byte[] bytes = texture.EncodeToPNG();
            string filePath = Application.dataPath + string.Format("/{0}.png", this.tilemapList[i].gameObject.name);
            File.WriteAllBytes(filePath, bytes);

            this.tilemapList[i].gameObject.SetActive(false);
        }

        Debug.Log("End");
    }
    private void DisableChildrenWithoutTilemapRecursively(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.GetComponent<Tilemap>() == null)
            {
                child.gameObject.SetActive(false);
            }

            this.DisableChildrenWithoutTilemapRecursively(child);
        }
    }

    private void RefreshComponent()
    {
        DestroyImmediate(this.GetComponent<TilemapRenderer>());
        DestroyImmediate(this.GetComponent<Tilemap>());
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.ExtrackTileMapObject();
        }
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    for(int i = 0; i<10;  i++) 
        //    {
        //        var ran = UnityEngine.Random.Range(0, 11);
        //        Debug.LogFormat("<color=blue>Unity.Random.Range : {0}</color>", ran);
        //    }
        //}
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //         var ran = random.Next(0,11);
        //         Debug.LogFormat("<color=red>System.Random.Next : {0}</color>", ran);
        //    }
        //}
    }
}
