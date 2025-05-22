using System.Text.Json.Serialization;

namespace CEIMICBE.Modelos.Response.Consulta01Clink;

public class EncabezadoDTO
{
    public string nrLaudo { get; set; }
    public string dataLaudo { get; set; }
    public string nmLaudo { get; set; }
    public string idEntidadeExterna { get; set; }
    public string idCarga { get; set; }
    public string nrRelatorio { get; set; }
    public string dtEmissao { get; set; }
    public string nmCliente { get; set; }
    public string dsEnderecoColeta { get; set; }
    public string dsIdentificacaoProjeto { get; set; }
    public string nmContato { get; set; }
    public string dsIdentificacaoAmostra { get; set; }
    public string nmMatriz { get; set; }
    public string dtAmostragem { get; set; }
    public string dtRecebimento { get; set; }
    public string nmResponsavel { get; set; }
    public int qnParametros { get; set; }

    [JsonIgnore]
    public string NRCONTROLE1 { get; set; }
    [JsonIgnore]
    public string NRCONTROLE2 { get; set; }
    [JsonIgnore]    
    public string CDAMOSTRA { get; set; }
    public object tbParametros { get; set; }
    public string file { get; set; }
}
