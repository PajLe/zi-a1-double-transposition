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
            await AES.AESCrypt(Constants.TestFilesPath + "B2A.bmp", key, Constants.Results_A3_2_FilesPath + "B2A_encr.bmp");
            await AES.AESDecrypt(Constants.Results_A3_2_FilesPath + "B2A_encr.bmp", key, Constants.Results_A3_2_FilesPath + "B2A_decrypted.bmp");
        }
    }
}
