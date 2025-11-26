using DesafioDevNet8.Domain.Entities;
using DesafioDevNet8.Domain.Interfaces;

namespace DesafioDevNet8.Infrastructure.Repositories;

// Armazena vendedores em memória (os dados não são salvos em banco)
public class VendedorRepository : IVendedorRepository
{
    private static readonly List<Vendedor> _vendedores = new();

    public void Adicionar(Vendedor vendedor)
    {
        _vendedores.Add(vendedor);
    }

    public Vendedor? ObterPorId(int id)
    {
        return _vendedores.FirstOrDefault(v => v.Id == id);
    }

    public IEnumerable<Vendedor> ObterTodos()
    {
        return _vendedores.AsEnumerable();
    }

    public void Atualizar(Vendedor vendedor)
    {
        var vendedorExistente = ObterPorId(vendedor.Id);
        if (vendedorExistente != null)
        {
            vendedorExistente.Nome = vendedor.Nome;
            vendedorExistente.ComissaoTotal = vendedor.ComissaoTotal;
        }
    }
}
