namespace ControleEstoque.WebAssembly.Model;

public class ProdutoEst 
{
    public int ProdutoEstId { get; set;}

    public int EstoqueId { get; set;}
    public virtual Estoque? Estoque { get; set; }

    public int ProdutoBaseId { get; set;}
    public virtual ProdutoBase? Prodbase { get; set; }

    public string? ProdutoDescricao { get; set;}

    public string? CodProd  { get; set;}

    public bool Status { get; set; } = false;


    public double QtdeEst { get; set;}

}
