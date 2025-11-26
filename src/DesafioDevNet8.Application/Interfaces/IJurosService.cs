using DesafioDevNet8.Domain.Entities;

namespace DesafioDevNet8.Application.Interfaces;

/// <summary>
/// Interface para o serviço de cálculo de juros de atraso
/// Define os métodos para calcular multas por atraso de pagamento
/// </summary>
public interface IJurosService
{
    /// <summary>
    /// Calcula o valor dos juros de atraso baseado na data de vencimento
    /// Regra: Multa de 2,5% ao dia sobre o valor original
    /// </summary>
    /// <param name="valorOriginal">Valor original do pagamento</param>
    /// <param name="dataVencimento">Data de vencimento do pagamento</param>
    /// <param name="dataCalculo">Data para cálculo (padrão: data atual)</param>
    /// <returns>Pagamento com juros calculados</returns>
    Pagamento CalcularJurosAtraso(decimal valorOriginal, DateTime dataVencimento, DateTime? dataCalculo = null);

    /// <summary>
    /// Calcula apenas o valor dos juros sem criar objeto Pagamento
    /// </summary>
    /// <param name="valorOriginal">Valor original</param>
    /// <param name="diasAtraso">Quantidade de dias de atraso</param>
    /// <returns>Valor dos juros</returns>
    decimal CalcularValorJuros(decimal valorOriginal, int diasAtraso);

    /// <summary>
    /// Calcula a quantidade de dias de atraso
    /// </summary>
    /// <param name="dataVencimento">Data de vencimento</param>
    /// <param name="dataCalculo">Data para cálculo (padrão: data atual)</param>
    /// <returns>Quantidade de dias de atraso (0 se não há atraso)</returns>
    int CalcularDiasAtraso(DateTime dataVencimento, DateTime? dataCalculo = null);
}
