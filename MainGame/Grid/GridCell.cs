using UnityEngine;

public class GridCell
{
    public bool IsEmpty { get; private set; }
    public GameObject Occupant { get; private set; }

    public GridCell()
    {
        IsEmpty = true;
        Occupant = null;
    }

    public void PlaceObject(GameObject obj)
    {
        IsEmpty = false;
        Occupant = obj;
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
