using ControleEstoque.WebAssembly.Model.Enum;

namespace ControleEstoque.WebAssembly.Model;

public class EstMvp
{
    public int Id { get; set; }
    //public Guid LigacaoId { get; set; }
    public int ProdutoEstId { get; set; }
    public ProdutoEst? ProdutoEst { get; set; }

    public DateTime DataMov { get; set; } = DateTime.Now;

    public double Qtde { get; set; }

    public TipoMov TipoMovp { get; set; }

    public string? Observacao { get; set; }

}
