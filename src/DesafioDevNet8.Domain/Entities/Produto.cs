namespace DesafioDevNet8.Domain.Entities;

// Produto cadastrado no sistema
public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    
    // Quantidade disponível no estoque
    public int QuantidadeEstoque { get; set; }

    public Produto() { }

    public Produto(int id, string nome, string descricao, int quantidadeInicial)
    {
        Id = id;
        Nome = nome;
        Descricao = descricao;
        QuantidadeEstoque = quantidadeInicial;
    }
}
