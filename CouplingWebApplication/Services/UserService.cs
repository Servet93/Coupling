using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using CouplingWebApplication.Models;
using Microsoft.AspNetCore.Http;

namespace CouplingWebApplication.Services
{
    public class UserService
    {
        private AesOperation _aesOperation = new AesOperation();
        //private DesOperation _desOperation = new DesOperation();
        
        public UserService()
        {
            
        }

        public void Save(UserData userData)
        {
            userData.Email= _aesOperation.EncryptString(AppSettings.UserRegisterEmailKey, userData.Email);
            userData.Password = _aesOperation.EncryptString(AppSettings.UserRegisterPasswordKey, userData.Password);
            string fileName = $"{Guid.NewGuid()}.json";
            using FileStream createStream = File.Create(fileName);
            JsonSerializer.Serialize(createStream, userData, new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All, UnicodeRanges.All),
                WriteIndented = true,
            });
        }

        public UserData Read(IFormFile formFile)
        {
            using Stream createStream = formFile.OpenReadStream();
            var userData = JsonSerializer.Deserialize<UserData>(createStream);

            userData.Email = _aesOperation.DecryptString(AppSettings.UserRegisterEmailKey, userData.Email);
            userData.Password = _aesOperation.DecryptString(AppSettings.UserRegisterPasswordKey, userData.Password);

            return userData;
        }
    }
}