using DesafioDevNet8.Application.Interfaces;
using DesafioDevNet8.Domain.Entities;
using DesafioDevNet8.Domain.Interfaces;
using System.Text.Json;

namespace DesafioDevNet8.Presentation;

// Aplicação principal que gerencia o menu interativo
public class ConsoleApplication
{
    private readonly IComissaoService _comissaoService;
    private readonly IEstoqueService _estoqueService;
    private readonly IJurosService _jurosService;
    private readonly IVendedorRepository _vendedorRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IPagamentoRepository _pagamentoRepository;

    public ConsoleApplication(
        IComissaoService comissaoService,
        IEstoqueService estoqueService,
        IJurosService jurosService,
        IVendedorRepository vendedorRepository,
        IProdutoRepository produtoRepository,
        IPagamentoRepository pagamentoRepository)
    {
        _comissaoService = comissaoService;
        _estoqueService = estoqueService;
        _jurosService = jurosService;
        _vendedorRepository = vendedorRepository;
        _produtoRepository = produtoRepository;
        _pagamentoRepository = pagamentoRepository;
    }

    // Inicia a aplicação e exibe o menu principal
    public async Task ExecutarAsync()
    {
        InicializarDadosExemplo();

        Console.WriteLine("==============================================");
        Console.WriteLine("  SISTEMA DE GESTAO - DESAFIO DEV .NET 8");
        Console.WriteLine("==============================================");
        Console.WriteLine();

        bool continuar = true;

        while (continuar)
        {
            ExibirMenu();
            var opcao = Console.ReadLine();

            Console.WriteLine();

            switch (opcao)
            {
                case "1":
                    await ProcessarComissoesAsync();
                    break;
                case "2":
                    await ProcessarVendasJsonAsync();
                    break;
                case "3":
                    await MovimentarEstoqueAsync();
                    break;
                case "4":
                    await ConsultarEstoqueAsync();
                    break;
                case "5":
                    await CalcularJurosAsync();
                    break;
                case "6":
                    await ExibirRelatoriosAsync();
                    break;
                case "0":
                    continuar = false;
                    Console.WriteLine("Encerrando o sistema. Ate logo!");
                    break;
                default:
                    Console.WriteLine("Opcao invalida! Tente novamente.");
                    break;
            }

            if (continuar)
            {
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        await Task.CompletedTask;
    }

    private void ExibirMenu()
    {
        Console.WriteLine("\n=== MENU PRINCIPAL ===");
        Console.WriteLine("1 - Cadastrar Venda e Calcular Comissao");
        Console.WriteLine("2 - Processar Vendas via JSON");
        Console.WriteLine("3 - Movimentar Estoque (Entrada/Saida)");
        Console.WriteLine("4 - Consultar Estoque");
        Console.WriteLine("5 - Calcular Juros de Atraso");
        Console.WriteLine("6 - Exibir Relatorios");
        Console.WriteLine("0 - Sair");
        Console.Write("\nEscolha uma opcao: ");
    }

    private async Task ProcessarComissoesAsync()
    {
        Console.WriteLine("=== CADASTRAR VENDA E CALCULAR COMISSAO ===\n");

        // Exibe vendedores disponíveis
        var vendedores = _vendedorRepository.ObterTodos();
        Console.WriteLine("Vendedores disponiveis:");
        foreach (var v in vendedores)
        {
            Console.WriteLine($"  ID: {v.Id} - Nome: {v.Nome} - Comissao Total: R$ {v.ComissaoTotal:F2}");
        }

        Console.Write("\nDigite o ID do vendedor: ");
        if (!int.TryParse(Console.ReadLine(), out int vendedorId))
        {
            Console.WriteLine("ID invalido!");
            return;
        }

        Console.Write("Digite o valor da venda (R$): ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal valor))
        {
            Console.WriteLine("Valor invalido!");
            return;
        }

        // Cria e processa a venda
        var venda = new Venda
        {
            Id = Random.Shared.Next(1000, 9999),
            VendedorId = vendedorId,
            Valor = valor,
            DataVenda = DateTime.Now
        };

        _comissaoService.ProcessarVenda(venda);

        // Exibe o resultado
        Console.WriteLine($"\nVenda cadastrada com sucesso!");
        Console.WriteLine($"ID da Venda: {venda.Id}");
        Console.WriteLine($"Valor: R$ {venda.Valor:F2}");
        Console.WriteLine($"Comissao calculada: R$ {venda.Comissao:F2}");

        var vendedor = _vendedorRepository.ObterPorId(vendedorId);
        if (vendedor != null)
        {
            Console.WriteLine($"Nova comissao total do vendedor: R$ {vendedor.ComissaoTotal:F2}");
        }

        await Task.CompletedTask;
    }

    // Processa múltiplas vendas de uma vez usando JSON
    private async Task ProcessarVendasJsonAsync()
    {
        Console.WriteLine("=== PROCESSAR VENDAS VIA JSON ===\n");

        Console.WriteLine("Formato esperado:");
        Console.WriteLine("[{\"id\": 1, \"vendedorId\": 1, \"valor\": 150.00}, ...]");
        Console.WriteLine("\nDigite o JSON de vendas (ou deixe em branco para usar exemplo):");
        
        var jsonInput = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(jsonInput))
        {
            // JSON de exemplo
            jsonInput = @"[
                {""id"": 101, ""vendedorId"": 1, ""valor"": 89.00},
                {""id"": 102, ""vendedorId"": 1, ""valor"": 250.00},
                {""id"": 103, ""vendedorId"": 2, ""valor"": 750.00},
                {""id"": 104, ""vendedorId"": 3, ""valor"": 450.00},
                {""id"": 105, ""vendedorId"": 2, ""valor"": 1200.00}
            ]";
            Console.WriteLine("\nUsando JSON de exemplo...");
        }

        try
        {
            _comissaoService.ProcessarVendasJson(jsonInput);
            Console.WriteLine("\nVendas processadas com sucesso!");
            
            // Exibe resumo
            var vendedores = _vendedorRepository.ObterTodos();
            Console.WriteLine("\nResumo de comissoes:");
            foreach (var v in vendedores)
            {
                Console.WriteLine($"  {v.Nome}: R$ {v.ComissaoTotal:F2}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao processar JSON: {ex.Message}");
        }

        await Task.CompletedTask;
    }

    private async Task MovimentarEstoqueAsync()
    {
        Console.WriteLine("=== MOVIMENTAR ESTOQUE ===\n");

        // Exibe produtos disponíveis
        var produtos = _produtoRepository.ObterTodos();
        Console.WriteLine("Produtos disponiveis:");
        foreach (var p in produtos)
        {
            Console.WriteLine($"  ID: {p.Id} - Nome: {p.Nome} - Estoque Atual: {p.QuantidadeEstoque}");
        }

        Console.Write("\nDigite o ID do produto: ");
        if (!int.TryParse(Console.ReadLine(), out int produtoId))
        {
            Console.WriteLine("ID invalido!");
            return;
        }

        Console.Write("Tipo de movimentacao (1-Entrada / 2-Saida): ");
        var tipo = Console.ReadLine();

        Console.Write("Quantidade: ");
        if (!int.TryParse(Console.ReadLine(), out int quantidade))
        {
            Console.WriteLine("Quantidade invalida!");
            return;
        }

        Console.Write("Descricao da movimentacao: ");
        var descricao = Console.ReadLine() ?? "";

        try
        {
            MovimentacaoEstoque movimentacao;

            if (tipo == "1")
            {
                movimentacao = _estoqueService.RegistrarEntrada(produtoId, quantidade, descricao);
            }
            else if (tipo == "2")
            {
                movimentacao = _estoqueService.RegistrarSaida(produtoId, quantidade, descricao);
            }
            else
            {
                Console.WriteLine("Tipo de movimentacao invalido!");
                return;
            }

            // Exibe resultado
            Console.WriteLine($"\nMovimentacao registrada com sucesso!");
            Console.WriteLine($"ID da Movimentacao: {movimentacao.Id}");
            Console.WriteLine($"Tipo: {movimentacao.TipoMovimentacao}");
            Console.WriteLine($"Quantidade: {movimentacao.Quantidade}");
            Console.WriteLine($"Estoque apos movimentacao: {movimentacao.QuantidadeAposMovimentacao}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }

        await Task.CompletedTask;
    }

    private async Task ConsultarEstoqueAsync()
    {
        Console.WriteLine("=== CONSULTAR ESTOQUE ===\n");

        var produtos = _produtoRepository.ObterTodos();
        Console.WriteLine("Produtos cadastrados:\n");
        
        foreach (var p in produtos)
        {
            Console.WriteLine($"ID: {p.Id}");
            Console.WriteLine($"Nome: {p.Nome}");
            Console.WriteLine($"Descricao: {p.Descricao}");
            Console.WriteLine($"Quantidade em Estoque: {p.QuantidadeEstoque}");
            Console.WriteLine("---");
        }

        await Task.CompletedTask;
    }

    private async Task CalcularJurosAsync()
    {
        Console.WriteLine("=== CALCULAR JUROS DE ATRASO ===\n");

        Console.Write("Digite o valor original (R$): ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal valor))
        {
            Console.WriteLine("Valor invalido!");
            return;
        }

        Console.Write("Digite a data de vencimento (dd/MM/yyyy): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime dataVencimento))
        {
            Console.WriteLine("Data invalida!");
            return;
        }

        // Calcula os juros
        var pagamento = _jurosService.CalcularJurosAtraso(valor, dataVencimento);

        // Exibe resultado
        Console.WriteLine($"\n=== RESULTADO DO CALCULO ===");
        Console.WriteLine($"Valor Original: R$ {pagamento.ValorOriginal:F2}");
        Console.WriteLine($"Data de Vencimento: {pagamento.DataVencimento:dd/MM/yyyy}");
        Console.WriteLine($"Data Atual: {DateTime.Now:dd/MM/yyyy}");
        Console.WriteLine($"Dias de Atraso: {pagamento.DiasAtraso}");
        Console.WriteLine($"Valor dos Juros (2,5% ao dia): R$ {pagamento.ValorJuros:F2}");
        Console.WriteLine($"Valor Total a Pagar: R$ {pagamento.ValorTotal:F2}");

        await Task.CompletedTask;
    }

    private async Task ExibirRelatoriosAsync()
    {
        Console.WriteLine("=== RELATORIOS ===\n");

        Console.WriteLine("1 - Relatorio de Comissoes");
        Console.WriteLine("2 - Relatorio de Estoque");
        Console.WriteLine("3 - Historico de Movimentacoes");
        Console.Write("\nEscolha: ");
        
        var opcao = Console.ReadLine();

        Console.WriteLine();

        switch (opcao)
        {
            case "1":
                ExibirRelatorioComissoes();
                break;
            case "2":
                await ConsultarEstoqueAsync();
                break;
            case "3":
                ExibirHistoricoMovimentacoes();
                break;
            default:
                Console.WriteLine("Opcao invalida!");
                break;
        }

        await Task.CompletedTask;
    }

    private void ExibirRelatorioComissoes()
    {
        Console.WriteLine("=== RELATORIO DE COMISSOES POR VENDEDOR ===\n");

        var vendedores = _vendedorRepository.ObterTodos();
        decimal totalGeral = 0;

        foreach (var vendedor in vendedores)
        {
            Console.WriteLine($"Vendedor: {vendedor.Nome} (ID: {vendedor.Id})");
            Console.WriteLine($"Comissao Total: R$ {vendedor.ComissaoTotal:F2}");
            Console.WriteLine("---");
            totalGeral += vendedor.ComissaoTotal;
        }

        Console.WriteLine($"\nTOTAL GERAL DE COMISSOES: R$ {totalGeral:F2}");
    }

    private void ExibirHistoricoMovimentacoes()
    {
        Console.WriteLine("=== HISTORICO DE MOVIMENTACOES ===\n");

        var produtos = _produtoRepository.ObterTodos();
        
        foreach (var produto in produtos)
        {
            var movimentacoes = _estoqueService.ObterHistoricoMovimentacoes(produto.Id);
            
            if (movimentacoes.Any())
            {
                Console.WriteLine($"Produto: {produto.Nome} (ID: {produto.Id})");
                Console.WriteLine($"Estoque Atual: {produto.QuantidadeEstoque}\n");
                
                foreach (var mov in movimentacoes)
                {
                    Console.WriteLine($"  [{mov.DataMovimentacao:dd/MM/yyyy HH:mm}]");
                    Console.WriteLine($"  Tipo: {mov.TipoMovimentacao}");
                    Console.WriteLine($"  Quantidade: {mov.Quantidade}");
                    Console.WriteLine($"  Descricao: {mov.Descricao}");
                    Console.WriteLine($"  Estoque apos: {mov.QuantidadeAposMovimentacao}");
                    Console.WriteLine();
                }
                Console.WriteLine("---");
            }
        }
    }

    // Cria alguns dados de exemplo para testar o sistema
    private void InicializarDadosExemplo()
    {
        _vendedorRepository.Adicionar(new Vendedor(1, "João Silva"));
        _vendedorRepository.Adicionar(new Vendedor(2, "Maria Santos"));
        _vendedorRepository.Adicionar(new Vendedor(3, "Pedro Oliveira"));

        _produtoRepository.Adicionar(new Produto(1, "Notebook Dell", "Notebook Dell Inspiron 15", 10));
        _produtoRepository.Adicionar(new Produto(2, "Mouse Logitech", "Mouse sem fio Logitech MX", 50));
        _produtoRepository.Adicionar(new Produto(3, "Teclado Mecânico", "Teclado mecânico RGB", 25));
    }
}
