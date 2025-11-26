using DesafioDevNet8.Domain.Entities;
using DesafioDevNet8.Domain.Interfaces;

namespace DesafioDevNet8.Infrastructure.Repositories;

public class MovimentacaoEstoqueRepository : IMovimentacaoEstoqueRepository
{
    private static readonly List<MovimentacaoEstoque> _movimentacoes = new();

    public void Adicionar(MovimentacaoEstoque movimentacao)
    {
        _movimentacoes.Add(movimentacao);
    }

    public MovimentacaoEstoque? ObterPorId(Guid id)
    {
        return _movimentacoes.FirstOrDefault(m => m.Id == id);
    }

    public IEnumerable<MovimentacaoEstoque> ObterTodas()
    {
        return _movimentacoes.AsEnumerable();
    }

    // Retorna as movimentações ordenadas por data
    public IEnumerable<MovimentacaoEstoque> ObterPorProduto(int produtoId)
    {
        return _movimentacoes
            .Where(m => m.ProdutoId == produtoId)
            .OrderBy(m => m.DataMovimentacao);
    }
}
