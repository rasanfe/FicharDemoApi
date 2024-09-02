using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SnapObjects.Data;
using DWNet.Data;
using FicharApi;
using FicharApi.Models;

namespace FicharApi.Services.Impl
{
    /// <summary>
    /// The service needs to be injected into the ConfigureServices method of the Startup class. The sample code is as follows:
    ///  services.AddScoped<I<NomregistroService>, NomregistroService>();
    /// </summary>
    public class NomregistroService : INomregistroService
    {
        private readonly DataContext _dataContext;
        private readonly IUsuariosService _usuarioService;
        
        public NomregistroService(DefaultDataContext dataContext)
        {
            _dataContext = dataContext;
            _usuarioService = new UsuariosService(dataContext);
        }


        public async Task<IEnumerable<Dw_Import_Nomregistro>> RetrieveAsync(string as_empresa,string as_empelado,DateTime? adt_fecha, CancellationToken cancellationToken)
		{
			var dataStore = new DataStore<Dw_Import_Nomregistro>(_dataContext);
            
			await dataStore.RetrieveAsync(new object[] { as_empresa, as_empelado, adt_fecha }, cancellationToken);
            
			return dataStore;
		}
        
        public async Task FicharAsync(string empresa, string empleado, double latitud, double longitud, CancellationToken cancellationToken)
        {
            using (var transaction = _dataContext.BeginTransaction())
            {
                const int mode = 37;
                const string procedureName = "fichar";
                
                try
                {
                    // Sets the parameter values.
                    // The parameter direction defaults to ParameterDirection.Input,
                    // so you can omit it.
                    ParamValue Empresa = ParamValue.New<string>("empresa", empresa);
                    ParamValue Empleado = ParamValue.New<string>("empleado", empleado);
                    ParamValue Mode = ParamValue.New<int>("mode", mode);
                    ParamValue Latitud = ParamValue.New<double>("latitud", latitud);
                    ParamValue Longitud = ParamValue.New<double>("longitud", longitud);

                    object[] idParams = new object[]
                     {
                        Empresa,
                        Empleado,
                        Mode,
                        Latitud,
                        Longitud
                     };

                    var rowsAffected = await _dataContext.SqlExecutor.ExecuteProcedureAsync(procedureName, idParams, cancellationToken);
                    
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Error al ejecutar el procedimiento almacenado fichar: {ex.Message}");
                }
            }
        }
        
            
        public async Task CambiarHoraAsync(int id, DateTime nuevaHora, CancellationToken cancellationToken)
        {
            using (var transaction = _dataContext.BeginTransaction())
            {
                try
                {
                    int mode = await GetModeFromIdAsync(id, cancellationToken);

                    if (mode != 32 ) //Insertado lo dejamos como insertado.
                    {
                        mode = 35; // Modificado
                    }
                    
                    // Actualizar Hora
                    var sql = @"UPDATE nomregistro
                                  SET nomregistro.datetime = @Nuevahora,
                                      mode = @mode  
                                WHERE nomregistro.no=@id";

                    object[] parametersUpdate = new object[]
                    {
                        nuevaHora,
                        mode,
                        id
                    };
                    
                    var rowsAffected = await _dataContext.SqlExecutor.ExecuteAsync(sql, parametersUpdate, cancellationToken);
                    
                    // Confirmar la transacción
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Deshacer la transacción en caso de error
                    transaction.Rollback();
                    throw new Exception($"Error al actualizar nomregistro: {ex.Message}");
                }
            }

        }

        public async Task BorrarFichajeAsync(int id, CancellationToken cancellationToken)
        {
            using (var transaction = _dataContext.BeginTransaction())
            {
                try
                {
                    // Actualizar Hora
                    var sql = @"DELETE nomregistro
                                     WHERE nomregistro.no=@id";

                    object[] parametersDelete = new object[]
                     {
                        id
                     };

                    var rowsAffected = await _dataContext.SqlExecutor.ExecuteAsync(sql, parametersDelete, cancellationToken);

                    // Confirmar la transacción
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Deshacer la transacción en caso de error
                    transaction.Rollback();
                    throw new Exception($"Error al eliminar nomregistro: {ex.Message}");
                }
            }
        }

        private async Task<int> GetModeFromIdAsync(int id, CancellationToken cancellationToken)
        {
             var sql = @"
                    SELECT mode 
                    FROM nomregistro
                    WHERE nomregistro.no = @Id;";

            int mode = await _dataContext.SqlExecutor.ScalarAsync<int>(sql, new object[] { id }, cancellationToken);

            return mode;
        }

        
    }
}