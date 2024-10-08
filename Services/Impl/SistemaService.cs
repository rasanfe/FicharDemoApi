//using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SnapObjects.Data;

namespace FicharApi.Services.Impl
{
    public class SistemaService : ISistemaService
    {
        private readonly DataContext _dataContext;
        
        public SistemaService(DefaultDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
               
        public async Task<string> NombreEmpresaAsync(string empresa, CancellationToken cancellationToken)
        {
            string nombreEmpresa = "";

             var sql = @"
                    SELECT rtrim(nombre) 
                    FROM empresas
                    WHERE empresa = @Empresa;";

            nombreEmpresa = await _dataContext.SqlExecutor.ScalarAsync<string>(sql, new object[] { empresa }, cancellationToken) ?? "";

            return nombreEmpresa;
        }
        
    }
}
