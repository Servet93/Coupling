using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using LooseCouplingWebApplication.Models;
using Microsoft.AspNetCore.Http;

namespace LooseCouplingWebApplication.Services
{
    public class UserService : IService<UserData>
    {
        private readonly ICryptography _cryptography;
        private readonly string _userRegisterEmailKey;
        private readonly string _userRegisterPasswordKey;
        public UserService(ICryptography cryptography, string userRegisterEmailKey, string userRegisterPasswordKey)
        {
            _cryptography = cryptography;
            _userRegisterEmailKey = userRegisterEmailKey;
            _userRegisterPasswordKey = userRegisterPasswordKey;
        }

        public void Save(UserData userData)
        {
            userData.Email = _cryptography.EncryptData(userData.Email, _userRegisterEmailKey);
            userData.Password = _cryptography.EncryptData(userData.Password, _userRegisterPasswordKey);
            string fileName = $"User_{Guid.NewGuid()}.json";
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

            userData.Email = _cryptography.DecryptData(userData.Email, _userRegisterEmailKey);
            userData.Password = _cryptography.DecryptData(userData.Password, _userRegisterPasswordKey);

            return userData;
        }
    }
}