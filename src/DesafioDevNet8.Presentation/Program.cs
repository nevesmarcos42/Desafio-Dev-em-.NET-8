using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DesafioDevNet8.Application.Interfaces;
using DesafioDevNet8.Application.Services;
using DesafioDevNet8.Domain.Interfaces;
using DesafioDevNet8.Infrastructure.Repositories;
using DesafioDevNet8.Presentation;

// Configura injeção de dependência
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Repositórios (Singleton mantém os dados durante toda a execução)
        services.AddSingleton<IVendedorRepository, VendedorRepository>();
        services.AddSingleton<IVendaRepository, VendaRepository>();
        services.AddSingleton<IProdutoRepository, ProdutoRepository>();
        services.AddSingleton<IMovimentacaoEstoqueRepository, MovimentacaoEstoqueRepository>();
        services.AddSingleton<IPagamentoRepository, PagamentoRepository>();

        // Serviços de aplicação
        services.AddScoped<IComissaoService, ComissaoService>();
        services.AddScoped<IEstoqueService, EstoqueService>();
        services.AddScoped<IJurosService, JurosService>();

        services.AddScoped<ConsoleApplication>();
    })
    .Build();

var app = host.Services.GetRequiredService<ConsoleApplication>();
await app.ExecutarAsync();
