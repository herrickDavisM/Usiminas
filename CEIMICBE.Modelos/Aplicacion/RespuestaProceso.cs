
using CEIMICBE.Modelos.Constantes;

namespace CEIMICBE.Modelos.Aplicacion
{
    public class RespuestaProceso
    {
        public RespuestaProceso()
        {
            estado = (int)EstadoProceso.Correcto;
            mensaje = String.Empty;
        }

        public RespuestaProceso(short _estado, string _mensaje)
        {
            estado = _estado;
            mensaje = _mensaje;
        }
        /// <summary>
        /// 0: OK
        /// 1:Error de Validacion
        /// -1:Error Interno App/BD
        /// </summary>
        public short estado { get; set; }
        /// <summary>
        /// Codigo de Error (Validacion o Interno BD)
        /// </summary>
        public string codigoError { get; set; }
        /// <summary>
        /// Mensaje de respuesta (Satisfactorio, Validacion o Error Interno)
        /// </summary>
        public string mensaje { get; set; }
        /// <summary>
        /// Resultado del procesamiento
        /// </summary>
        public int resultProceso { get; set; }
        /// <summary>
        /// Datos de proceso
        /// </summary>
        public object dataProceso { get; set; }
        /// <summary>
        /// Tipo de aprobación
        /// </summary>
    }
}
