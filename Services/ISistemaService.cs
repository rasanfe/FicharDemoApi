using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FicharApi.Services
{
    public interface ISistemaService
    {
         Task<string> NombreEmpresaAsync(string empresa, CancellationToken cancellationToken);
    }
}
