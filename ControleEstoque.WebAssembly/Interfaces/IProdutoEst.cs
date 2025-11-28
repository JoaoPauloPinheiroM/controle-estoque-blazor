using ControleEstoque.WebAssembly.Model;
using ControleEstoque.WebAssembly.Model.Enum;
namespace ControleEstoque.WebAssembly.Interfaces;

public interface IProdutoEst
{
    public Task<ICollection<ProdutoEst>> GetProdutoEstList();
    public Task<ProdutoEst> GetProdutoEstById(int produtoid);
    public Task UpdateStatusProdutoEst(int produtoestId, bool status);
    public Task MovimentarEst ( string codProd , string CodEst , TipoMov TipoMov , double qtde , string observacao );
    public Task<ICollection<EstMvp>> GetMovimentacoesList();
}