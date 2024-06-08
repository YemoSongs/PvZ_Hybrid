using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Grid<TGridObject>
{

    // 定义一个事件，当网格对象发生变化时触发
    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;

    // 定义事件参数类，包含网格的x和y坐标
    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    private int width;  // 网格宽度
    private int height; // 网格高度
    private float cellSize; // 每个单元格的大小
    private Vector3 originPosition; // 网格的原点位置
    private TGridObject[,] gridArray; // 存储网格对象的二维数组

    // 构造函数，初始化网格
    public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];

        // 使用传入的委托创建每个网格对象
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridObject(this, x, y);
            }
        }

        // 显示调试信息
        bool showDebug = false;
        if (showDebug)
        {
            TextMesh[,] debugTextArray = new TextMesh[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    // 创建文本显示网格对象信息
                    debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y]?.ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 10, Color.white, TextAnchor.MiddleCenter);
                    // 绘制网格线
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 10f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 10f);
                }
            }
            // 绘制外边框线
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 10f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 10f);

            // 订阅事件，当网格对象发生变化时更新调试文本
            OnGridObjectChanged += (object sender, OnGridObjectChangedEventArgs eventArgs) => {
                debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
            };
        }
    }

    /// <summary>
    /// 获取网格宽度
    /// </summary>
    /// <returns></returns>
    public int GetWidth()
    {
        return width;
    }

    /// <summary>
    /// 获取网格高度
    /// </summary>
    /// <returns></returns>
    public int GetHeight()
    {
        return height;
    }

    /// <summary>
    /// 获取单元格大小
    /// </summary>
    /// <returns></returns>
    public float GetCellSize()
    {
        return cellSize;
    }

    /// <summary>
    /// 根据网格坐标获取网格起始点世界坐标
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    /// <summary>
    /// 根据网格坐标获取网格中心点的世界坐标
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Vector3 GetWorldPositionCenter(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition + new Vector3(cellSize, cellSize) * .5f;
    }


    /// <summary>
    /// 根据世界坐标获取网格坐标
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    /// <summary>
    /// 设置指定坐标的网格对象
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="value"></param>
    public void SetGridObject(int x, int y, TGridObject value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            // 触发事件
            if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
        }
    }

    /// <summary>
    /// 手动触发网格对象变化事件
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void TriggerGridObjectChanged(int x, int y)
    {
        if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
    }

    /// <summary>
    /// 根据世界坐标设置网格对象
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <param name="value"></param>
    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x, y, value);
    }

    /// <summary>
    /// 获取指定坐标的网格对象
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(TGridObject);
        }
    }

    /// <summary>
    /// 根据世界坐标获取网格对象
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }
}
