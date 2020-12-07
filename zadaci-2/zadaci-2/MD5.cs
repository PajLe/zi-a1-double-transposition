using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace zadaci_2
{
    public static class MD5
    {

        public static async Task<string> MD5Hash(string inputFilePath)
        {
            uint[] k = new uint[64];
            int[] r = new int[] { 7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22, 5, 9, 14, 20, 5, 9, 14, 20, 5, 9, 14, 20, 5, 9, 14, 20, 4, 11, 16, 23, 4, 11, 16, 23, 4, 11, 16, 23, 4, 11, 16, 23, 6, 10, 15, 21, 6, 10, 15, 21, 6, 10, 15, 21, 6, 10, 15, 21 };
            for (int i = 0; i < 64; i++)
                k[i] = (uint)Math.Floor(Math.Abs(Math.Sin(i + 1)) * Math.Pow(2, 32));

            uint h0 = 0x67452301;
            uint h1 = 0xEFCDAB89;
            uint h2 = 0x98BADCFE;
            uint h3 = 0x10325476;

            await foreach (byte[] tenMegabytes in FileSystemService.ReadFileTenMegabytesAtATime(inputFilePath))
            {
                BitArray tenMegaBytesInBits = new BitArray(tenMegabytes);
                if (tenMegaBytesInBits.Count % 512 != 0)
                    ExpandMessage(tenMegaBytesInBits);

                for (int i = 0; i < tenMegaBytesInBits.Count; i += 512) // for each 512 bit chunk
                {
                    uint[] w = new uint[16]
                    {
                        tenMegaBytesInBits.ToUInt(i),
                        tenMegaBytesInBits.ToUInt(i+32),
                        tenMegaBytesInBits.ToUInt(i+64),
                        tenMegaBytesInBits.ToUInt(i+96),
                        tenMegaBytesInBits.ToUInt(i+128),
                        tenMegaBytesInBits.ToUInt(i+160),
                        tenMegaBytesInBits.ToUInt(i+192),
                        tenMegaBytesInBits.ToUInt(i+224),
                        tenMegaBytesInBits.ToUInt(i+256),
                        tenMegaBytesInBits.ToUInt(i+288),
                        tenMegaBytesInBits.ToUInt(i+320),
                        tenMegaBytesInBits.ToUInt(i+352),
                        tenMegaBytesInBits.ToUInt(i+384),
                        tenMegaBytesInBits.ToUInt(i+416),
                        tenMegaBytesInBits.ToUInt(i+448),
                        tenMegaBytesInBits.ToUInt(i+480)
                    };
                    uint a = h0;
                    uint b = h1;
                    uint c = h2;
                    uint d = h3;

                    for (uint j = 0; j < 64; j++)
                    {
                        uint f, g;
                        if (0 <= j && j <= 15)
                        {
                            f = (b & c) | ((~b) & d);
                            g = j;
                        }
                        else if (16 <= j && j <= 31)
                        {
                            f = (d & b) | ((~d) & c);
                            g = (5 * j + 1) % 16;
                        }
                        else if (32 <= j && j <= 47)
                        {
                            f = b ^ c ^ d;
                            g = (3 * j + 5) % 16;
                        }
                        else //if (48 <= j && j <= 63)
                        {
                            f = c ^ (b | (~d));
                            g = (7 * j) % 16;
                        }

                        uint temp = d;
                        d = c;
                        c = b;
                        b += LeftRotate((a + f + k[j] + w[g]), r[j]);
                        a = temp;
                    }
                    h0 += a;
                    h1 += b;
                    h2 += c;
                    h3 += d;
                }
            }

            return JoinResultToString(h0, h1, h2, h3);
        }

        private static string JoinResultToString(uint h0, uint h1, uint h2, uint h3)
        {
            var h0Bytes = BitConverter.GetBytes(h0);
            var h1Bytes = BitConverter.GetBytes(h1);
            var h2Bytes = BitConverter.GetBytes(h2);
            var h3Bytes = BitConverter.GetBytes(h3);

            StringBuilder s = new StringBuilder();
            foreach (var b in h0Bytes)
                s.AppendFormat("{0:X2}", b);
            foreach (var b in h1Bytes)
                s.AppendFormat("{0:X2}", b);
            foreach (var b in h2Bytes)
                s.AppendFormat("{0:X2}", b);
            foreach (var b in h3Bytes)
                s.AppendFormat("{0:X2}", b);

            return s.ToString();
        }

        private static uint LeftRotate(uint x, int c)
        {
            return (x << c) | (x >> (32 - c));
        }

        private static void ExpandMessage(BitArray bitArray)
        {
            if (bitArray.Count % 512 != 0)
            {
                long initialLength = bitArray.Count;
                bitArray.AppendBit(true);
                while (bitArray.Count % 512 != 448)
                    bitArray.AppendBit(false);

                byte[] initialLengthBytes = BitConverter.GetBytes(initialLength);
                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(initialLengthBytes);

                for (int i = 0; i < 8; i++)
                    bitArray.AppendByte(initialLengthBytes[i]);
            }
        }
    }
}
