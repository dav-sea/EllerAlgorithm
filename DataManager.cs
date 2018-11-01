using System;
using System.Collections.Generic;
using System.Text;
using Data = IDataProvider;

namespace EllerAlgorithm
{
    public sealed class DataEllerAlgorithmLogic
    {
        private int mWidth, mHeight;
        private Data mData;

        public void FlowRow(int row)
        {
            var positionSourceRow = row * mHeight;//Начало исходной строки
            var positionTargetRow = (row + 1) * mHeight;//Начало целевой строки

            for (int i = 0; i < mWidth; i++)//Проходим по всей строке
                if (!mData.ContainsBottomBorder(positionSourceRow + i))//В случае если ячейка не имеет нижней границы
                    //То установим ячейки ниже тоже множество что и у текщей ячейки
                    mData.SetGroup(
                        positionTargetRow + i,
                        mData.GetGroup(positionSourceRow + i));
        }

        public void FillGroups(int beginPosition,int endPosition)
        {
            for (; beginPosition < endPosition; beginPosition++)
                if (mData.GetGroup(beginPosition) == 0)
                    mData.SetGroup(beginPosition, GetNextGroup(beginPosition));
        }

        public byte GetNextGroup(int position)
        {
            //Считаем ряд и сдигаем его на 1 вправо 
            //сдиг делаем, чтобы избежать путаницы с нулевым множеством
            return (byte)(position % mHeight + 1);
        }



        //public void CopyBottomBorders(int sourcePosition, int targetPosition, int count)
        //{
        //    UpdateSize();
        //    for (int i = 0; i < count; i++)
        //    {
        //        mData.InsertBottomBorder(
        //            targetPosition + i,
        //            mData.ContainsBottomBorder(sourcePosition + i));
        //    }
        //}
        //public void CopyGroups(int sourcePosition, int targetPosition, int count)
        //{
        //    UpdateSize();
        //    for (int i = 0; i < count; i++)
        //    {
        //        mData.SetGroup(
        //            targetPosition + i,
        //            mData.GetGroup(sourcePosition + i));
        //    }
        //}
        //public void CopyRightBorders(int sourcePosition, int targetPosition, int count)
        //{
        //    UpdateSize();
        //    for (int i = 0; i < count; i++)
        //    {
        //        mData.InsertRightBorder(
        //            targetPosition + i,
        //            mData.ContainsRightBorder(sourcePosition + i));
        //    }
        //}

        public Data GetDataProvider()
        {
            return mData;
        }

        public void SetDataProvider(Data data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            mData = data;
            UpdateSize();
        }

        void UpdateSize()
        {
            mWidth = mData.GetWidth();
            mHeight = mData.GetHeight();
        }

        //void CheckParameters(int sourceBeginIndex, int sourceEndIndex, int targetBeginIndex, int targetEndIndex)
        //{
        //    if (sourceBeginIndex < 0) throw new ArgumentException();
        //    if (sourceBeginIndex > sourceEndIndex) throw new ArgumentException();
        //    if (sourceEndIndex > targetBeginIndex) throw new ArgumentException();
        //    if (targetBeginIndex > targetEndIndex) throw new ArgumentException();
        //    if (targetEndIndex > mWidth) throw new ArgumentOutOfRangeException();
        //}
    }
}
