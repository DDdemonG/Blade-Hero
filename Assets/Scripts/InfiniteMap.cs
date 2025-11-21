using System.Collections.Generic;
using UnityEngine;

public class InfiniteMap : MonoBehaviour
{
    public Transform player;        
    public GameObject tilePrefab;  

    public int halfWidth = 2;       
    public int halfHeight = 2;      

    private Dictionary<Vector2Int, GameObject> tiles = new Dictionary<Vector2Int, GameObject>();
    private float tileWidth;
    private float tileHeight;

    void Start()
    {
        SpriteRenderer sr = tilePrefab.GetComponent<SpriteRenderer>();
        tileWidth = sr.bounds.size.x;
        tileHeight = sr.bounds.size.y;
    }

    void Update()
    {
        int px = Mathf.RoundToInt(player.position.x / tileWidth);
        int py = Mathf.RoundToInt(player.position.y / tileHeight);


        for (int x = px - halfWidth; x <= px + halfWidth; x++)
        {
            for (int y = py - halfHeight; y <= py + halfHeight; y++)
            {
                Vector2Int coord = new Vector2Int(x, y);

                if (!tiles.ContainsKey(coord))
                {
                    Vector3 pos = new Vector3(x * tileWidth, y * tileHeight, 1);
                    GameObject tile = Instantiate(tilePrefab, pos, Quaternion.identity);
                    tiles.Add(coord, tile);
                }
            }
        }

        List<Vector2Int> toRemove = new List<Vector2Int>();
        foreach (var tile in tiles)
        {
            if (Mathf.Abs(tile.Key.x - px) > halfWidth + 1 ||
                Mathf.Abs(tile.Key.y - py) > halfHeight + 1)
            {
                Destroy(tile.Value);
                toRemove.Add(tile.Key);
            }
        }
       
        foreach (var key in toRemove)
        {
            tiles.Remove(key);
        }
    }
}

