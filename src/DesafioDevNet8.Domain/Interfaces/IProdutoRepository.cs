using DesafioDevNet8.Domain.Entities;

namespace DesafioDevNet8.Domain.Interfaces;

/// <summary>
/// Interface para o repositório de produtos
/// Define os métodos de acesso e manipulação de dados de produtos
/// </summary>
public interface IProdutoRepository
{
    /// <summary>
    /// Adiciona um novo produto ao repositório
    /// </summary>
    /// <param name="produto">Produto a ser adicionado</param>
    void Adicionar(Produto produto);

    /// <summary>
    /// Busca um produto pelo ID
    /// </summary>
    /// <param name="id">ID do produto</param>
    /// <returns>Produto encontrado ou null</returns>
    Produto? ObterPorId(int id);

    /// <summary>
    /// Obtém todos os produtos cadastrados
    /// </summary>
    /// <returns>Lista de produtos</returns>
    IEnumerable<Produto> ObterTodos();

    /// <summary>
    /// Atualiza os dados de um produto
    /// </summary>
    /// <param name="produto">Produto com dados atualizados</param>
    void Atualizar(Produto produto);
}
