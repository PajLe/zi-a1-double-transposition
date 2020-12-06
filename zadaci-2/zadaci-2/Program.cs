using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace zadaci_2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            byte[] key = new byte[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 11, 12, 13, 14, 15, 16 };
            AES.AESCrypt(Constants.TestFilesPath + "1GB.zip", key, Constants.Results_A3_2_FilesPath + "output.zip");
        }
    }
}
