using ControleEstoque.WebAssembly.Interfaces;
using ControleEstoque.WebAssembly.Model;
using ControleEstoque.WebAssembly.Services;

namespace ControleEstoque.WebAssembly.Services;

public class ProdBaseServices : IProdutoBase
{
    private readonly ILocalStorageService _localStorage;
    private const string StorageKey = "produtosBase";

    public ProdBaseServices(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<ICollection<ProdutoBase>> GetProdutoBaseList()
    {
        var produtos = await _localStorage.GetItemAsync<List<ProdutoBase>>(StorageKey);
        return produtos ?? new List<ProdutoBase>();
    }

    public async Task<ProdutoBase?> GetProdutoBaseById(int produtoid)
    {
        var produtos = await GetProdutoBaseList();
        return produtos.FirstOrDefault(p => p.Id == produtoid);
    }

    public async Task AddProdutoBase(ProdutoBase produtobase)
    {
        var produtos = await GetProdutoBaseList();

        produtobase.Id = produtos.Any() ? produtos.Max(p => p.Id) + 1 : 1;

        produtos.Add(produtobase);
        await _localStorage.SetItemAsync(StorageKey, produtos);
    }

    public async Task UpdateProdutoBase(ProdutoBase produtobase)
    {
        var produtos = (await GetProdutoBaseList()).ToList();
        var index = produtos.FindIndex(p => p.Id == produtobase.Id);

        if (index >= 0)
        {
            produtos[index] = produtobase;
            await _localStorage.SetItemAsync(StorageKey, produtos);
        }
    }

    public async Task UpdateStatusProdutoBase(int produtobaseId, bool status)
    {
        var produtos = await GetProdutoBaseList();
        var produto = produtos.FirstOrDefault(p => p.Id == produtobaseId);

        if (produto != null)
        {
            produto.Status = status;
            await _localStorage.SetItemAsync(StorageKey, produtos);
        }
    }
}
