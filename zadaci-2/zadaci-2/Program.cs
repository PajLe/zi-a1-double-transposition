using System;
using System.IO;
using System.Text;

namespace zadaci_2
{
    class Program
    {
        static void Main(string[] args)
        {
            // dev tests
            //var bytesEncr = DoubleTranspositionCrypto.Encrypt(
            //    FileSystemService.ReadAllBytes(Constants.A1_2_FilesPath + "testColumns.txt"),
            //    new int[] { 0, 1, 5, 4, 6, 3, 2 },
            //    new int[] { 1, 4, 2, 7, 3, 8, 0, 5, 6 });
            //FileSystemService.WriteAllBytes(Constants.A1_2_FilesPath + "testRows_encrypted.txt", bytesEncr);

            //var bytesDecrypt = DoubleTranspositionCrypto.Decrypt(
            //    FileSystemService.ReadAllBytes(Constants.A1_2_FilesPath + "testRows_encrypted.txt"),
            //    new int[] { 0, 1, 5, 4, 6, 3, 2 },
            //    new int[] { 1, 4, 2, 7, 3, 8, 0, 5, 6 });
            //FileSystemService.WriteAllBytes(Constants.A1_2_FilesPath + "testRows_decrypted.txt", bytesDecrypt);

            //byte[] bmp1 = FileSystemService.ReadAllBytes(Constants.TestFilesPath + "B2D.bmp");
            //int pos1 = bmp1[10] + 256 * (bmp1[11] + 256 * (bmp1[12] + 256 * bmp1[13]));

            //FileStream fsr = System.IO.File.OpenRead(Constants.TestFilesPath + "B2D.bmp");
            //byte[] a = new byte[fsr.Length];
            //fsr.Read(a, 0, Convert.ToInt32(fsr.Length));
            //int pos = a[10] + 256 * (a[11] + 256 * (a[12] + 256 * a[13]));

            string crypted = PlayfairCrypto.Encrypt("iNst[]rume]nts", "monarchy");
            Console.WriteLine(crypted);
        }
    }
}
