using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutElement
{
    private Vector2 position;
    public bool occupied { get; private set; }
    public LayoutElement(Vector2 position)
    {
        this.position = position;
        occupied = false;
    }
    public Vector2 GetPosition()
    {
        return position;
    }
    public void SetOccupied(bool occupied)
    {
        this.occupied = occupied;
    }
}
public class LayoutGroup
{
    public enum Orientations
    {
        Horizontal,
        Vertical
    }
    private int numElements;
    private float spacing;
    private Orientations orientation;
    private List<LayoutElement> layoutElements = new List<LayoutElement>();
    public LayoutGroup(int numElements, float spacing, float yPosition)
    {
        this.numElements = numElements;
        this.spacing = spacing;
        for (int i = 0; i < numElements; i++)
        {
            layoutElements.Add(new LayoutElement(new Vector2(i * spacing, yPosition)));
        }
    }
    public Vector2 GetPosition(int index)
    {
        return layoutElements[index].GetPosition();
    }
    public void SetOccupied(int index, out List<int> moveIndexs)
    {
        layoutElements[index].SetOccupied(false);
        moveIndexs = MoveElements(index);
    }

    public void SetOccupied(int index)
    {
        layoutElements[index].SetOccupied(true);
    }

    private List<int> MoveElements(int index)
    {
        List<int> moveIndexs = new List<int>();
        for (int i = index + 1; i < layoutElements.Count; i++)
        {
            if (layoutElements[i].occupied)
            {
                moveIndexs.Add(i);
            }
        }
        return moveIndexs;
    }
    private List<int> GetElementsToMove()
    {
        List<int> elementsToMove = new List<int>();
        for(int i = 0; i<layoutElements.Count; i++)
        {
            bool move = CheckIfMoveElement(i);
            if (move) elementsToMove.Add(i);
        }
        return elementsToMove;
    }
    private bool CheckIfMoveElement(int index)
    {
        if (index == 0) return false;
        if(layoutElements[index - 1].occupied)
        {
            return false;
        }
        if (layoutElements[index].occupied)
        {
            return true;
        }
        return false;
        
    }
    private List<LayoutElement> GetUnoccupiedElements()
    {
        List<LayoutElement> unoccupiedElements = new List<LayoutElement>();
        foreach(LayoutElement element in layoutElements)
        {
            if (!element.occupied)
            {
                unoccupiedElements.Add(element);
            }
        }
        return unoccupiedElements;
    }

}
