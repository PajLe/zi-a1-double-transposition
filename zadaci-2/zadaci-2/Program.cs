using System;
using System.IO;
using System.Text;

namespace zadaci_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //string crypted = PlayfairCrypto.Encrypt("ins,truments instruments", "monarchy");
            //Console.WriteLine(crypted);
            //string decrypted = PlayfairCrypto.Decrypt(crypted, "monarchy");
            //Console.WriteLine(decrypted);

            string crypted = PlayfairCrypto.Encrypt(FileSystemService.ReadAllText(Constants.TestFilesPath + "bible.txt"), "monarchy");
            FileSystemService.WriteAllTextUtf8(Constants.Results_A2_2_FilesPath + "bible_encrypted.txt", crypted);
            string decrypted = PlayfairCrypto.Decrypt(FileSystemService.ReadAllText(Constants.Results_A2_2_FilesPath + "bible_encrypted.txt"), "monarchy");
            FileSystemService.WriteAllTextUtf8(Constants.Results_A2_2_FilesPath + "bible_decrypted.txt", decrypted);

            crypted = PlayfairCrypto.Encrypt(FileSystemService.ReadAllText(Constants.TestFilesPath + "E.coli"), "monarchy");
            FileSystemService.WriteAllTextUtf8(Constants.Results_A2_2_FilesPath + "E_encrypted.coli", crypted);
            decrypted = PlayfairCrypto.Decrypt(FileSystemService.ReadAllText(Constants.Results_A2_2_FilesPath + "E_encrypted.coli"), "monarchy");
            FileSystemService.WriteAllTextUtf8(Constants.Results_A2_2_FilesPath + "E_decrypted.coli", decrypted);

            crypted = PlayfairCrypto.Encrypt(FileSystemService.ReadAllText(Constants.TestFilesPath + "world192.txt"), "monarchy");
            FileSystemService.WriteAllTextUtf8(Constants.Results_A2_2_FilesPath + "world192_encrypted.txt", crypted);
            decrypted = PlayfairCrypto.Decrypt(FileSystemService.ReadAllText(Constants.Results_A2_2_FilesPath + "world192_encrypted.txt"), "monarchy");
            FileSystemService.WriteAllTextUtf8(Constants.Results_A2_2_FilesPath + "world192_decrypted.txt", decrypted);
        }
    }
}
