using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CLink_ServiciosUSIMINAS
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }


        public async Task<ActionResult> EnviarJson()
        {

            try
            {
                string respuestaApi = await new Consulta01ClinkAPL(configuration, configuracionAPP).EnviarJson();

                return Ok(new
                {
                    respuestaApi
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
