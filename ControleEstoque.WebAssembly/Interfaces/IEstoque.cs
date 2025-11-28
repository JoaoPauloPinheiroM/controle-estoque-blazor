using ControleEstoque.WebAssembly.Model;

namespace ControleEstoque.WebAssembly.Interfaces;

public interface IEstoque
{
    public  Task<ICollection<Estoque>> GetEstoqueList();
    public  Task<Estoque> GetEstoqueById(int estoqueId);
    public  Task AddEstoque(Estoque estoque);
    public  Task UpdateStatusEstoque(int estoqueId, bool status);

}
