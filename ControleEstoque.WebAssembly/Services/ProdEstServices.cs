using ControleEstoque.WebAssembly.Interfaces;
using ControleEstoque.WebAssembly.Model;
using ControleEstoque.WebAssembly.Model.Enum;
using ControleEstoque.WebAssembly.Services;

namespace ControleEstoque.WebAssembly.Services;

public class ProdEstServices : IProdutoEst
{
    private readonly ILocalStorageService _localStorage;
    private readonly IEstoque _estoqueService;
    private readonly IProdutoBase _produtoBaseService;

    private const string ProdutosEstKey = "produtosEst";
    private const string MovimentacoesKey = "movimentacoes";

    public ProdEstServices(
        ILocalStorageService localStorage,
        IEstoque estoqueService,
        IProdutoBase produtoBaseService)
    {
        _localStorage = localStorage;
        _estoqueService = estoqueService;
        _produtoBaseService = produtoBaseService;
    }

    public async Task<ProdutoEst?> GetProdutoEstById(int produtoid)
    {
        var produtos = await GetProdutoEstList();
        return produtos.FirstOrDefault(p => p.ProdutoEstId == produtoid);
    }

    public async Task<ICollection<ProdutoEst>> GetProdutoEstList()
    {
        var produtos = await _localStorage.GetItemAsync<List<ProdutoEst>>(ProdutosEstKey);
        return produtos ?? new List<ProdutoEst>();
    }

    public async Task MovimentarEst(string codProd, string codEst, TipoMov tipoMov, double qtde, string observacao)
    {
        // Validar estoque
        var estoques = await _estoqueService.GetEstoqueList();
        var estoque = estoques.FirstOrDefault(e => e.CodEstoque == codEst);

        if (estoque == null || !estoque.Status)
        {
            throw new Exception("Estoque não EXISTE ou está INATIVO");
        }

        // Validar produto base
        var produtosBase = await _produtoBaseService.GetProdutoBaseList();
        var prodBase = produtosBase.FirstOrDefault(pb => pb.CodProd == codProd);

        if (prodBase == null || !prodBase.Status)
        {
            throw new Exception("Produto não EXISTE ou está INATIVO");
        }

        // Obter lista de produtos em estoque
        var produtosEst = (await GetProdutoEstList()).ToList();
        var prodEst = produtosEst.FirstOrDefault(pe => pe.EstoqueId == estoque.Id && pe.CodProd == codProd);

        // Se produto não existe no estoque
        if (prodEst == null)
        {
            if (tipoMov == TipoMov.Saida)
            {
                throw new Exception("Produto não EXISTE no estoque para SAIDA");
            }

            prodEst = new ProdutoEst
            {
                ProdutoEstId = produtosEst.Any() ? produtosEst.Max(p => p.ProdutoEstId) + 1 : 1,
                CodProd = codProd,
                ProdutoDescricao = prodBase.Descricao,
                EstoqueId = estoque.Id,
                ProdutoBaseId = prodBase.Id,
                QtdeEst = qtde,
                Status = true
            };
            produtosEst.Add(prodEst);
        }
        else
        {
            // Atualizar quantidade
            if (tipoMov == TipoMov.Entrada)
            {
                prodEst.QtdeEst += qtde;
            }
            else if (tipoMov == TipoMov.Saida)
            {
                if (prodEst.QtdeEst < qtde)
                {
                    throw new Exception("Quantidade em estoque insuficiente para SAIDA");
                }
                prodEst.QtdeEst -= qtde;
            }
        }

        // Salvar produtos em estoque
        await _localStorage.SetItemAsync(ProdutosEstKey, produtosEst);

        // Registrar movimentação
        var movimentacoes = await _localStorage.GetItemAsync<List<EstMvp>>(MovimentacoesKey)
            ?? new List<EstMvp>();

        var mov = new EstMvp
        {
            Id = movimentacoes.Any() ? movimentacoes.Max(m => m.Id) + 1 : 1,
            ProdutoEstId = prodEst.ProdutoEstId,
            Qtde = qtde,
            TipoMovp = tipoMov,
            Observacao = observacao,
            DataMov = DateTime.Now
        };

        movimentacoes.Add(mov);
        await _localStorage.SetItemAsync(MovimentacoesKey, movimentacoes);
    }

    public async Task UpdateStatusProdutoEst(int produtoestId, bool status)
    {
        var produtos = (await GetProdutoEstList()).ToList();
        var produto = produtos.FirstOrDefault(p => p.ProdutoEstId == produtoestId);

        if (produto != null)
        {
            produto.Status = status;
            await _localStorage.SetItemAsync(ProdutosEstKey, produtos);
        }
    }

    public async Task<ICollection<EstMvp>> GetMovimentacoesList()
    {
        var movimentacoes = await _localStorage.GetItemAsync<List<EstMvp>>(MovimentacoesKey);
        return movimentacoes ?? new List<EstMvp>();
    }
}
