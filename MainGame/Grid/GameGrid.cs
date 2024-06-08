using UnityEngine;

public class GameGrid : MonoBehaviour
{
    private Grid<GridCell> grid;

    public  int width = 9;
    public  int height = 5;
    public  float cellSize;
    public Vector3 originPosition;


    public void StartGrid()
    {
        grid = new Grid<GridCell>(width, height, cellSize, originPosition, (Grid<GridCell> g, int x, int y) => new GridCell(x,y));
    }



    public bool CanPlacePlant(Vector3 worldPosition)
    {
        int x, y;
        grid.GetXY(worldPosition, out x, out y);
        GridCell cell = grid.GetGridObject(x, y);
        if (cell == null)
            return false;

        return cell.IsEmpty;
    }



    public bool PlacePlant(Vector3 worldPosition, GameObject plant)
    {
        int x, y;
        grid.GetXY(worldPosition, out x, out y);
        GridCell cell = grid.GetGridObject(x, y);
        print(cell.pos);
        if (cell.IsEmpty)
        {
            cell.PlaceObject(plant);
            cell.gameGrid = this;
            plant.transform.SetParent(transform, false);
            plant.transform.position = grid.GetWorldPositionCenter(x, y);

            grid.TriggerGridObjectChanged(x, y);
            return true;
        }
        return false;
    }

    public bool RemovePlant(Vector3 worldPosition)
    {
        int x, y;
        grid.GetXY(worldPosition, out x, out y);
        GridCell cell = grid.GetGridObject(x, y);
        if (!cell.IsEmpty && cell.Occupant.CompareTag("Plant"))
        {
            cell.RemoveObject();
            grid.TriggerGridObjectChanged(x, y);
            return true;
        }
        return false;
    }

    public bool PlaceZombie(Vector3 worldPosition, GameObject zombie)
    {
        int x, y;
        grid.GetXY(worldPosition, out x, out y);
        GridCell cell = grid.GetGridObject(x, y);
        if (cell.IsEmpty)
        {
            cell.PlaceObject(zombie);
            grid.TriggerGridObjectChanged(x, y);
            return true;
        }
        return false;
    }

    public bool RemoveZombie(Vector3 worldPosition)
    {
        int x, y;
        grid.GetXY(worldPosition, out x, out y);
        GridCell cell = grid.GetGridObject(x, y);
        if (!cell.IsEmpty && cell.Occupant.CompareTag("Zombie"))
        {
            cell.RemoveObject();
            grid.TriggerGridObjectChanged(x, y);
            return true;
        }
        return false;
    }


    private void OnDrawGizmos()
    {
        if (grid != null)
        {
            for (int x = 0; x < grid.GetWidth(); x++)
            {
                for (int y = 0; y < grid.GetHeight(); y++)
                {
                    Gizmos.color = grid.GetGridObject(x, y).IsEmpty ? Color.green : Color.red;
                    Gizmos.DrawWireCube(grid.GetWorldPosition(x, y) + new Vector3(grid.GetCellSize(), grid.GetCellSize()) * 0.5f, new Vector3(grid.GetCellSize(), grid.GetCellSize()));
                }
            }
        }
    }


}
