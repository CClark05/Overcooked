using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using UnityEngine.UIElements;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance;
    private List<Tile> tiles;
    private void Awake()
    {
        Instance = this;
        tiles = new List<Tile>();
    }
    public void AddTile(Vector2 position, BaseCounter counter)
    {
        tiles.Add(new Tile(position, counter));
    }
    public List<Tile> GetTiles()
    {
        return tiles;
    }

    public bool TryGetBaseCounterFromPosition(Vector2 position, out BaseCounter counter)
    {
        foreach(Tile tile in tiles)
        {
            float minX = tile.GetPosition().x - Tile.GetTileSize() / 2f;
            float minY = tile.GetPosition().y - Tile.GetTileSize() / 2f;
            float maxX = tile.GetPosition().x + Tile.GetTileSize() / 2f;
            float maxY = tile.GetPosition().y + Tile.GetTileSize() / 2f;
            if(position.x >= minX && position.x <= maxX && position.y >= minY && position.y <= maxY)
            {
                counter = tile.GetCounter();
                return true;
            }
            /**
            if (Vector2.Distance(tile.GetPosition(), position) < 0.01f)
            {
                counter = tile.GetCounter();
                return true;
            }
            */
        }
        counter = null;
        return false;
    }

    
}
