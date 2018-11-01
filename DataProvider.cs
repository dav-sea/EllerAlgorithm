using System;


public class DataProvider : IEullerAlgorithmDataProvider
{
    readonly CellData[,] CellsData;
    int Width;
    int Height;

    public DataProvider(int width, int height)
    {
        Width = width;
        Height = height;
        CellsData = new CellData[width, height];
    }


    public bool ContainsBottomBorder(int x, int y)
    {
        return CellsData[x, y].Borders.IsDownBorder();
    }

    public byte GetGroup(int x, int y)
    {
        return CellsData[x, y].Group;
    }

    public int GetHeight()
    {
        return Height;
    }

    public int GetWidth()
    {
        return Width;
    }

    public bool ContainsRightBorder(int x, int y)
    {
        return CellsData[x, y].Borders.IsRightBorder();
    }

    public void InsertDownBorder(int x, int y, bool state = true)
    {
        CellsData[x, y].InsertBottomBorder(state);
    }

    public void SetGroup(int x, int y, byte group)
    {
        CellsData[x, y].Group = group;
    }

    public void InsertRightBorder(int x, int y, bool state = true)
    {
        CellsData[x, y].InsertRightBorder(state);
    }
}

public struct CellData
{
    public BorderData Borders;
    public byte Group;

    public void InsertBottomBorder(bool state)
    {
        if (state)
            Borders |= BorderData.Bottom;
        else Borders &= ~BorderData.Bottom;
    }
    public void InsertRightBorder(bool state)
    {
        if (state)
            Borders |= BorderData.Right;
        else Borders &= ~BorderData.Right;
    }
}

[Flags]
public enum BorderData : byte
{
    NoBorders = 0,
    Right = 1,
    Bottom = 2,
}
public static class BorderDataExtenter
{
    public static bool IsRightBorder(this BorderData data)
    {
        return (data & BorderData.Right) != 0;
    }

    public static bool IsDownBorder(this BorderData data)
    {
        return (data & BorderData.Bottom) != 0;
    }

}