
using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text;
using System.Transactions;
using CEIMICBE.Aplicacion.Base;
using CEIMICBE.Modelos.Aplicacion;
using CEIMICBE.Modelos.Constantes;
using CEIMICBE.Modelos.Response.Consulta01Clink;
using CEIMICBE.Repositorio;
using Dapper;
using Ionic.Zlib;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CEIMICBE.Aplicacion.CLinkCons01;
public class Consulta01ClinkAPL : AplicacionBase
{
    private readonly Consulta01ClinkRepositorio cons01ClinkRepositorio;
    public Consulta01ClinkAPL(IConfiguration appConfiguracion, IOptions<ConfiguracionAPP> configuracionAPP) : base(appConfiguracion, configuracionAPP)
    {
        cons01ClinkRepositorio = new Consulta01ClinkRepositorio(parametrosApp.ConexionBDClink);
    }

    public List<EncabezadoDTO> ListarEncabezado()
    {

        try
        {
            
            List<EncabezadoDTO> listaEncabezado = cons01ClinkRepositorio.ListarEncabezado();
            foreach (var item in listaEncabezado)
            {
                item.tbParametros = cons01ClinkRepositorio.ListarParametro(int.Parse(item.NRCONTROLE1), int.Parse(item.NRCONTROLE2));
                item.qnParametros = ((List<ParametroDTO>)item.tbParametros).Count;


                List<FileDTO> getFile = cons01ClinkRepositorio.GetFile(int.Parse(item.CDAMOSTRA));
                foreach (var itemFile in getFile)
                {
                    item.file = ObtenerPdfBase64(itemFile.Arquivo);

                }
                //enviar encabezado a funcion funion EnviarJsonParametros quye recibe un string en formato Json
                string jsonEncabezado = JsonConvert.SerializeObject(item);
                EnviarJsonParametros(jsonEncabezado);
                


            }

            return listaEncabezado;

        }
        catch (Exception)
        {
            return [];
        }

    }

    public static bool EsZlib(byte[] data)
    {
        return data.Length > 2 && data[0] == 0x78 && (data[1] == 0x9C || data[1] == 0xDA);
    }

    public static bool EsZip(byte[] data)
    {
        return data.Length > 4 && data[0] == 0x50 && data[1] == 0x4B;
    }

    public static bool EsPdf(byte[] data)
    {
        return data.Length > 4 && data[0] == 0x25 && data[1] == 0x50 && data[2] == 0x44 && data[3] == 0x46;
    }

    public static string ObtenerPdfBase64(byte[] fileData)
    {
        // Ver si es un Zlib comprimido
        if (EsZlib(fileData))
        {
            try
            {
                using (var compressedStream = new MemoryStream(fileData))
                using (var decompressedStream = new MemoryStream())
                using (var zlibStream = new ZlibStream(compressedStream, Ionic.Zlib.CompressionMode.Decompress))
                {
                    zlibStream.CopyTo(decompressedStream);
                    fileData = decompressedStream.ToArray(); 
                }
            }
            catch (Exception ex)
            {
                return $"Error al descomprimir Zlib: {ex.Message}";
            }
        }

        // Ver si es ZIP
        if (EsZip(fileData))
        {
            try
            {
                using (var zipStream = new MemoryStream(fileData))
                using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Read))
                {
                    var pdfEntry = archive.Entries.FirstOrDefault(e => e.FullName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase));

                    if (pdfEntry != null)
                    {
                        using (var entryStream = pdfEntry.Open())
                        using (var pdfMemory = new MemoryStream())
                        {
                            entryStream.CopyTo(pdfMemory);
                            byte[] pdfBytes = pdfMemory.ToArray();
                            return Convert.ToBase64String(pdfBytes);
                        }
                    }
                }

