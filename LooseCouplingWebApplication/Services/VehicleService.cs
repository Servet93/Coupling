using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using LooseCouplingWebApplication.Models;
using Microsoft.AspNetCore.Http;

namespace LooseCouplingWebApplication.Services
{
    public class VehicleService : IService<VehicleData>
    {
        private readonly ICryptography _cryptography;
        private readonly string _vehicleIdentificationSecretKey;
        private readonly string _userRegisterPasswordKey;
        
        public VehicleService(ICryptography cryptography, string vehicleIdentificationSecretKey)
        {
            _cryptography = cryptography;
            _vehicleIdentificationSecretKey = vehicleIdentificationSecretKey;
        }

        public void Save(VehicleData vehicleData)
        {
            vehicleData.IdentificationNumber = _cryptography.EncryptData(vehicleData.IdentificationNumber, _vehicleIdentificationSecretKey);
            string fileName = $"Vehicle_{Guid.NewGuid()}.json";
            using FileStream createStream = File.Create(fileName);
            JsonSerializer.Serialize(createStream, vehicleData, new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All, UnicodeRanges.All),
                WriteIndented = true,
            });
        }

        public VehicleData Read(IFormFile formFile)
        {
            using Stream createStream = formFile.OpenReadStream();
            var vehicleData = JsonSerializer.Deserialize<VehicleData>(createStream);

            vehicleData.IdentificationNumber = _cryptography.DecryptData(vehicleData.IdentificationNumber, _vehicleIdentificationSecretKey);
            return vehicleData;
        }
    }
}