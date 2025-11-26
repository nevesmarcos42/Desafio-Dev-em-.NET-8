using DesafioDevNet8.Application.Interfaces;
using DesafioDevNet8.Domain.Entities;
using DesafioDevNet8.Domain.Interfaces;

namespace DesafioDevNet8.Application.Services;

// Service para controlar movimentações de estoque
public class EstoqueService : IEstoqueService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMovimentacaoEstoqueRepository _movimentacaoRepository;

    public EstoqueService(IProdutoRepository produtoRepository, IMovimentacaoEstoqueRepository movimentacaoRepository)
    {
        _produtoRepository = produtoRepository;
        _movimentacaoRepository = movimentacaoRepository;
    }

    // Adiciona produtos ao estoque
    public MovimentacaoEstoque RegistrarEntrada(int produtoId, int quantidade, string descricao)
    {
        var produto = _produtoRepository.ObterPorId(produtoId);
        if (produto == null)
            throw new InvalidOperationException($"Produto com ID {produtoId} não encontrado.");

        if (quantidade <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(quantidade));

        // Aumenta o estoque
        produto.QuantidadeEstoque += quantidade;
        _produtoRepository.Atualizar(produto);

        // Registra a movimentação
        var movimentacao = new MovimentacaoEstoque
        {
            ProdutoId = produtoId,
            TipoMovimentacao = "Entrada",
            Quantidade = quantidade,
            Descricao = descricao,
            QuantidadeAposMovimentacao = produto.QuantidadeEstoque
        };

        _movimentacaoRepository.Adicionar(movimentacao);
        return movimentacao;
    }

    // Remove produtos do estoque
    public MovimentacaoEstoque RegistrarSaida(int produtoId, int quantidade, string descricao)
    {
        var produto = _produtoRepository.ObterPorId(produtoId);
        if (produto == null)
            throw new InvalidOperationException($"Produto com ID {produtoId} não encontrado.");

        if (quantidade <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(quantidade));

        // Verifica se tem estoque suficiente
        if (produto.QuantidadeEstoque < quantidade)
        {
            throw new InvalidOperationException(
                $"Estoque insuficiente. Disponível: {produto.QuantidadeEstoque}, Solicitado: {quantidade}");
        }

        // Diminui o estoque
        produto.QuantidadeEstoque -= quantidade;
        _produtoRepository.Atualizar(produto);

        // Registra a movimentação
        var movimentacao = new MovimentacaoEstoque
        {
            ProdutoId = produtoId,
            TipoMovimentacao = "Saída",
            Quantidade = quantidade,
            Descricao = descricao,
            QuantidadeAposMovimentacao = produto.QuantidadeEstoque
        };

        _movimentacaoRepository.Adicionar(movimentacao);
        return movimentacao;
    }

    public IEnumerable<MovimentacaoEstoque> ObterHistoricoMovimentacoes(int produtoId)
    {
        return _movimentacaoRepository.ObterPorProduto(produtoId);
    }

    public int VerificarEstoque(int produtoId)
    {
        var produto = _produtoRepository.ObterPorId(produtoId);
        if (produto == null)
            throw new InvalidOperationException($"Produto com ID {produtoId} não encontrado.");

        return produto.QuantidadeEstoque;
    }
}
