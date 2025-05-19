using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CEIMICBE.Modelos.Aplicacion;
using CEIMICBE.Modelos.Constantes;
using CEIMICBE.Modelos.Request.Consulta01Clink;
using CEIMICBE.Modelos.Response.Consulta01Clink;
using Dapper;

namespace CEIMICBE.Repositorio;

public class Consulta01ClinkRepositorio : RepositorioMSSQL
{
    public Consulta01ClinkRepositorio(string _cadenaConexion) : base(_cadenaConexion)
    {
    }
    public List<EncabezadoDTO> ListarEncabezado()
    {
        var parametros = new DynamicParameters();
        try
        {
            List<EncabezadoDTO> lista = base.Listar<EncabezadoDTO>($"{Constante.SPClinkConsulta01Encabezado}", parametros);
            return lista;
        }
        catch (Exception)
        {
            throw;
        }
        ;
    }
    public List<ParametroDTO> ListarParametro(int nrocontrole1, int nrocontrole2)
    {
        var parametros = new DynamicParameters();
        try
        {
            parametros.Add("@NRCONTROLE1", nrocontrole1);
            parametros.Add("@NRCONTROLE2", nrocontrole2);
            List<ParametroDTO> lista = base.Listar<ParametroDTO>($"{Constante.SPClinkConsulta01Parametros}", parametros);
            return lista;
        }
        catch (Exception)
        {
            throw;
        }
        ;
    }
    public List<FileDTO> GetFile(int cdamostra)
    {
        var parametros = new DynamicParameters();
        try
        {
            parametros.Add("@CDMOSTRA", cdamostra);
            List<FileDTO> lista = base.Listar<FileDTO>($"{Constante.SPClinkConsulta01File}", parametros);
            return lista;
        }
        catch (Exception)
        {
            throw;
        }
        ;
    }

    public RespuestaProceso RegistroJson(int cdamostra)
    {
        RespuestaProceso respuestaProceso = new RespuestaProceso();
        var parametros = new DynamicParameters();
        try
        {
            parametros.Add("@CDMOSTRA", cdamostra);
            respuestaProceso = base.Actualizar(Constante.SPClinkConsulta01setapiLaudo2, parametros);
        }
        catch (Exception ex)
        {
            return GenerarExcepcion(ex, (short)EstadoProceso.ErrorLogico);

        }
        ;
        return respuestaProceso;
    }
}
