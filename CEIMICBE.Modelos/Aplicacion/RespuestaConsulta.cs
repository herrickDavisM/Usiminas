
namespace CEIMICBE.Modelos.Aplicacion
{
    public class RespuestaConsulta
    {
        public RespuestaConsulta()
        {
            estado = true;
            mensaje = String.Empty;
        }

        public RespuestaConsulta(bool _estado, string _mensaje)
        {
            estado = _estado;
            mensaje = _mensaje;
        }
        /// <summary>
        /// Estado proceso
        /// </summary>
        public bool estado { get; set; }
        /// <summary>
        /// Mensaje de respuesta 
        /// </summary>
        public string mensaje { get; set; }

        /// <summary>
        /// object que contiene la data de consulta
        /// </summary>
        public object respuestaData { get; set; }
    }
}
