using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SnapObjects.Data;

namespace FicharApi.Services
{
    public interface IUsuariosService
    {
        Task<string> EmpresaUsuarioAsync(string usuario, CancellationToken cancellationToken);
        
        Task<string> GrupoUsuarioAsync(string usuario, CancellationToken cancellationToken);
        
        Task<string> EmpleadoUsuarioAsync(string usuario, CancellationToken cancellationToken);
        
        Task<string> UsuarioEmpleadoAsync(string empresa, string empleado, CancellationToken cancellationToken);

    }
}
