using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace zadaci_2
{
    public static class AES
    {
        private static readonly byte[] SBox =
        {
            //0     1     2     3     4     5     6     7     8     9     A     B     C     D     E     F
            0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab, 0x76, //0
            0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0, //1
            0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15, //2
            0x04, 0xc7, 0x23, 0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75, //3
            0x09, 0x83, 0x2c, 0x1a, 0x1b, 0x6e, 0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84, //4
            0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1, 0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf, //5
            0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45, 0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f, 0xa8, //6
            0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3, 0xd2, //7
            0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64, 0x5d, 0x19, 0x73, //8
            0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b, 0xdb, //9
            0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79, //A
            0xe7, 0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 0x08, //B
            0xba, 0x78, 0x25, 0x2e, 0x1c, 0xa6, 0xb4, 0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a, //C
            0x70, 0x3e, 0xb5, 0x66, 0x48, 0x03, 0xf6, 0x0e, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e, //D
            0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e, 0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf, //E
            0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68, 0x41, 0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16  //F
        };


        private static readonly byte[] SBoxInvert =
        {
            0x52, 0x09, 0x6a, 0xd5, 0x30, 0x36, 0xa5, 0x38, 0xbf, 0x40, 0xa3, 0x9e, 0x81, 0xf3, 0xd7, 0xfb,
            0x7c, 0xe3, 0x39, 0x82, 0x9b, 0x2f, 0xff, 0x87, 0x34, 0x8e, 0x43, 0x44, 0xc4, 0xde, 0xe9, 0xcb,
            0x54, 0x7b, 0x94, 0x32, 0xa6, 0xc2, 0x23, 0x3d, 0xee, 0x4c, 0x95, 0x0b, 0x42, 0xfa, 0xc3, 0x4e,
            0x08, 0x2e, 0xa1, 0x66, 0x28, 0xd9, 0x24, 0xb2, 0x76, 0x5b, 0xa2, 0x49, 0x6d, 0x8b, 0xd1, 0x25,
            0x72, 0xf8, 0xf6, 0x64, 0x86, 0x68, 0x98, 0x16, 0xd4, 0xa4, 0x5c, 0xcc, 0x5d, 0x65, 0xb6, 0x92,
            0x6c, 0x70, 0x48, 0x50, 0xfd, 0xed, 0xb9, 0xda, 0x5e, 0x15, 0x46, 0x57, 0xa7, 0x8d, 0x9d, 0x84,
            0x90, 0xd8, 0xab, 0x00, 0x8c, 0xbc, 0xd3, 0x0a, 0xf7, 0xe4, 0x58, 0x05, 0xb8, 0xb3, 0x45, 0x06,
            0xd0, 0x2c, 0x1e, 0x8f, 0xca, 0x3f, 0x0f, 0x02, 0xc1, 0xaf, 0xbd, 0x03, 0x01, 0x13, 0x8a, 0x6b,
            0x3a, 0x91, 0x11, 0x41, 0x4f, 0x67, 0xdc, 0xea, 0x97, 0xf2, 0xcf, 0xce, 0xf0, 0xb4, 0xe6, 0x73,
            0x96, 0xac, 0x74, 0x22, 0xe7, 0xad, 0x35, 0x85, 0xe2, 0xf9, 0x37, 0xe8, 0x1c, 0x75, 0xdf, 0x6e,
            0x47, 0xf1, 0x1a, 0x71, 0x1d, 0x29, 0xc5, 0x89, 0x6f, 0xb7, 0x62, 0x0e, 0xaa, 0x18, 0xbe, 0x1b,
            0xfc, 0x56, 0x3e, 0x4b, 0xc6, 0xd2, 0x79, 0x20, 0x9a, 0xdb, 0xc0, 0xfe, 0x78, 0xcd, 0x5a, 0xf4,
            0x1f, 0xdd, 0xa8, 0x33, 0x88, 0x07, 0xc7, 0x31, 0xb1, 0x12, 0x10, 0x59, 0x27, 0x80, 0xec, 0x5f,
            0x60, 0x51, 0x7f, 0xa9, 0x19, 0xb5, 0x4a, 0x0d, 0x2d, 0xe5, 0x7a, 0x9f, 0x93, 0xc9, 0x9c, 0xef,
            0xa0, 0xe0, 0x3b, 0x4d, 0xae, 0x2a, 0xf5, 0xb0, 0xc8, 0xeb, 0xbb, 0x3c, 0x83, 0x53, 0x99, 0x61,
            0x17, 0x2b, 0x04, 0x7e, 0xba, 0x77, 0xd6, 0x26, 0xe1, 0x69, 0x14, 0x63, 0x55, 0x21, 0x0c, 0x7d
        };

        private static Dictionary<byte, byte> gfmultBy02 = new Dictionary<byte, byte>();
        private static Dictionary<byte, byte> gfmultBy03 = new Dictionary<byte, byte>();
        private static Dictionary<byte, byte> gfmultBy09 = new Dictionary<byte, byte>();
        private static Dictionary<byte, byte> gfmultBy0b = new Dictionary<byte, byte>();
        private static Dictionary<byte, byte> gfmultBy0d = new Dictionary<byte, byte>();
        private static Dictionary<byte, byte> gfmultBy0e = new Dictionary<byte, byte>();

        public static async Task AESCrypt(string inputFilePath, byte[] key, string outputFilePath)
        {
            if (key.Length != 16)
                throw new ArgumentException("Key has to be 16 bytes", nameof(key));

            byte[] keyCopy = new byte[key.Length];
            key.CopyTo(keyCopy, 0);
            TimeSpan totalElapsed = new TimeSpan();
            TimeSpan subBytes = new TimeSpan(),
                createInputMatrix = new TimeSpan(), 
                shiftRows = new TimeSpan(),
                mixColumns = new TimeSpan(),
                addRoundKey = new TimeSpan(),
                cryptoMatrixToArray = new TimeSpan();
            IList<Task> writeTasks = new List<Task>();
            using (FileStream fw = new FileStream(outputFilePath, FileMode.OpenOrCreate))
            {
                int indexOfReadBytes = 0;
                await foreach (var byteArray10MB in FileSystemService.ReadFileTenMegabytesAtTheTime(inputFilePath))
                {
                    Stopwatch s = new Stopwatch();
                    Stopwatch privateMethodsStopwatch = new Stopwatch();
                    s.Start();
                    int remainderDividingBy16 = byteArray10MB.Length % 16;
                    byte[] bytesToWrite10MB = new byte[byteArray10MB.Length];
                    for (int i = 0; i < byteArray10MB.Length - remainderDividingBy16; i += 16)
                    {
                        privateMethodsStopwatch.Start();
                        byte[][] inputMatrix = CreateInputMatrix4By4(byteArray10MB, i);
                        createInputMatrix = createInputMatrix.Add(privateMethodsStopwatch.Elapsed);

                        privateMethodsStopwatch.Restart();
                        AddRoundKey(inputMatrix, keyCopy);
                        addRoundKey = addRoundKey.Add(privateMethodsStopwatch.Elapsed);

                        for (int round = 1; round <= 13; round++)
                        {
                            privateMethodsStopwatch.Restart();
                            SubBytes(inputMatrix);
                            subBytes = subBytes.Add(privateMethodsStopwatch.Elapsed);

                            privateMethodsStopwatch.Restart();
                            ShiftRows(inputMatrix);
                            shiftRows = shiftRows.Add(privateMethodsStopwatch.Elapsed);

                            privateMethodsStopwatch.Restart();
                            MixColumns(inputMatrix);
                            mixColumns = mixColumns.Add(privateMethodsStopwatch.Elapsed);

                            privateMethodsStopwatch.Restart();
                            AddRoundKey(inputMatrix, keyCopy);
                            addRoundKey = addRoundKey.Add(privateMethodsStopwatch.Elapsed);
                        }

                        privateMethodsStopwatch.Restart();
                        SubBytes(inputMatrix);
                        subBytes = subBytes.Add(privateMethodsStopwatch.Elapsed);

                        privateMethodsStopwatch.Restart();
                        ShiftRows(inputMatrix);
                        shiftRows = shiftRows.Add(privateMethodsStopwatch.Elapsed);

                        privateMethodsStopwatch.Restart();
                        AddRoundKey(inputMatrix, keyCopy);
                        addRoundKey = addRoundKey.Add(privateMethodsStopwatch.Elapsed);

                        privateMethodsStopwatch.Restart();
                        Array.Copy(CryptoMatrixToArray(inputMatrix), 0, bytesToWrite10MB, i, 16);
                        cryptoMatrixToArray = cryptoMatrixToArray.Add(privateMethodsStopwatch.Elapsed);
                    }
                    while (remainderDividingBy16 > 0)
                    {
                        bytesToWrite10MB[byteArray10MB.Length - remainderDividingBy16] = byteArray10MB[byteArray10MB.Length - remainderDividingBy16];
                        remainderDividingBy16--;
                    }
                    s.Stop();
                    Console.WriteLine(" - encrypt processed 10MB - " + Path.GetFileName(outputFilePath) + " - " + bytesToWrite10MB.Length + " - elapsed: " + s.Elapsed + " - " + indexOfReadBytes);
                    totalElapsed = totalElapsed.Add(s.Elapsed);
                    writeTasks.Add(WriteCryptoBytes(fw, bytesToWrite10MB));
                    indexOfReadBytes++;
                }
                await Task.WhenAll(writeTasks);
            }
            Console.WriteLine("--------------------------total elapsed: " + totalElapsed);
            Console.WriteLine("\t\t sub bytes: " + subBytes);
            Console.WriteLine("\t\t createInputMatrix: " + createInputMatrix);
            Console.WriteLine("\t\t shiftRows: " + shiftRows);
            Console.WriteLine("\t\t mixColumns: " + mixColumns);
            Console.WriteLine("\t\t addRoundKey: " + addRoundKey);
            Console.WriteLine("\t\t cryptoMatrixToArray: " + cryptoMatrixToArray);
        }

        private static void MixColumns(byte[][] inputMatrix)
        {
            for (int j = 0; j < 4; j++)
            {
                byte a0 = inputMatrix[0][j];
                byte a1 = inputMatrix[1][j];
                byte a2 = inputMatrix[2][j];
                byte a3 = inputMatrix[3][j];

                byte r0 = (byte)(gfmultby02(a0) ^ a3 ^ a2 ^ gfmultby03(a1));
                byte r1 = (byte)(gfmultby02(a1) ^ a0 ^ a3 ^ gfmultby03(a2));
                byte r2 = (byte)(gfmultby02(a2) ^ a1 ^ a0 ^ gfmultby03(a3));
                byte r3 = (byte)(gfmultby02(a3) ^ a2 ^ a1 ^ gfmultby03(a0));

                inputMatrix[0][j] = r0;
                inputMatrix[1][j] = r1;
                inputMatrix[2][j] = r2;
                inputMatrix[3][j] = r3;
            }
        }

        private static void ShiftRows(byte[][] inputMatrix)
        {
            for (int i = 1; i < 4; i++)
                inputMatrix[i].ShiftLeft(i);
        }

        private static void SubBytes(byte[][] inputMatrix)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    inputMatrix[i][j] = SBox[inputMatrix[i][j]];
        }

        private static void AddRoundKey(byte[][] inputMatrix, byte[] keyCopy)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    inputMatrix[i][j] = (byte)(inputMatrix[i][j] ^ keyCopy[i * 4 + j]);

            keyCopy.ShiftLeft(1); // keyschedule
        }

        private static byte[] CryptoMatrixToArray(byte[][] cryptoMatrix4x4)
        {
            byte[] array = new byte[16];
            int arrayIndex = 0;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    array[arrayIndex++] = cryptoMatrix4x4[i][j];

            return array;
        }

        private static async Task WriteCryptoBytes(FileStream fw, byte[] cryptoBytes)
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            await fw.WriteAsync(cryptoBytes, 0, cryptoBytes.Length);
            s.Stop();
            Console.WriteLine("\twrite elapsed: " + s.Elapsed);
        }

        private static byte[][] CreateInputMatrix4By4(byte[] sourceBytes, int startPos)
        {
            byte[][] matrix4x4 = new byte[4][];
            for (int i = 0; i < 4; i++)
            {
                matrix4x4[i] = new byte[4];

                for (int j = 0; j < 4; j++)
                    matrix4x4[i][j] = sourceBytes[startPos++];
            }

            return matrix4x4;
        }

        private static byte gfmultby01(byte b)
        {
            return b;
        }

        private static byte gfmultby02(byte b)
        {
            if (gfmultBy02.TryGetValue(b, out byte toRet))
            {
                return toRet;
            }
            else
            {
                if (b < 0x80)
                    toRet = (byte)(int)(b << 1);
                else
                    toRet = (byte)((int)(b << 1) ^ (int)(0x1b));

                gfmultBy02.Add(b, toRet);

                return toRet;
            }
        }

        private static byte gfmultby03(byte b)
        {
            if (gfmultBy03.TryGetValue(b, out byte toRet))
            {
                return toRet;
            }
            else
            {
                toRet = (byte)((int)gfmultby02(b) ^ (int)b);

                gfmultBy03.Add(b, toRet);

                return toRet;
            }
        }

        private static byte gfmultby09(byte b)
        {
            if (gfmultBy09.TryGetValue(b, out byte toRet))
            {
                return toRet;
            }
            else
            {
                toRet = (byte)((int)gfmultby02(gfmultby02(gfmultby02(b))) ^ (int)b);

                gfmultBy09.Add(b, toRet);

                return toRet;
            }
        }

        private static byte gfmultby0b(byte b)
        {
            if (gfmultBy0b.TryGetValue(b, out byte toRet))
            {
                return toRet;
            } 
            else
            {
                toRet = (byte)((int)gfmultby02(gfmultby02(gfmultby02(b))) ^ (int)gfmultby02(b) ^ (int)b);

                gfmultBy0b.Add(b, toRet);

                return toRet;
            }
        }

        private static byte gfmultby0d(byte b)
        {
            if (gfmultBy0d.TryGetValue(b, out byte toRet))
            {
                return toRet;
            }
            else
            {
                toRet = (byte)((int)gfmultby02(gfmultby02(gfmultby02(b))) ^ (int)gfmultby02(gfmultby02(b)) ^ (int)(b));

                gfmultBy0d.Add(b, toRet);

                return toRet;
            }
        }

        private static byte gfmultby0e(byte b)
        {
            if (gfmultBy0e.TryGetValue(b, out byte toRet))
            {
                return toRet;
            }
            else
            {
                toRet = (byte)((int)gfmultby02(gfmultby02(gfmultby02(b))) ^ (int)gfmultby02(gfmultby02(b)) ^ (int)gfmultby02(b));

                gfmultBy0e.Add(b, toRet);

                return toRet;
            }
        }

        public static async Task AESDecrypt(string inputFilePath, byte[] key, string outputFilePath)
        {
            if (key.Length != 16)
                throw new ArgumentException("Key has to be 16 bytes", nameof(key));

            byte[] keyCopy = new byte[key.Length];
            key.CopyTo(keyCopy, 0);
            IList<Task> writeTasks = new List<Task>();
            TimeSpan totalElapsed = new TimeSpan();
            TimeSpan subBytes = new TimeSpan(),
                createInputMatrix = new TimeSpan(),
                shiftRows = new TimeSpan(),
                mixColumns = new TimeSpan(),
                addRoundKey = new TimeSpan(),
                cryptoMatrixToArray = new TimeSpan();
            using (FileStream fw = new FileStream(outputFilePath, FileMode.OpenOrCreate))
            {
                Stopwatch s = new Stopwatch();
                s.Start();
                int indexOfReadBytes = 0;
                await foreach (var byteArray10MB in FileSystemService.ReadFileTenMegabytesAtTheTime(inputFilePath))
                {
                    Stopwatch privateMethodsStopwatch = new Stopwatch();

                    int remainderDividingBy16 = byteArray10MB.Length % 16;
                    byte[] bytesToWrite10MB = new byte[byteArray10MB.Length];
                    for (int i = 0; i < byteArray10MB.Length - remainderDividingBy16; i += 16)
                    {
                        privateMethodsStopwatch.Start();
                        byte[][] inputMatrix = CreateInputMatrix4By4(byteArray10MB, i);
                        createInputMatrix = createInputMatrix.Add(privateMethodsStopwatch.Elapsed);

                        privateMethodsStopwatch.Restart();
                        keyCopy.ShiftRight(2);
                        AddRoundKeyInverse(inputMatrix, keyCopy);
                        addRoundKey = addRoundKey.Add(privateMethodsStopwatch.Elapsed);

                        for (int round = 1; round <= 13; round++)
                        {
                            privateMethodsStopwatch.Restart();
                            ShiftRowsInverse(inputMatrix);
                            shiftRows = shiftRows.Add(privateMethodsStopwatch.Elapsed);

                            privateMethodsStopwatch.Restart();
                            SubBytesInverse(inputMatrix);
                            subBytes = subBytes.Add(privateMethodsStopwatch.Elapsed);

                            privateMethodsStopwatch.Restart();
                            AddRoundKeyInverse(inputMatrix, keyCopy);
                            addRoundKey = addRoundKey.Add(privateMethodsStopwatch.Elapsed);

                            privateMethodsStopwatch.Restart();
                            MixColumnsInverse(inputMatrix);
                            mixColumns = mixColumns.Add(privateMethodsStopwatch.Elapsed);
                        }

                        privateMethodsStopwatch.Restart();
                        ShiftRowsInverse(inputMatrix);
                        shiftRows = shiftRows.Add(privateMethodsStopwatch.Elapsed);

                        privateMethodsStopwatch.Restart();
                        SubBytesInverse(inputMatrix);
                        subBytes = subBytes.Add(privateMethodsStopwatch.Elapsed);

                        privateMethodsStopwatch.Restart();
                        AddRoundKeyInverse(inputMatrix, keyCopy);
                        addRoundKey = addRoundKey.Add(privateMethodsStopwatch.Elapsed);

                        privateMethodsStopwatch.Restart();
                        Array.Copy(CryptoMatrixToArray(inputMatrix), 0, bytesToWrite10MB, i, 16);
                        cryptoMatrixToArray = cryptoMatrixToArray.Add(privateMethodsStopwatch.Elapsed);
                    }
                    while (remainderDividingBy16 > 0)
                    {
                        bytesToWrite10MB[byteArray10MB.Length - remainderDividingBy16] = byteArray10MB[byteArray10MB.Length - remainderDividingBy16];
                        remainderDividingBy16--;
                    }
                    s.Stop();
                    Console.WriteLine(" - decrypt processed 10MB - " + Path.GetFileName(outputFilePath) + " - " + bytesToWrite10MB.Length + " - elapsed: " + s.Elapsed + " - " + indexOfReadBytes);
                    totalElapsed = totalElapsed.Add(s.Elapsed);
                    writeTasks.Add(WriteCryptoBytes(fw, bytesToWrite10MB));
                    indexOfReadBytes++;
                }
                await Task.WhenAll(writeTasks);
            }
            Console.WriteLine("--------------------------total elapsed: " + totalElapsed);
            Console.WriteLine("\t\t sub bytes: " + subBytes);
            Console.WriteLine("\t\t createInputMatrix: " + createInputMatrix);
            Console.WriteLine("\t\t shiftRows: " + shiftRows);
            Console.WriteLine("\t\t mixColumns: " + mixColumns);
            Console.WriteLine("\t\t addRoundKey: " + addRoundKey);
            Console.WriteLine("\t\t cryptoMatrixToArray: " + cryptoMatrixToArray);
        }

        private static void MixColumnsInverse(byte[][] inputMatrix)
        {
            for (int j = 0; j < 4; j++)
            {
                byte a0 = inputMatrix[0][j];
                byte a1 = inputMatrix[1][j];
                byte a2 = inputMatrix[2][j];
                byte a3 = inputMatrix[3][j];

                byte r0 = (byte)(gfmultby0e(a0) ^ gfmultby09(a3) ^ gfmultby0d(a2) ^ gfmultby0b(a1));
                byte r1 = (byte)(gfmultby0e(a1) ^ gfmultby09(a0) ^ gfmultby0d(a3) ^ gfmultby0b(a2));
                byte r2 = (byte)(gfmultby0e(a2) ^ gfmultby09(a1) ^ gfmultby0d(a0) ^ gfmultby0b(a3));
                byte r3 = (byte)(gfmultby0e(a3) ^ gfmultby09(a2) ^ gfmultby0d(a1) ^ gfmultby0b(a0));

                inputMatrix[0][j] = r0;
                inputMatrix[1][j] = r1;
                inputMatrix[2][j] = r2;
                inputMatrix[3][j] = r3;
            }
        }

        private static void SubBytesInverse(byte[][] inputMatrix)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    inputMatrix[i][j] = SBoxInvert[inputMatrix[i][j]];
        }

        private static void ShiftRowsInverse(byte[][] inputMatrix)
        {
            for (int i = 1; i < 4; i++)
                inputMatrix[i].ShiftRight(i);
        }

        private static void AddRoundKeyInverse(byte[][] inputMatrix, byte[] keyCopy)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    inputMatrix[i][j] = (byte)(inputMatrix[i][j] ^ keyCopy[i * 4 + j]);

            keyCopy.ShiftRight(1); // reverse keyschedule
        }
    }
}
