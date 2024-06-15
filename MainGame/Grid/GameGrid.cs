using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    private Grid<GridCell> grid;

    public List<ZombieSpawner> spwns;
    

    public  int width = 9;
    public  int height = 5;
    public  float cellSize;
    public Vector3 originPosition;

    /// <summary>
    /// 开始构建网格
    /// </summary>
    public void StartGrid()
    {
        grid = new Grid<GridCell>(width, height, cellSize, originPosition, (Grid<GridCell> g, int x, int y) => new GridCell(x,y,this));

        InitZombieSpawnerPoints(new int[] { 0, 1, 2, 3, 4, } );
    }


    /// <summary>
    /// 判断是否可以种植植物
    /// </summary>
    /// <param name="worldPosition">世界坐标</param>
    /// <returns></returns>
    public bool CanPlacePlant(Vector3 worldPosition)
    {
        int x, y;
        grid.GetXY(worldPosition, out x, out y);
        GridCell cell = grid.GetGridObject(x, y);
        if (cell == null)
            return false;

        return cell.IsEmpty;
    }


    /// <summary>
    /// 种植植物
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <param name="plant"></param>
    /// <returns></returns>
    public bool PlacePlant(Vector3 worldPosition, GameObject plant)
    {
        int x, y;
        grid.GetXY(worldPosition, out x, out y);
        GridCell cell = grid.GetGridObject(x, y);
        print(cell.pos);
        if (cell.IsEmpty)
        {
            cell.PlaceObject(plant);
            
            plant.transform.SetParent(transform, false);
            plant.transform.position = grid.GetWorldPositionCenter(x, y);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 触发单元格变化的函数监听
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void TriggerGridObjectChanged(int x, int y)
    {
        grid.TriggerGridObjectChanged(x, y);
    }

    /// <summary>
    /// 初始化僵尸生成点
    /// </summary>
    /// <param name="spownerRoads">僵尸生成在哪些路，0——4，自下而上</param>
    private void InitZombieSpawnerPoints(int[] spownerRoads)
    {

        for (int i = 0; i < spownerRoads.Length; i++)
        {
            Vector3 lastCellPos = grid.GetWorldPositionCenter(width - 1, spownerRoads[i]);
            print(lastCellPos); 
            Vector3 pos = lastCellPos + Vector3.right*15;

            ABResMgr.Instance.LoadResAsync<GameObject>("zombie", "ZombieSpowner", (res) =>
            {
                GameObject spowner = Instantiate(res.gameObject);
                spowner.transform.position = pos;
                spowner.transform.SetParent(transform.parent, false);
                ZombieSpawner spawner = spowner.GetComponent<ZombieSpawner>();

                //spawner.StartSpawningZombies(3);

                spwns.Add(spawner);

            });
        }
        LevelMgr.Instance.spwns = spwns;
        
    } 





    #region 待定
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

    #endregion

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
