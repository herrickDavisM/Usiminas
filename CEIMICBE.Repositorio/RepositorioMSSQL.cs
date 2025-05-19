
using Dapper;
using CEIMICBE.Modelos.Aplicacion;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CEIMICBE.Repositorio
{
    public class RepositorioMSSQL
    {
        SqlConnection con = new SqlConnection();
        string _cadenaConexion = "";

        public RepositorioMSSQL(string _cadenaConexion)
        {
            con.ConnectionString = _cadenaConexion;
        }

        public RespuestaProceso GenerarExcepcion(Exception exception, short estado)
        {
            return new RespuestaProceso()
            {
                estado = estado,
                mensaje = exception.Message

            };
        }
        /*public RespuestaAuth GenerarEx(Exception exception, short estado)
        {
            return new RespuestaAuth()
            {
                statusCode = estado,
                mensaje = exception.Message

            };
        }*/

        public Object Listar(string uspBD, DynamicParameters parameters)

        {
            Object dataResult = new Object();
            con.Open();

            try
            {
                var dataBD = con.Query(uspBD, param: parameters, commandType: CommandType.StoredProcedure);
                dataResult = dataBD.AsList();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            con.Close();

            return dataResult;
        }

        public List<T> Listar<T>(string uspBD, DynamicParameters parameters)
        where T : class, new()
        {
            List<T> dataResult = new List<T>();
            con.Open();
            try
            {
                var dataBD = con.Query<T>(uspBD, param: parameters, commandType: CommandType.StoredProcedure);
                dataResult = dataBD.AsList<T>();
            }
            catch (Exception)
            {
                con.Close();
                throw;
            }
            con.Close();

            return dataResult;
        }

        public T TraerUno<T>(string uspBD, DynamicParameters parameters)
        where T : class, new()
        {
            T dataResult = new T();
            con.Open();
            try
            {
                var dataBD = con.QueryFirst<T>(uspBD, param: parameters, commandType: CommandType.StoredProcedure);
                dataResult = dataBD;
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            con.Close();

            return dataResult;
        }
        public Object TraerUno(string uspBD, DynamicParameters parameters)
        {
            Object dataResult = new Object();
            con.Open();

            try
            {

                var dataBD = con.QueryFirst(uspBD, param: parameters, commandType: CommandType.StoredProcedure);
                dataResult = dataBD;
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            con.Close();

            return dataResult;
        }

        public RespuestaProceso Insertar(string uspBD, DynamicParameters parameters)
        {
            RespuestaProceso respuestaProceso;
            con.Open();
            try
            {
                parameters.Add("@n_result_Proceso", dbType: DbType.Int16, direction: ParameterDirection.Output);
                parameters.Add("@n_estado", dbType: DbType.Int16, direction: ParameterDirection.Output);
                parameters.Add("@s_mensaje", dbType: DbType.String, direction: ParameterDirection.Output, size: 1000);
                con.Execute(uspBD, param: parameters, commandType: CommandType.StoredProcedure);
                respuestaProceso = new RespuestaProceso()
                {
                    estado = parameters.Get<Int16>("@n_estado"),
                    mensaje = parameters.Get<String>("@s_mensaje"),
                    resultProceso = parameters.Get<Int16>("@n_result_Proceso"),
                };
            }
            catch (Exception ex)
            {
                con.Close();
                respuestaProceso = new RespuestaProceso()
                {
                    estado = -1,
                    mensaje = ex.Message
                };
            }
            con.Close();

            return respuestaProceso;
        }
        public RespuestaProceso Actualizar(string uspBD, DynamicParameters parameters)
        {
            RespuestaProceso respuestaProceso = new RespuestaProceso();
            con.Open();
            try
            {
                parameters.Add("@n_result_Proceso", dbType: DbType.Int16, direction: ParameterDirection.Output);
                parameters.Add("@n_estado", dbType: DbType.Int16, direction: ParameterDirection.Output);
                parameters.Add("@s_mensaje", dbType: DbType.String, direction: ParameterDirection.Output, size: 1000);
                con.Execute(uspBD, param: parameters, commandType: CommandType.StoredProcedure);
                respuestaProceso = new RespuestaProceso()
                {
                    estado = parameters.Get<Int16>("@n_estado"),
                    mensaje = parameters.Get<String>("@s_mensaje"),
                    resultProceso = parameters.Get<Int16>("@n_result_Proceso"),
                };
            }
            catch (Exception ex)
            {
                con.Close();
                respuestaProceso.estado = -1;
                respuestaProceso.mensaje = ex.Message;
            }
            con.Close();

            return respuestaProceso;
        }
        public RespuestaProceso Eliminar(string uspBD, DynamicParameters parameters)
        {
            RespuestaProceso respuestaProceso = new RespuestaProceso();
            con.Open();
            try
            {
                parameters.Add("@n_result_Proceso", dbType: DbType.Int16, direction: ParameterDirection.Output);
                parameters.Add("@n_estado", dbType: DbType.Int16, direction: ParameterDirection.Output);
                parameters.Add("@s_mensaje", dbType: DbType.String, direction: ParameterDirection.Output, size: 1000);
                con.Execute(uspBD, param: parameters, commandType: CommandType.StoredProcedure);
                respuestaProceso = new RespuestaProceso()
                {
                    estado = parameters.Get<Int16>("@n_estado"),
                    mensaje = parameters.Get<String>("@s_mensaje"),
                    resultProceso = parameters.Get<Int16>("@n_result_Proceso"),
                };
            }
            catch (Exception ex)
            {
                con.Close();
                respuestaProceso.estado = -1;
                respuestaProceso.mensaje = ex.Message;
            }
            con.Close();

            return respuestaProceso;
        }


        public string TraerUnoString(string uspBD, DynamicParameters parameters)
        {
            string dataResult = "";
            con.Open();

            try
            {
                string dataBD = con.QueryFirst<string>(uspBD, param: parameters, commandType: CommandType.StoredProcedure);
                dataResult = dataBD;
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            con.Close();

            return dataResult;
        }

        public RespuestaProceso Procesar(string uspBD, DynamicParameters parameters, string resultado)
        {
            RespuestaProceso respuestaProceso = new RespuestaProceso();
            con.Open();

            try
            {
                con.Execute(uspBD, param: parameters, commandType: CommandType.StoredProcedure);
                respuestaProceso.dataProceso = parameters.Get<int>(resultado);

            }
            catch (Exception ex)
            {
                con.Close();
                respuestaProceso.estado = -1;
                respuestaProceso.mensaje = ex.Message;
            }
            con.Close();

            return respuestaProceso;
        }

        public Object ListarSQL(string uspBD, DynamicParameters parameters)
        {
            Object dataResult = new Object();
            con.Open();

            try
            {                
                var dataBD = con.Query(uspBD, param: parameters); 
                dataResult = dataBD.AsList();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            con.Close();

            return dataResult;
        }

        public List<T> ListarSQL<T>(string uspBD, DynamicParameters parameters)
        where T : class, new()
        {
            List<T> dataResult = new List<T>();
            con.Open();

            try
            {
                var dataBD = con.Query<T>(uspBD, param: parameters); 
                dataResult = dataBD.AsList<T>();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            con.Close();

            return dataResult;
        }

        public T TraerUnoSQL<T>(string uspBD, DynamicParameters parameters)
        where T : class, new()
        {
            T dataResult = new T();
            con.Open();

            try
            {
                var dataBD = con.QueryFirst<T>(uspBD, param: parameters); 
                dataResult = dataBD;
            }
            catch (Exception ex)
            {
                dataResult = null;
                con.Close();
                throw;
            }
            con.Close();

            return dataResult;
        }
    }
}
