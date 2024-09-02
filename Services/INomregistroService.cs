using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FicharApi;
using FicharApi.Models;

namespace FicharApi.Services
{
    public interface INomregistroService
    {
		Task<IEnumerable<Dw_Import_Nomregistro>> RetrieveAsync(string as_empresa, string as_empelado, DateTime? adt_fecha, CancellationToken cancellationToken);

        Task FicharAsync(string empresa, string empleado, double latitud, double longitud, CancellationToken cancellationToken);

        Task CambiarHoraAsync(int id, DateTime nuevaHora, CancellationToken cancellationToken);

        Task BorrarFichajeAsync(int id, CancellationToken cancellationToken);

    }
}