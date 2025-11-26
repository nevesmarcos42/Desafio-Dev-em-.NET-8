using DesafioDevNet8.Domain.Entities;
using DesafioDevNet8.Domain.Interfaces;

namespace DesafioDevNet8.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private static readonly List<Produto> _produtos = new();

    public void Adicionar(Produto produto)
    {
        _produtos.Add(produto);
    }

    public Produto? ObterPorId(int id)
    {
        return _produtos.FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<Produto> ObterTodos()
    {
        return _produtos.AsEnumerable();
    }

    public void Atualizar(Produto produto)
    {
        var produtoExistente = ObterPorId(produto.Id);
        if (produtoExistente != null)
        {
            produtoExistente.Nome = produto.Nome;
            produtoExistente.Descricao = produto.Descricao;
            produtoExistente.QuantidadeEstoque = produto.QuantidadeEstoque;
        }
    }
}
