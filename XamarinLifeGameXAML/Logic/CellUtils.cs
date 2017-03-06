using System;

namespace XamarinLifeGameXAML.Logic
{
    public class CellUtils
    {
        public static int GetIndex(Tuple<int, int> point)
        {
            return GetIndex(point.Item1, point.Item2);
        }

        public static int GetIndex(int x, int y)
        {
            return (y + x * CellSize);
        }

        public static Tuple<int, int> GetPoint(int index)
        {
            var y = index % CellSize;
            var x = (index - y) / CellSize;

            return Tuple.Create(x, y);
        }

        public static int CellSize => 7;

        public static int ArraySize => CellSize * CellSize;
    }
}