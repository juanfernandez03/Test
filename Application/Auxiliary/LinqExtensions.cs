using System.Collections.Generic;

namespace System.Linq
{
    public static class LinqExtensions
    {

        private static readonly Random _generator = new Random();

        public static T RandomItem<T>(this T[] array)
        {
            return array[_generator.Next(array.Length)];
        }
        public static IEnumerable<T> GetColumn<T>(this T[,] array, int column)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                yield return array[i, column];
            }
        }
        public static IEnumerable<IEnumerable<T>> GetColumns<T>(this T[,] array)
        {
            for (int i = 0; i < array.GetLength(1); i++)
            {
                yield return array.GetColumn(i);
            }
        }
    }
}

