using System;
using System.Collections.Generic;

public interface IDataProvider : ICloneable
{
    bool ContainsRightBorder(int position);
    bool ContainsBottomBorder(int position);

    void InsertBottomBorder(int position, bool state = true);
    void InsertRightBorder(int position, bool state = true);

    void SetGroup(int position, byte group);
    byte GetGroup(int position);

    int GetWidth();
    int GetHeight();
}

public static partial class DataProviderMethods
{
   
}