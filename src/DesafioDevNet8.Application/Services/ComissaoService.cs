using System.Text.Json;
using DesafioDevNet8.Application.Interfaces;
using DesafioDevNet8.Domain.Entities;
using DesafioDevNet8.Domain.Interfaces;

namespace DesafioDevNet8.Application.Services;

// Service responsável por calcular comissões de vendas
public class ComissaoService : IComissaoService
{
    private readonly IVendaRepository _vendaRepository;
    private readonly IVendedorRepository _vendedorRepository;

    public ComissaoService(IVendaRepository vendaRepository, IVendedorRepository vendedorRepository)
    {
        _vendaRepository = vendaRepository;
        _vendedorRepository = vendedorRepository;
    }

    // Calcula a comissão com base no valor da venda
    // Regras: < R$100 = 0%, R$100-499 = 1%, >= R$500 = 5%
    public decimal CalcularComissao(decimal valorVenda)
    {
        if (valorVenda < 100)
            return 0;
        
        if (valorVenda < 500)
            return valorVenda * 0.01m; // 1% para vendas entre 100 e 499
        
        return valorVenda * 0.05m; // 5% para vendas >= 500
    }

    // Processa uma venda: calcula comissão e atualiza o vendedor
    public void ProcessarVenda(Venda venda)
    {
        venda.Comissao = CalcularComissao(venda.Valor);
        _vendaRepository.Adicionar(venda);

        // Atualiza o total de comissões do vendedor
        var vendedor = _vendedorRepository.ObterPorId(venda.VendedorId);
        if (vendedor != null)
        {
            vendedor.ComissaoTotal += venda.Comissao;
            _vendedorRepository.Atualizar(vendedor);
        }
    }

    // Processa várias vendas de uma vez a partir de um JSON
    public void ProcessarVendasJson(string jsonVendas)
    {
        try
        {
            var vendas = JsonSerializer.Deserialize<List<Venda>>(jsonVendas);

            if (vendas != null)
            {
                foreach (var venda in vendas)
                {
                    ProcessarVenda(venda);
                }
            }
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException("Erro ao processar JSON de vendas: " + ex.Message, ex);
        }
    }

    // Retorna o total de comissões de cada vendedor
    public Dictionary<int, decimal> ObterRelatorioComissoes()
    {
        var relatorio = new Dictionary<int, decimal>();

        var vendedores = _vendedorRepository.ObterTodos();
        foreach (var vendedor in vendedores)
        {
            relatorio[vendedor.Id] = vendedor.ComissaoTotal;
        }

        return relatorio;
    }
}
