using FicharApi.Models;

namespace FicharApi.Services
{
    public interface IAuthService
    {
        Usuarios Login(AuthUser AuthRequest);
          

    }
}
