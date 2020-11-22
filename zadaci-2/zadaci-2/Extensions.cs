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
    }
}
