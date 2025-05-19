using CEIMICBE.Modelos.Aplicacion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CEIMICBE.Aplicacion.Base
{
    public class AplicacionBase
    {
        public IConfiguration configuracion;
        public ConfiguracionAPP parametrosApp;
        public AplicacionBase(IConfiguration iConfiguration, IOptions<ConfiguracionAPP> iConfiguracionAPP)
        {
            configuracion = iConfiguration;
            parametrosApp = iConfiguracionAPP.Value;
        }
    }
}