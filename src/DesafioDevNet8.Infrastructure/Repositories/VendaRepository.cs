using DesafioDevNet8.Domain.Entities;
using DesafioDevNet8.Domain.Interfaces;

namespace DesafioDevNet8.Infrastructure.Repositories;

public class VendaRepository : IVendaRepository
{
    private static readonly List<Venda> _vendas = new();

    public void Adicionar(Venda venda)
    {
        _vendas.Add(venda);
    }

    public Venda? ObterPorId(int id)
    {
        return _vendas.FirstOrDefault(v => v.Id == id);
    }

    public IEnumerable<Venda> ObterTodas()
    {
        return _vendas.AsEnumerable();
    }

    public IEnumerable<Venda> ObterPorVendedor(int vendedorId)
    {
        return _vendas.Where(v => v.VendedorId == vendedorId);
    }
}
