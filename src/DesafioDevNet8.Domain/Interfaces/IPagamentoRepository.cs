using DesafioDevNet8.Domain.Entities;

namespace DesafioDevNet8.Domain.Interfaces;

/// <summary>
/// Interface para o repositório de pagamentos
/// Define os métodos de acesso e manipulação de dados de pagamentos
/// </summary>
public interface IPagamentoRepository
{
    /// <summary>
    /// Adiciona um novo pagamento ao repositório
    /// </summary>
    /// <param name="pagamento">Pagamento a ser adicionado</param>
    void Adicionar(Pagamento pagamento);

    /// <summary>
    /// Busca um pagamento pelo ID
    /// </summary>
    /// <param name="id">ID do pagamento</param>
    /// <returns>Pagamento encontrado ou null</returns>
    Pagamento? ObterPorId(int id);

    /// <summary>
    /// Obtém todos os pagamentos cadastrados
    /// </summary>
    /// <returns>Lista de pagamentos</returns>
    IEnumerable<Pagamento> ObterTodos();

    /// <summary>
    /// Atualiza os dados de um pagamento
    /// </summary>
    /// <param name="pagamento">Pagamento com dados atualizados</param>
    void Atualizar(Pagamento pagamento);
}
