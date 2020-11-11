using System;
using System.IO;

namespace zadaci_2
{
    class Program
    {
        static void Main(string[] args)
        {
            // dev tests
            //DoubleTranspositionCrypto.Encrypt(
            //    FileSystemService.ReadAllTextUtf8(Constants.A1_2_FilesPath + "testRows.txt"),
            //    new int[] { 0, 1, 5, 4, 6, 3, 2 }, 
            //    new int[] { 1, 4, 2, 7, 3, 8, 0, 5, 6 },
            //    "outputRowsAndColsTest.txt");

            //DoubleTranspositionCrypto.Decrypt(
            //    FileSystemService.ReadAllTextUtf8(Constants.A1_2_FilesPath + "encoded.txt"),
            //    new int[] { 0, 1, 5, 4, 6, 3, 2 },
            //    new int[] { 1, 4, 2, 7, 3, 8, 0, 5, 6 },
            //    "outputDecodedRowsAndColsTest.txt");

            // .txt file
            DoubleTranspositionCrypto.Encrypt(
                FileSystemService.ReadAllTextUtf8(Constants.TestFilesPath + "A1B1PlainText3.txt"),
                new int[] { 0, 1, 5, 4, 6, 3, 2 },
                new int[] { 1, 4, 2, 7, 3, 8, 0, 5, 6 },
                Constants.Results_A1_2_FilesPath + "A1B1PlainText3_encoded.txt");

            DoubleTranspositionCrypto.Decrypt(
                FileSystemService.ReadAllTextUtf8(Constants.Results_A1_2_FilesPath + "A1B1PlainText3_encoded.txt"),
                new int[] { 0, 1, 5, 4, 6, 3, 2 },
                new int[] { 1, 4, 2, 7, 3, 8, 0, 5, 6 },
                Constants.Results_A1_2_FilesPath + "A1B1PlainText3_decoded.txt");

            //FileSystemService.WriteAllText("asd.docx", FileSystemService.ReadAllTextDefault(Constants.TestFilesPath + "A1B1PlainText2.docx"));

            // .docx file
            //DoubleTranspositionCrypto.Encrypt(
            //    FileSystemService.ReadAllTextUtf8(Constants.TestFilesPath + "A1B1PlainText2.docx"),
            //    new int[] { 0, 1, 5, 4, 6, 3, 2 },
            //    new int[] { 1, 4, 2, 7, 3, 8, 0, 5, 6 },
            //    Constants.Results_A1_2_FilesPath + "A1B1PlainText2_encoded.docx");

            //DoubleTranspositionCrypto.Decrypt(
            //    FileSystemService.ReadAllTextUtf8(Constants.Results_A1_2_FilesPath + "A1B1PlainText2_encoded.docx"),
            //    new int[] { 0, 1, 5, 4, 6, 3, 2 },
            //    new int[] { 1, 4, 2, 7, 3, 8, 0, 5, 6 },
            //    Constants.Results_A1_2_FilesPath + "A1B1PlainText2_decoded.docx");

            // .ppt file
            //DoubleTranspositionCrypto.Encrypt(
            //    FileSystemService.ReadAllTextUtf8(Constants.TestFilesPath + "A1B1PlainText3.txt"),
            //    new int[] { 0, 1, 5, 4, 6, 3, 2 },
            //    new int[] { 1, 4, 2, 7, 3, 8, 0, 5, 6 },
            //    Constants.Results_A1_2_FilesPath + "A1B1PlainText3_encoded.txt");

            //DoubleTranspositionCrypto.Decrypt(
            //    FileSystemService.ReadAllTextUtf8(Constants.Results_A1_2_FilesPath + "A1B1PlainText3_encoded.txt"),
            //    new int[] { 0, 1, 5, 4, 6, 3, 2 },
            //    new int[] { 1, 4, 2, 7, 3, 8, 0, 5, 6 },
            //    Constants.Results_A1_2_FilesPath + "A1B1PlainText3_decoded.txt");
        }
    }
}
