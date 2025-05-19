
namespace CEIMICBE.WebAPI.Service;

public interface IServicioArchivo
{
    string ObtenerPdfBase64(byte[] fileData);
}
