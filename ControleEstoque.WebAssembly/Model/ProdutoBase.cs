namespace ControleEstoque.WebAssembly.Model;

public class ProdutoBase
{
    public int Id { get; set;}
    public string? Descricao { get; set;}
    public string? CodProd  { get; set;}
    public bool Status { get; set; } = false;
}
