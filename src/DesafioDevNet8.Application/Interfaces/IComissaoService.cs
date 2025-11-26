using DesafioDevNet8.Domain.Entities;

namespace DesafioDevNet8.Application.Interfaces;

/// <summary>
/// Interface para o serviço de cálculo de comissões de vendedores
/// Define os métodos para processar vendas e calcular comissões
/// </summary>
public interface IComissaoService
{
    /// <summary>
    /// Calcula a comissão de uma venda baseado nas regras de negócio
    /// Regras:
    /// - Vendas abaixo de R$100,00: 0% de comissão
    /// - Vendas abaixo de R$500,00: 1% de comissão
    /// - Vendas a partir de R$500,00: 5% de comissão
    /// </summary>
    /// <param name="valorVenda">Valor da venda</param>
    /// <returns>Valor da comissão calculada</returns>
    decimal CalcularComissao(decimal valorVenda);

    /// <summary>
    /// Processa uma venda, calculando a comissão e atualizando o total do vendedor
    /// </summary>
    /// <param name="venda">Venda a ser processada</param>
    void ProcessarVenda(Venda venda);

    /// <summary>
    /// Processa uma lista de vendas em formato JSON
    /// </summary>
    /// <param name="jsonVendas">JSON contendo as vendas</param>
    void ProcessarVendasJson(string jsonVendas);

    /// <summary>
    /// Obtém o relatório de comissões de todos os vendedores
    /// </summary>
    /// <returns>Dicionário com ID do vendedor e total de comissões</returns>
    Dictionary<int, decimal> ObterRelatorioComissoes();
}
