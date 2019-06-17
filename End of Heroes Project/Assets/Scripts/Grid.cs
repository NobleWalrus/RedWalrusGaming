using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{

    int rows;
    int columns;
    const float size = 2;
    public Grid()
    {
        rows = 10;
        columns = 2;
    }
    public int getRows()
    {
        return rows;
    }

    public void setRows(int R)
    {
        rows = R;
    }

    public int getColumns()
    {
        return columns;
    }

    public void setColumns(int C)
    {
        columns = C;
    }

    public float getSize()
    {
        return size;
    }

}