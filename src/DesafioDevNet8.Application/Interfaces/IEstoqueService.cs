using DesafioDevNet8.Domain.Entities;

namespace DesafioDevNet8.Application.Interfaces;

/// <summary>
/// Interface para o serviço de movimentação de estoque
/// Define os métodos para registrar entradas e saídas de produtos
/// </summary>
public interface IEstoqueService
{
    /// <summary>
    /// Registra uma entrada de produtos no estoque
    /// Aumenta a quantidade disponível do produto
    /// </summary>
    /// <param name="produtoId">ID do produto</param>
    /// <param name="quantidade">Quantidade a ser adicionada</param>
    /// <param name="descricao">Descrição da entrada</param>
    /// <returns>Movimentação registrada</returns>
    MovimentacaoEstoque RegistrarEntrada(int produtoId, int quantidade, string descricao);

    /// <summary>
    /// Registra uma saída de produtos do estoque
    /// Diminui a quantidade disponível do produto
    /// </summary>
    /// <param name="produtoId">ID do produto</param>
    /// <param name="quantidade">Quantidade a ser removida</param>
    /// <param name="descricao">Descrição da saída</param>
    /// <returns>Movimentação registrada</returns>
    MovimentacaoEstoque RegistrarSaida(int produtoId, int quantidade, string descricao);

    /// <summary>
    /// Obtém o histórico de movimentações de um produto
    /// </summary>
    /// <param name="produtoId">ID do produto</param>
    /// <returns>Lista de movimentações</returns>
    IEnumerable<MovimentacaoEstoque> ObterHistoricoMovimentacoes(int produtoId);

    /// <summary>
    /// Verifica a quantidade atual em estoque de um produto
    /// </summary>
    /// <param name="produtoId">ID do produto</param>
    /// <returns>Quantidade em estoque</returns>
    int VerificarEstoque(int produtoId);
}
