using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SnapObjects.Data;

namespace FicharApi.Services
{
    public interface IEmpleadosService
    {
        Task<string> CompuestoEmpleadoAsync(string empresa, string empleado, CancellationToken cancellationToken);
           
    }
    
}
