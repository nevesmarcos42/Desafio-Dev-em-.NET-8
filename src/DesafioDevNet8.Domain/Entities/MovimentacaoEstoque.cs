namespace DesafioDevNet8.Domain.Entities;

// Registra cada movimentação de estoque (entrada ou saída)
public class MovimentacaoEstoque
{
    // Usamos GUID para garantir que cada movimentação tenha um ID único
    public Guid Id { get; set; }
    
    public int ProdutoId { get; set; }
    public string TipoMovimentacao { get; set; } = string.Empty; // "Entrada" ou "Saída"
    public int Quantidade { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataMovimentacao { get; set; }
    
    // Armazena quanto ficou em estoque após essa movimentação
    public int QuantidadeAposMovimentacao { get; set; }

    public MovimentacaoEstoque() 
    {
        Id = Guid.NewGuid();
        DataMovimentacao = DateTime.Now;
    }

    public MovimentacaoEstoque(int produtoId, string tipoMovimentacao, int quantidade, string descricao)
    {
        Id = Guid.NewGuid();
        ProdutoId = produtoId;
        TipoMovimentacao = tipoMovimentacao;
        Quantidade = quantidade;
        Descricao = descricao;
        DataMovimentacao = DateTime.Now;
    }
}
