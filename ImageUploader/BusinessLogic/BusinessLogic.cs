using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;

namespace ImageUploader.BusinessLogic
{
    public class BusinessLogic
    {
        const string ServerLocation = "//snavndrsfint111.fastts.firstam.net/CodeFest5/Enzo Techoholics/enc/";
        public bool EncryptImageN_Save(string ImageName)
        {
            var filePath = "C:\\Users\\vvvidhyuth\\Desktop\\CodeFest5.0\\ImageUploader\\ImageUploader\\UploadedImagesTemp\\" + ImageName;
            //var filePath = "../UploadedImagesTemp/" + ImageName;
            byte[] imageBytes = File.ReadAllBytes(filePath);

            string original = Convert.ToBase64String(imageBytes);
            byte[] encrypted = null;
            using (AesManaged myAes = new AesManaged())
            {
                myAes.Key = keyGenerator();
                myAes.IV = GenerateIV();
                // Encrypt the string to an array of bytes.
                encrypted = EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV);
            }

            if (encrypted != null)
            {
                string serverPath = ServerLocation + ImageName;
                //string serverPath = "../DownloadedImagesTemp/" + ImageName;

                File.WriteAllBytes(serverPath, encrypted);

                File.Delete(filePath);//Deleting the file from API server
                return true;
            }


            return false;
        }

        public async Task<string> GetN_DecryptImage(string ImageName)
        {
            try
            {

                var filePath = ServerLocation + ImageName;
                if (!Directory.Exists(ServerLocation) || !File.Exists(ServerLocation + ImageName)) 
                {
                    throw new Exception("Encrypted file is not found");
                }

                byte[] encrypted = File.ReadAllBytes(filePath);

                if ((encrypted == null || encrypted.Length == 0))
                    throw new Exception("Encrypted file is null");


                string original = string.Empty;

                using (AesManaged myAes = new AesManaged())
                {
                    //myAes.GenerateKey();
                    //keyGenerator();

                    //myAes.GenerateIV();
                    //GenerateIV();

                    myAes.Key = keyGenerator();
                    myAes.IV = GenerateIV();
                    original = DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);
                }
                if (!string.IsNullOrEmpty(original))
                {
                    return original;
                }
                throw new Exception("Image not found");
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        private byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an AesManaged object
            // with the specified key and IV.
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        private string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an AesManaged object
            // with the specified key and IV.
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        public byte[] GenerateIV()
        {
            string keyStr = "a864#$jk%_^&6157";//length needs to 16
            byte[] key = System.Text.Encoding.UTF8.GetBytes(keyStr);

            return key;
        }

        public byte[] keyGenerator()
        {
            string keyStr = "01#$234ijklmn!@%^56789abc&*defgh";//length needs to 32

            byte[] key = System.Text.Encoding.UTF8.GetBytes(keyStr);

            return key;
        }

    }
}