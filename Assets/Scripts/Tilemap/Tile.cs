using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Tile 
{
    private static float tileSize = 1f;

    private Vector2 position;
    private BaseCounter counter;
    public Tile(Vector2 position, BaseCounter counter)
    {
        this.position = position;
        this.counter = counter;
    }   
    public Vector2 GetPosition()
    {
        return position;
    }
    public BaseCounter GetCounter()
    {
        return counter;
    }
    public static float GetTileSize()
    {
        return tileSize;
    }
}
