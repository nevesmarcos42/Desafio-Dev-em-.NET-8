using DesafioDevNet8.Domain.Entities;

namespace DesafioDevNet8.Domain.Interfaces;

/// <summary>
/// Interface para o repositório de vendedores
/// Define os métodos de acesso e manipulação de dados de vendedores
/// </summary>
public interface IVendedorRepository
{
    /// <summary>
    /// Adiciona um novo vendedor ao repositório
    /// </summary>
    /// <param name="vendedor">Vendedor a ser adicionado</param>
    void Adicionar(Vendedor vendedor);

    /// <summary>
    /// Busca um vendedor pelo ID
    /// </summary>
    /// <param name="id">ID do vendedor</param>
    /// <returns>Vendedor encontrado ou null</returns>
    Vendedor? ObterPorId(int id);

    /// <summary>
    /// Obtém todos os vendedores cadastrados
    /// </summary>
    /// <returns>Lista de vendedores</returns>
    IEnumerable<Vendedor> ObterTodos();

    /// <summary>
    /// Atualiza os dados de um vendedor
    /// </summary>
    /// <param name="vendedor">Vendedor com dados atualizados</param>
    void Atualizar(Vendedor vendedor);
}
