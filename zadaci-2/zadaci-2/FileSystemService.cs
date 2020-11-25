using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace zadaci_2
{
    public static class FileSystemService
    {

        public static string ReadAllTextUtf8(string path)
        {
            return File.ReadAllText(path, Encoding.UTF8);
        }

        public static string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public static void WriteAllText(string path, string contents)
        {
            File.WriteAllText(path, contents);
        }
        public static void WriteAllTextUtf8(string path, string contents)
        {
            File.WriteAllText(path, contents, Encoding.UTF8);
        }

        public static byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }

        public static byte[] ReadAllBytesUtf8(string path)
        {
            string text = File.ReadAllText(path);
            return Encoding.UTF8.GetBytes(text);
        }

        public static void WriteAllBytes(string path, byte[] bytes)
        {
            File.WriteAllBytes(path, bytes);
        }

        public static void WriteAllBytesUtf8(string path, byte[] bytes)
        {
            File.WriteAllText(path, Encoding.UTF8.GetString(bytes));
        }

        public static void WriteBmpBytes(string path, byte[] originalBmp, byte[] cryptodBmp)
        {
            using (FileStream b = File.OpenWrite(path))
            {
                int pos = originalBmp[10] + 256 * (originalBmp[11] + 256 * (originalBmp[12] + 256 * originalBmp[13]));
                for (int i = 0; i < originalBmp.Length; i++)
                {
                    if (i < pos)
                        b.WriteByte(originalBmp[i]);
                    else
                        b.WriteByte(cryptodBmp[i]);
                }
            }
        }
    }
}
