using System.Collections.Generic;

public interface IEullerAlgorithmDataProvider
{
    bool ContainsRightBorder(int x, int y);
    bool ContainsBottomBorder(int x, int y);

    void InsertDownBorder(int x, int y, bool state = true);
    void InsertRightBorder(int x, int y, bool state = true);

    void SetGroup(int x, int y, byte group);
    byte GetGroup(int x, int y);

    int GetWidth();
    int GetHeight();
}

public static partial class EullerAlgorithmDataProviderMethods
{
    public static byte GetNextRightBorder(this IEullerAlgorithmDataProvider dataProvider, int x, int y)
    {
        var width = dataProvider.GetWidth();
        byte temp;

        for (; x < width; x++)
        {
            temp = dataProvider.GetGroup(x, y);
            if (temp != 0) return temp;
        }

        return 0;
    }
    public static byte GetNextLeftBorder(this IEullerAlgorithmDataProvider dataProvider, int x, int y)
    {
        var width = dataProvider.GetWidth();
        byte temp;

        for (; x >= 0; x--)
        {
            temp = dataProvider.GetGroup(x, y);
            if (temp != 0) return temp;
        }

        return 0;
    }
    public static byte GetNextUpBorder(this IEullerAlgorithmDataProvider dataProvider, int x, int y)
    {
        var height = dataProvider.GetHeight();
        byte temp;

        for (; y >= 0; y--)
        {
            temp = dataProvider.GetGroup(x, y);
            if (temp != 0) return temp;
        }

        return 0;
    }
    public static byte GetNextDownBorder(this IEullerAlgorithmDataProvider dataProvider, int x, int y)
    {
        var height = dataProvider.GetHeight();
        byte temp;

        for (; y < height; y++)
        {
            temp = dataProvider.GetGroup(x, y);
            if (temp != 0) return temp;
        }

        return 0;
    }
    public static byte GetNextGroup(this IEullerAlgorithmDataProvider dataProvider, int x, int y)
    {
        // var left = dataProvider.GetNextLeftBorder(x, y);
        // var rigth = dataProvider.GetNextRightBorder(x, y);
        // var down = dataProvider.GetNextDownBorder(x, y);
        // var up = dataProvider.GetNextUpBorder(x, y);

        // return (byte)(up & down | left & rigth);
        return (byte)(x + 1);
    }

    // public static int CountGroupsInRow(this IEullerAlgorithmDataProvider dataProvider, int row, int startX)
    // {
    //     int group = dataProvider.GetGroup(startX, row);
    //     int count = 0;
    //     for (int i = startX; i < dataProvider.GetWidth() && dataProvider.GetGroup(i, row) == group; i++)
    //     {
    //         count++;
    //         if (dataProvider.ContainsRightBorder(i, row))
    //             break;
    //     }
    //     for (int i = startX - 1; i >= 0 && dataProvider.GetGroup(i, row) == group; i--)
    //     {
    //         if (dataProvider.ContainsRightBorder(i, row))
    //             break;
    //         count++;
    //     }
    //     return count;
    // }

    public static int[] GetIndexesGroupAtRow(this IEullerAlgorithmDataProvider dataProvider, int row, int startX)
    {

        int group = dataProvider.GetGroup(startX, row);
        var width = dataProvider.GetWidth();
        var result = new List<int>(width);

        for (int i = startX; i < width && dataProvider.GetGroup(i, row) == group; i++)
        {
            result.Add(i);
            if (dataProvider.ContainsRightBorder(i, row)) break;
        }
        for (int i = startX - 1; i >= 0 && dataProvider.GetGroup(i, row) == group; i--)
        {
            if (dataProvider.ContainsRightBorder(i, row)) break;
            result.Add(i);
        }

        return result.ToArray();
    }

    public static int CountGroupsInRowWithNoBorders(this IEullerAlgorithmDataProvider dataProvider, int row, int startX)
    {
        var group = GetIndexesGroupAtRow(dataProvider, row, startX);
        int count = 0;
        for (int i = 0; i < group.Length; i++)
            if (!dataProvider.ContainsBottomBorder(group[i], row))
                count++;
        return count;
    }

    public static void CopyRow(IEullerAlgorithmDataProvider dataProvider, int thisRow, int hereRow)
    {
        for (int i = 0; i < dataProvider.GetWidth(); i++)
        {
            dataProvider.SetGroup(i, hereRow, dataProvider.GetGroup(i, thisRow));
            dataProvider.InsertDownBorder(i, hereRow, dataProvider.ContainsBottomBorder(i, thisRow));
            dataProvider.InsertRightBorder(i, hereRow, dataProvider.ContainsRightBorder(i, thisRow));
        }
    }
    public static void CopyGroupRow(IEullerAlgorithmDataProvider dataProvider, int thisRow, int hereRow)
    {
        for (int i = 0; i < dataProvider.GetWidth(); i++)
            dataProvider.SetGroup(i, hereRow, dataProvider.GetGroup(i, thisRow));
    }
}