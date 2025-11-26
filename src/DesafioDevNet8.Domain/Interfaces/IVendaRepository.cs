using DesafioDevNet8.Domain.Entities;

namespace DesafioDevNet8.Domain.Interfaces;

/// <summary>
/// Interface para o repositório de vendas
/// Define os métodos de acesso e manipulação de dados de vendas
/// </summary>
public interface IVendaRepository
{
    /// <summary>
    /// Adiciona uma nova venda ao repositório
    /// </summary>
    /// <param name="venda">Venda a ser adicionada</param>
    void Adicionar(Venda venda);

    /// <summary>
    /// Busca uma venda pelo ID
    /// </summary>
    /// <param name="id">ID da venda</param>
    /// <returns>Venda encontrada ou null</returns>
    Venda? ObterPorId(int id);

    /// <summary>
    /// Obtém todas as vendas cadastradas
    /// </summary>
    /// <returns>Lista de vendas</returns>
    IEnumerable<Venda> ObterTodas();

    /// <summary>
    /// Obtém todas as vendas de um vendedor específico
    /// </summary>
    /// <param name="vendedorId">ID do vendedor</param>
    /// <returns>Lista de vendas do vendedor</returns>
    IEnumerable<Venda> ObterPorVendedor(int vendedorId);
}
