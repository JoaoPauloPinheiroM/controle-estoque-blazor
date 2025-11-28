namespace ControleEstoque.WebAssembly.Model;

public class Estoque
{

    public int Id { get; set;}

    public string? CodEstoque { get; set; }

    public string? EstoqueDescricao { get; set;}

    public bool Status { get; set; } = false;

    public ICollection<ProdutoEst> ProdutosEst { get; set; } = new List<ProdutoEst>();
}
