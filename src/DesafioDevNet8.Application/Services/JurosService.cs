using DesafioDevNet8.Application.Interfaces;
using DesafioDevNet8.Domain.Entities;

namespace DesafioDevNet8.Application.Services;

// Service para calcular juros de pagamentos atrasados
public class JurosService : IJurosService
{
    // Taxa aplicada: 2,5% ao dia
    private const decimal TaxaJurosDiaria = 0.025m;

    // Calcula o pagamento com juros incluídos
    public Pagamento CalcularJurosAtraso(decimal valorOriginal, DateTime dataVencimento, DateTime? dataCalculo = null)
    {
        var dataReferencia = dataCalculo ?? DateTime.Now;
        int diasAtraso = CalcularDiasAtraso(dataVencimento, dataReferencia);
        decimal valorJuros = CalcularValorJuros(valorOriginal, diasAtraso);

        var pagamento = new Pagamento
        {
            ValorOriginal = valorOriginal,
            DataVencimento = dataVencimento,
            DiasAtraso = diasAtraso,
            ValorJuros = valorJuros,
            ValorTotal = valorOriginal + valorJuros
        };

        return pagamento;
    }

    // Calcula só o valor dos juros (valor original * 2,5% * dias)
    public decimal CalcularValorJuros(decimal valorOriginal, int diasAtraso)
    {
        if (diasAtraso <= 0)
            return 0;

        return valorOriginal * TaxaJurosDiaria * diasAtraso;
    }

    // Retorna quantos dias se passaram desde o vencimento
    public int CalcularDiasAtraso(DateTime dataVencimento, DateTime? dataCalculo = null)
    {
        var dataReferencia = dataCalculo ?? DateTime.Now;
        var diferenca = (dataReferencia.Date - dataVencimento.Date).Days;
        
        return diferenca > 0 ? diferenca : 0;
    }
}
