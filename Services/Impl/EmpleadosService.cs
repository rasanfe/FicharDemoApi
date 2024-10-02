using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SnapObjects.Data;

namespace FicharApi.Services.Impl
{
    public class EmpleadosService : IEmpleadosService
    {
        private readonly DataContext _dataContext;
        
        public EmpleadosService(DefaultDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<string> CompuestoEmpleadoAsync(string empresa, string empleado, CancellationToken cancellationToken)
        {
            string nombre = "";
            string apellidos = "";
            string compuesto = "";
            
            
            var sql = @"SELECT nombre,
                        apellidos
                    FROM nomgenter
                    WHERE nomgenter.empresa=@empresa
                    AND nomgenter.codigo=@empleado";
            
            var list = await _dataContext.SqlExecutor.SelectAsync<DynamicModel>(sql, empresa, empleado, cancellationToken);
            
            int rowCount = list.Count;
            
           if (rowCount > 1)
            {
                throw new Exception("La consulta SQL ha devuelto más de un resultado.");
            }
            else if (rowCount == 0)
            {
                throw new Exception("La consulta SQL no ha devuelto ningún resultado.");
            }
            

            nombre = list[0].GetValue<string>("nombre");
            apellidos = list[0].GetValue<string>("apellidos");
            
            if ((nombre == null) || (nombre.Trim() == ""))
            {
                nombre = "";
            }
            
            if ((apellidos == null) || (apellidos.Trim() == ""))
            {
                apellidos = "";
            }

            compuesto = nombre + " " + apellidos;

            return compuesto;

        }

        public async Task<IList<DynamicModel>> ConductoresEmpresaAsync(string empresa, CancellationToken cancellationToken)
        {
            List<string> empleados = new List<string>();

            string grupo = "16"; //Consuctores

            var sql = @"SELECT ROW_NUMBER() OVER (ORDER BY convert(int, nomgenter.codigo)) as id,
                               nomgenter.empresa,
                               nomgenter.codigo,
                               trim(nomgenter.nombre)+' ' + trim(nomgenter.apellidos) as compuesto 
                    FROM nomgenter, 
                    Usuarios_mobile  
                    WHERE ( nomgenter.empresa = Usuarios_mobile.empresa ) and 
                          ( nomgenter.codigo = Usuarios_mobile.empleado ) and 
                          ( ( nomgenter.empresa = @empresa ) AND  
                          (Usuarios_mobile.grupo = @grupo) AND  
                          ( nomgenter.activo = 'S' ) AND  
                         (Usuarios_mobile.activo = 1)) 
                         ORDER BY convert(int, nomgenter.codigo)";

            var result = await _dataContext.SqlExecutor.SelectAsync<DynamicModel>(sql, empresa, grupo, cancellationToken);

            //empleados = result.Select(r => r.GetValue<string>(0)).ToList();



            return result;
        }


        public async Task<IList<DynamicModel>> EmpleadosEmpresasAsync(CancellationToken cancellationToken)
        {
            List<string> empleados = new List<string>();

            var sql = @"SELECT ROW_NUMBER() OVER (ORDER BY convert(int, nomgenter.codigo)) as id,
                               nomgenter.empresa,
                               nomgenter.codigo,
                               trim(nomgenter.nombre)+' ' + trim(nomgenter.apellidos) as compuesto 
                    FROM nomgenter, 
                    Usuarios_mobile  
                    WHERE ( nomgenter.empresa = Usuarios_mobile.empresa ) and 
                          ( nomgenter.codigo = Usuarios_mobile.empleado ) and 
                          ( ( nomgenter.activo = 'S' ) AND  
                         (Usuarios_mobile.activo = 1)) 
                         ORDER BY ROW_NUMBER() OVER (ORDER BY convert(int, nomgenter.codigo))";

            var result = await _dataContext.SqlExecutor.SelectAsync<DynamicModel>(sql, cancellationToken);

            //empleados = result.Select(r => r.GetValue<string>(0)).ToList();



            return result;
        }

       
    }
}
