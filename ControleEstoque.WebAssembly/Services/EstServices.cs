using ControleEstoque.WebAssembly.Interfaces;
using ControleEstoque.WebAssembly.Model;
using ControleEstoque.WebAssembly.Services;

namespace ControleEstoque.WebAssembly.Services;

public class EstServices : IEstoque
{
    private readonly ILocalStorageService _localStorage;
    private const string StorageKey = "estoques";

    public EstServices(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<ICollection<Estoque>> GetEstoqueList()
    {
        var estoques = await _localStorage.GetItemAsync<List<Estoque>>(StorageKey);
        return estoques ?? new List<Estoque>();
    }

    public async Task<Estoque?> GetEstoqueById(int estoqueId)
    {
        var estoques = await GetEstoqueList();
        return estoques.FirstOrDefault(e => e.Id == estoqueId);
    }

    public async Task AddEstoque(Estoque estoque)
    {
        var estoques = await GetEstoqueList();

        estoque.Id = estoques.Any() ? estoques.Max(e => e.Id) + 1 : 1;

        estoques.Add(estoque);
        await _localStorage.SetItemAsync(StorageKey, estoques);
    }

    public async Task UpdateStatusEstoque(int estoqueId, bool status)
    {
        var estoques = await GetEstoqueList();
        var estoque = estoques.FirstOrDefault(e => e.Id == estoqueId);

        if (estoque != null)
        {
            estoque.Status = status;
            await _localStorage.SetItemAsync(StorageKey, estoques);
        }
    }
}
