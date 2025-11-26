using DesafioDevNet8.Domain.Entities;
using DesafioDevNet8.Domain.Interfaces;

namespace DesafioDevNet8.Infrastructure.Repositories;

public class PagamentoRepository : IPagamentoRepository
{
    private static readonly List<Pagamento> _pagamentos = new();

    public void Adicionar(Pagamento pagamento)
    {
        _pagamentos.Add(pagamento);
    }

    public Pagamento? ObterPorId(int id)
    {
        return _pagamentos.FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<Pagamento> ObterTodos()
    {
        return _pagamentos.AsEnumerable();
    }

    public void Atualizar(Pagamento pagamento)
    {
        var pagamentoExistente = ObterPorId(pagamento.Id);
        if (pagamentoExistente != null)
        {
            pagamentoExistente.Descricao = pagamento.Descricao;
            pagamentoExistente.ValorOriginal = pagamento.ValorOriginal;
            pagamentoExistente.DataVencimento = pagamento.DataVencimento;
            pagamentoExistente.ValorJuros = pagamento.ValorJuros;
            pagamentoExistente.ValorTotal = pagamento.ValorTotal;
            pagamentoExistente.DiasAtraso = pagamento.DiasAtraso;
        }
    }
}
