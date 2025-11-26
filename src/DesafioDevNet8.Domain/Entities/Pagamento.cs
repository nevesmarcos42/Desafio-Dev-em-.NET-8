namespace DesafioDevNet8.Domain.Entities;

// Representa um pagamento que pode ter juros por atraso
public class Pagamento
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal ValorOriginal { get; set; }
    public DateTime DataVencimento { get; set; }
    
    // Campos calculados quando há atraso
    public decimal ValorJuros { get; set; }
    public decimal ValorTotal { get; set; }
    public int DiasAtraso { get; set; }

    public Pagamento() { }

    public Pagamento(int id, string descricao, decimal valorOriginal, DateTime dataVencimento)
    {
        Id = id;
        Descricao = descricao;
        ValorOriginal = valorOriginal;
        DataVencimento = dataVencimento;
        ValorJuros = 0;
        ValorTotal = valorOriginal;
        DiasAtraso = 0;
    }
}
