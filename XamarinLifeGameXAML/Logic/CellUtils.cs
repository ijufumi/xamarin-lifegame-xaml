using System;

namespace XamarinLifeGameXAML.Logic
{
    public class CellUtils
    {
        public const int CellSize = 9;

        public static int GetIndex(Tuple<int, int> point)
        {
            return (point.Item2 + point.Item1 * CellSize);
        }

        public static Tuple<int, int> GetPoint(int index)
        {
            var y = index % CellSize;
            var x = (index - y) / CellSize;

            return Tuple.Create(x, y);
        }
    }
}