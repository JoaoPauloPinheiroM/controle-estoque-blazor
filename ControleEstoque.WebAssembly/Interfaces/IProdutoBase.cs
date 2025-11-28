using ControleEstoque.WebAssembly.Model;
namespace ControleEstoque.WebAssembly.Interfaces;

public interface IProdutoBase
{
    public Task<ICollection<ProdutoBase>> GetProdutoBaseList();
    public Task<ProdutoBase> GetProdutoBaseById(int produtoid);
    public Task AddProdutoBase(ProdutoBase produtobase);
    public Task UpdateProdutoBase(ProdutoBase produtobase);
    public Task UpdateStatusProdutoBase(int produtobaseId, bool status);
}
