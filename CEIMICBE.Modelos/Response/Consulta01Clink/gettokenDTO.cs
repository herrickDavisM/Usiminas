using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIMICBE.Modelos.Response.Consulta01Clink
{
    public class gettokenDTO
    {
       public  string? access_token { get; set; }
        public int? expires_in { get; set; }
        public string? refresh_expires_in { get; set; }
        public string? refresh_token { get; set; }
        public string? token_type { get; set; }
        public string? not_before_policy { get; set; }
        public string? session_state { get; set; }
        public string? scope { get; set; }

    }
}
