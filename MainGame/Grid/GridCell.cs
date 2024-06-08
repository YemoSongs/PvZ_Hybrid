using UnityEngine;

public class GridCell
{

    /// <summary>
    /// 在网格中的坐标
    /// </summary>
    public Vector2 pos;

    public GameGrid gameGrid;

    public bool IsEmpty { get; private set; }
    public GameObject Occupant { get; private set; }

    public GridCell(int x,int y)
    {
        IsEmpty = true;
        Occupant = null;
        pos = new Vector2(x,y);
    }

    public void PlaceObject(GameObject obj)
    {
        IsEmpty = false;
        Occupant = obj;
        obj.GetComponent<Plant>().gridCell = this;
    }

    public void RemoveObject()
    {
        IsEmpty = true;
        Occupant = null;
    }

    public override string ToString()
    {
        return IsEmpty ? "Empty" : "Occupied";
    }
}
