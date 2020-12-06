using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace zadaci_2
{
    public static class Extensions
    {
        public static bool IsInArrayRange(this int a, int arrayLength)
        {
            if (a < 0 || a >= arrayLength)
                return false;

            return true;
        }

        public static int Mod(this int k, int n) { return ((k %= n) < 0) ? k + n : k; }

        public static void ShiftLeft<T>(this T[] array, int positions)
        {
            for (int j = 0; j < positions; j++)
            {
                T firstElement = array[0];
                for (int i = 0; i < array.Length - 1; i++)
                    array[i] = array[i + 1];
                array[array.Length - 1] = firstElement;
            }
        }

        public static void ShiftRight<T>(this T[] array, int positions)
        {
            for (int j = 0; j < positions; j++)
            {
                T lastElement = array[array.Length - 1];
                for (int i = array.Length - 1; i > 0; i--)
                    array[i] = array[i - 1];
                array[0] = lastElement;
            }
        }
    }
}
