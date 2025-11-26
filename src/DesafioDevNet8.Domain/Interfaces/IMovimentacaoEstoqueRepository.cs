using DesafioDevNet8.Domain.Entities;

namespace DesafioDevNet8.Domain.Interfaces;

/// <summary>
/// Interface para o repositório de movimentações de estoque
/// Define os métodos de acesso e manipulação de dados de movimentações
/// </summary>
public interface IMovimentacaoEstoqueRepository
{
    /// <summary>
    /// Adiciona uma nova movimentação ao repositório
    /// </summary>
    /// <param name="movimentacao">Movimentação a ser adicionada</param>
    void Adicionar(MovimentacaoEstoque movimentacao);

    /// <summary>
    /// Busca uma movimentação pelo ID
    /// </summary>
    /// <param name="id">ID da movimentação (GUID)</param>
    /// <returns>Movimentação encontrada ou null</returns>
    MovimentacaoEstoque? ObterPorId(Guid id);

    /// <summary>
    /// Obtém todas as movimentações cadastradas
    /// </summary>
    /// <returns>Lista de movimentações</returns>
    IEnumerable<MovimentacaoEstoque> ObterTodas();

    /// <summary>
    /// Obtém todas as movimentações de um produto específico
    /// </summary>
    /// <param name="produtoId">ID do produto</param>
    /// <returns>Lista de movimentações do produto</returns>
    IEnumerable<MovimentacaoEstoque> ObterPorProduto(int produtoId);
}
