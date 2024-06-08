using UnityEngine;

public class GridCell
{

    /// <summary>
    /// 在网格中的坐标
    /// </summary>
    public Vector2Int pos;

    public GameGrid gameGrid;

    public bool IsEmpty { get; private set; }
    public GameObject Occupant { get; private set; }

    public GridCell(int x,int y,GameGrid gameGrid)
    {
        IsEmpty = true;
        Occupant = null;
        pos = new Vector2Int(x,y);
        this.gameGrid = gameGrid;
    }

    public void PlaceObject(GameObject obj)
    {
        IsEmpty = false;
        Occupant = obj;
        obj.GetComponent<Plant>().gridCell = this;
        gameGrid.TriggerGridObjectChanged(pos.x, pos.y);
    }

    public void RemoveObject()
    {
        IsEmpty = true;
        Occupant = null;
        gameGrid.TriggerGridObjectChanged(pos.x, pos.y);
    }

    public override string ToString()
    {
        return IsEmpty ? "Empty" : "Occupied";
    }
}
