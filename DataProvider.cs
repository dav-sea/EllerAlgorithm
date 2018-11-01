using System;


public class DataProvider : IDataProvider
{
    readonly CellData[] CellsData;
    int Width;
    int Height;

    public bool ContainsBottomBorder(int position)
    {
        return (CellsData[position].Borders & BorderData.Bottom) != 0;
    }
    public bool ContainsRightBorder(int position)
    {
        return (CellsData[position].Borders & BorderData.Right) != 0;
    }

    public byte GetGroup(int position)
    {
        return CellsData[position].Group;
    }
    public void SetGroup(int position, byte group)
    {
        CellsData[position].Group = group;
    }

    public int GetHeight()
    {
        return Height;
    }
    public int GetWidth()
    {
        return Width;
    }

    public void InsertBottomBorder(int position, bool state = true)
    {
        CellsData[position].InsertBottomBorder(state);
    }
    public void InsertRightBorder(int position, bool state = true)
    {
        CellsData[position].InsertRightBorder(state);
    }

    object ICloneable.Clone()
    {
        return Clone();
    }
    public DataProvider Clone()
    {
        var clone = new DataProvider(Width, Height);
        CellsData.CopyTo(CellsData, 0);
        return clone;
    }

    private DataProvider() : this(1) { }
    public DataProvider(int side) : this(side, side) {  }
    public DataProvider(int width,int height)
    {
        Width = width;
        Height = height;
        CellsData = new CellData[width * height];
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

    public static bool IsBottomBorder(this BorderData data)
    {
        return (data & BorderData.Bottom) != 0;
    }

}