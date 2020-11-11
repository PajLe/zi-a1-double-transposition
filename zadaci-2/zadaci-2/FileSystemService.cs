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
            File.WriteAllText(path, contents, Encoding.Default);
        }
    }
}
