using Microsoft.AspNetCore.Http;

namespace LooseCouplingWebApplication.Services
{
    public interface IService<T>
    {
        void Save(T data);
        T Read(IFormFile formFile);
    }
}