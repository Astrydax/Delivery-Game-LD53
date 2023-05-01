using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using Unity.VisualScripting;

public class Grid 
{
    private int  width;
    private int height;
    private float cellSize;
    private int[,] gridArray;
    private TextMesh[,] debugTextArray;

    public Grid(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for(int y = 0; y < gridArray.GetLength(1); y++)
            {
                debugTextArray[x,y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWolrdPosition(x, y) + new Vector3 (cellSize,cellSize) * 0.5f, 20, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWolrdPosition(x, y), GetWolrdPosition(x, y +1), Color.white, 100f);
                Debug.DrawLine(GetWolrdPosition(x, y), GetWolrdPosition(x + 1, y ), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWolrdPosition(0, height), GetWolrdPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWolrdPosition(width, 0), GetWolrdPosition(width, height), Color.white, 100f);

        SetValue(2, 1, 52);
    }

    private Vector3 GetWolrdPosition(int x, int y)
    {
        return new Vector3(x,y) * cellSize;
    }

    public void SetValue(int x, int y, int value)
    {
        if(x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            debugTextArray[x,y].text = gridArray[x,y].ToString();
        }        
    }
    

    
}
