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

            // .txt file
            var txtEncryptBytes = DoubleTranspositionCrypto.Encrypt(
                FileSystemService.ReadAllBytes(Constants.TestFilesPath + "A1B1PlainText3.txt"),
                new int[] { 0, 1, 5, 4, 6, 3, 2 },
                new int[] { 1, 4, 2, 7, 3, 8, 0, 5, 6 });
            FileSystemService.WriteAllBytes(Constants.Results_A1_2_FilesPath + "A1B1PlainText3_encrypted.txt", txtEncryptBytes);

            var txtDecryptBytes = DoubleTranspositionCrypto.Decrypt(
                FileSystemService.ReadAllBytes(Constants.Results_A1_2_FilesPath + "A1B1PlainText3_encrypted.txt"),
                new int[] { 0, 1, 5, 4, 6, 3, 2 },
                new int[] { 1, 4, 2, 7, 3, 8, 0, 5, 6 });
            FileSystemService.WriteAllBytes(Constants.Results_A1_2_FilesPath + "A1B1PlainText3_decrypted.txt", txtDecryptBytes);

            //var allBytes = FileSystemService.ReadAllBytes(Constants.TestFilesPath + "A1B1PlainText2.docx");
            //byte p = allBytes[0];
            //allBytes[0] = allBytes[1];
            //allBytes[1] = p;
            //FileSystemService.WriteAllBytes("encrypted.docx", allBytes);

            //var encryptedBytes = FileSystemService.ReadAllBytes("encrypted.docx");
            //p = encryptedBytes[0];
            //encryptedBytes[0] = encryptedBytes[1];
            //encryptedBytes[1] = p;
            //FileSystemService.WriteAllBytes("decrypted.docx", encryptedBytes);

            // .docx file
            var bytesEncr = DoubleTranspositionCrypto.Encrypt(
                FileSystemService.ReadAllBytes(Constants.TestFilesPath + "A1B1PlainText2.docx"),
                new int[] { 0, 1, 5, 4, 6, 3, 2 },
                new int[] { 1, 4, 2, 7, 3, 8, 0, 5, 6 });
            FileSystemService.WriteAllBytes(Constants.Results_A1_2_FilesPath + "A1B1PlainText2_encrypted.docx", bytesEncr);

            var bytesDecrypt = DoubleTranspositionCrypto.Decrypt(
                FileSystemService.ReadAllBytes(Constants.Results_A1_2_FilesPath + "A1B1PlainText2_encrypted.docx"),
                new int[] { 0, 1, 5, 4, 6, 3, 2 },
                new int[] { 1, 4, 2, 7, 3, 8, 0, 5, 6 });
            FileSystemService.WriteAllBytes(Constants.Results_A1_2_FilesPath + "A1B1PlainText2_decrypted.docx", bytesDecrypt);

            // .ppt file
            var bytesEncrPpt = DoubleTranspositionCrypto.Encrypt(
                FileSystemService.ReadAllBytes(Constants.TestFilesPath + "A1B1PlainText1.ppt"),
                new int[] { 0, 1, 5, 4, 6, 3, 2 },
                new int[] { 1, 4, 2, 7, 3, 8, 0, 5, 6 });
            FileSystemService.WriteAllBytes(Constants.Results_A1_2_FilesPath + "A1B1PlainText1_encrypted.ppt", bytesEncrPpt);

            var bytesDecryptPpt = DoubleTranspositionCrypto.Decrypt(
                FileSystemService.ReadAllBytes(Constants.Results_A1_2_FilesPath + "A1B1PlainText1_encrypted.ppt"),
                new int[] { 0, 1, 5, 4, 6, 3, 2 },
                new int[] { 1, 4, 2, 7, 3, 8, 0, 5, 6 });
            FileSystemService.WriteAllBytes(Constants.Results_A1_2_FilesPath + "A1B1PlainText1_decrypted.ppt", bytesDecryptPpt);
        }
    }
}
