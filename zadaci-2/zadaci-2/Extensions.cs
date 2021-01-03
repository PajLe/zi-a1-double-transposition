using System;
using System.Collections;
using System.Collections.Concurrent;
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

        public static void AppendBit(this BitArray bitArray, bool bit)
        {
            bitArray.Length++;
            bitArray.Set(bitArray.Length - 1, bit);
        }

        public static void AppendByte(this BitArray bitArray, byte b)
        {
            BitArray byteBitArray = new BitArray(new byte[] { b });
            for (int i = 0; i < 8; i++)
                bitArray.AppendBit(byteBitArray[0]);
        }

        public static void AppendBytes(this BitArray bitArray, byte[] bytes)
        {
            foreach (var b in bytes)
                bitArray.AppendByte(b);
        }

        public static uint ToUInt(this BitArray bitArray, int startPos)
        {
            BitArray uintBitArray = new BitArray(32);
            for (int i = 0; i < 32; i++)
                uintBitArray[i] = bitArray[startPos++];

            uint[] arr = new uint[1];
            uintBitArray.CopyTo(arr, 0);
            return arr[0];
        }

        public static byte[] Xor(this byte[] bytes, byte[] with)
        {
            byte[] xoredBytes = new byte[bytes.Length];
            for (int i = 0; i < xoredBytes.Length; i++)
                xoredBytes[i] = (byte)(bytes[i] ^ with[i]);

            return xoredBytes;
        }

        public static T[] ToArray<T>(this ConcurrentDictionary<int, T> concurrentDictionary) where T : struct
        {
            T[] array = new T[concurrentDictionary.Count];
            for (int i = 0; i < concurrentDictionary.Keys.Count; i++)
                concurrentDictionary.TryGetValue(i, out array[i]);

            return array;
        }

        public static ConcurrentDictionary<int, T> ToConcurrentDictionary<T>(this T[] array) where T : struct
        {
            ConcurrentDictionary<int, T> concurrentDictionary = new ConcurrentDictionary<int, T>();
            for (int i = 0; i < array.Length; i++)
                concurrentDictionary.TryAdd(i, array[i]);

            return concurrentDictionary;
        }

        public static void ArrayCopyToConcurrentDictionary<T>(T[] sourceArray, int sourceIndex, 
            ConcurrentDictionary<int, T> destinationConcurrentDictionary, int destinationIndex, int length) where T : struct
        {
            for (int i = 0; i < length; i++)
                destinationConcurrentDictionary.TryAdd(destinationIndex++, sourceArray[sourceIndex++]);
        }
    }
}
