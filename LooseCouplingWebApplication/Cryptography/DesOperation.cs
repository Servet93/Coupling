using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LooseCouplingWebApplication
{
    public class DesOperation: ICryptography 
    {  
        public string EncryptData(string data, string key)
        {  
            byte[] _key = Encoding.UTF8.GetBytes(key); //Encryption Key   
            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };  
            byte[] inputByteArray;   
  
            try  
            {
                // DESCryptoServiceProvider is a cryptography class defind in c#.  
                DESCryptoServiceProvider ObjDES = new DESCryptoServiceProvider();  
                inputByteArray = Encoding.UTF8.GetBytes(data);  
                MemoryStream Objmst = new MemoryStream();  
                CryptoStream Objcs = new CryptoStream(Objmst, ObjDES.CreateEncryptor(_key, IV), CryptoStreamMode.Write);  
                Objcs.Write(inputByteArray, 0, inputByteArray.Length);  
                Objcs.FlushFinalBlock();  
  
                return Convert.ToBase64String(Objmst.ToArray());//encrypted string  
            }  
            catch (System.Exception ex)  
            {  
                throw ex;  
            }  
        }  
  
        public string DecryptData(string data, string key)  
        {  
            byte[] _key = Encoding.UTF8.GetBytes(key);// Key   
            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };  
            byte[] inputByteArray = new byte[data.Length];  
  
            try  
            {
                DESCryptoServiceProvider ObjDES = new DESCryptoServiceProvider();  
                inputByteArray = Convert.FromBase64String(data);  
  
                MemoryStream Objmst = new MemoryStream();  
                CryptoStream Objcs = new CryptoStream(Objmst, ObjDES.CreateDecryptor(_key, IV), CryptoStreamMode.Write);  
                Objcs.Write(inputByteArray, 0, inputByteArray.Length);  
                Objcs.FlushFinalBlock();  
  
                Encoding encoding = Encoding.UTF8;  
                return encoding.GetString(Objmst.ToArray());  
            }  
            catch (System.Exception ex)  
            {  
                throw ex;  
            }  
        }   
    }  
}