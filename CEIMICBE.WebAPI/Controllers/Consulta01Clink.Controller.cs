using CEIMICBE.Aplicacion.CLinkCons01;
using CEIMICBE.Modelos.Aplicacion;
using CEIMICBE.Modelos.Response.Consulta01Clink;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CEIMICBE.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Consulta01Clink : ControllerBase
{
    private readonly IConfiguration configuration;
    private readonly IOptions<ConfiguracionAPP> configuracionAPP;
    private readonly ILogger<Consulta01Clink> logger;
    private readonly IWebHostEnvironment environment;
    public Consulta01Clink(ILogger<Consulta01Clink> _logger,
        IConfiguration _configuration,
        IOptions<ConfiguracionAPP> _configuracionAPP,
        IOptions<ConexionBD> _conexionBD,
        IWebHostEnvironment hostEnvironment)
    {
        logger = _logger;
        configuration = _configuration;
        configuracionAPP = _configuracionAPP;
        environment = hostEnvironment;
    }

    [HttpGet]
    [Route("ListarEncabezado")]
    public ActionResult ListarEncabezado()
    {

        try
        {
            List<EncabezadoDTO> respuestaConsulta = new Consulta01ClinkAPL(configuration, configuracionAPP).ListarEncabezado();   
            
            return Ok(respuestaConsulta);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }


    
    [HttpGet]
    [Route("getToken")]
    public ActionResult getToken()
    {
        Consulta01ClinkAPL objResp = new Consulta01ClinkAPL(configuration, configuracionAPP);

        try
        {
            var obj = objResp.ObtenerToken();
            return Ok(obj);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPost]
    [Route("RegistroJson")]
    public ActionResult RegistroJson(int cdamostra)
    {
        RespuestaProceso respuestaProceso = new RespuestaProceso();
        try
        {
            respuestaProceso = new Consulta01ClinkAPL(configuration, configuracionAPP).RegistroJson(cdamostra);

            return Ok(respuestaProceso);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }



}