                return "No se encontró un PDF dentro del ZIP.";
            }
            catch (Exception ex)
            {
                return $"Error al leer ZIP: {ex.Message}";
            }
        }

        // Ver si es PDF
        if (EsPdf(fileData))
        {
            return Convert.ToBase64String(fileData);
        }

        return "No es un PDF";
    }

    public gettokenDTO  ObtenerToken()
    {
        // URL del endpoint de token
        string tokenUrl = "https://sso-ipa.usiminas.com/auth/realms/usiminas/protocol/openid-connect/token";

        using (var client = new HttpClient())
        {
            // Construir la solicitud HTTP POST
            var request = new HttpRequestMessage(HttpMethod.Post, tokenUrl);
            // Si se requiere enviar cookies, se agregan en la cabecera
            request.Headers.Add("Cookie", "citrix_ns_id=AAA7IjjUZzt5YocAAAAAADtvjESSijNyBhCBO4idWuuPmMAsd-NuWxhulT_WKtsOOw==jz3UZw==3wEcokKJP0nIM4b2YpW6zVnwFZE=; 08c4e147c55816c374adb5edce82cf29=58d44d13b367991fe864a853680d5725");

            // Parámetros del formulario para el grant_type "password"
            var formFields = new Dictionary<string, string>
           {
               { "username", "SERVLABCEIMIC01" },
               { "password", "OoB5qCuZUn3NYP" },
               { "client_id", "NBGA_ENTIDADES_EXTERNAS" },
               { "client_secret", "dc922c82-d352-4284-97fa-3339f8c300da" },
               { "grant_type", "password" }
           };

            // Asigna el contenido del body en formato application/x-www-form-urlencoded
            request.Content = new FormUrlEncodedContent(formFields);

            // Enviar la solicitud de forma sincrónica
            var response = client.SendAsync(request).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();

            // Leer la respuesta
            var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        

            gettokenDTO tokenResponse = JsonConvert.DeserializeObject<gettokenDTO>(result);

            // Leer el contenido de la respuesta
            string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            dynamic json = JsonConvert.DeserializeObject(responseContent);
            string accessToken = json.access_token;

            return tokenResponse;
        }
    }

    private  string EnviarJsonParametros(string jsonEncabezado)
    {
        var token = ObtenerToken();

        using var client = new HttpClient();

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);

        string url = "https://nbga-gestao-ambiental-back-quarkus-hml-ipa.usiminas.com/rest/ceimic-api/salvar-anexo-info";
        var content = new StringContent(jsonEncabezado, Encoding.UTF8, "application/json");

        var response = client.PostAsync(url, content).Result;
        var result = response.Content.ReadAsStringAsync().Result;

        //capturar errores de response para ser mas detallados enrespuesta
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            Console.WriteLine("Token expirado o no autorizado.");
            return "Token expirado o no autorizado.";
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            Console.WriteLine($"Error en la solicitud: {result}");
            return $"Error en la solicitud: {result}";
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
        {
            Console.WriteLine($"Error interno del servidor: {result}");
            return $"Error interno del servidor: {result}";
        }



        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Error {response.StatusCode}: {result}");
        }
        string a = "";
        return a;
        
    }


    public RespuestaProceso RegistroJson(int cdamostra)
    {
        RespuestaProceso respuestaProceso;
        try
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    respuestaProceso = cons01ClinkRepositorio.RegistroJson(cdamostra);
                    if (respuestaProceso.estado == 0 && respuestaProceso.resultProceso > 0)
                    {
                        scope.Complete();
                    }
                    else
                        scope.Dispose();
                }
            }
            catch (TransactionAbortedException ex)
            {
                respuestaProceso = new RespuestaProceso()
                {
                    estado = (int)EstadoProceso.ErrorMSSQL,
                    mensaje = ex.Message
                };
            }
        }
        catch (Exception ex)
        {
            respuestaProceso = new RespuestaProceso((short)EstadoProceso.ErrorLogico, ex.Message);
        }
        return respuestaProceso;
    }

}
