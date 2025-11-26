# Desafio Dev em .NET 8

Sistema de gestão desenvolvido em .NET 8 com C# seguindo os princípios de Clean Architecture.

## Descrição

Este projeto implementa três funcionalidades principais:

1. **Cálculo de Comissão de Vendedores**

   - Vendas abaixo de R$100,00: sem comissão
   - Vendas de R$100,00 até R$499,99: 1% de comissão
   - Vendas a partir de R$500,00: 5% de comissão
   - Suporte para processamento em lote via JSON

2. **Controle de Estoque**

   - Registro de movimentações com identificador único (GUID)
   - Entrada e saída de produtos
   - Histórico completo de movimentações
   - Atualização automática de quantidade em estoque

3. **Cálculo de Juros de Atraso**
   - Multa de 2,5% ao dia sobre o valor original
   - Cálculo baseado na data de vencimento
   - Exibição detalhada de dias de atraso e valor total

## Arquitetura

O projeto segue os princípios de **Clean Architecture** com separação em camadas:

```
src/
├── DesafioDevNet8.Domain/          # Camada de Domínio
│   ├── Entities/                   # Entidades do negócio
│   └── Interfaces/                 # Interfaces de repositórios
│
├── DesafioDevNet8.Application/     # Camada de Aplicação
│   ├── Interfaces/                 # Interfaces de serviços
│   └── Services/                   # Implementação dos serviços
│
├── DesafioDevNet8.Infrastructure/  # Camada de Infraestrutura
│   └── Repositories/               # Implementação dos repositórios
│
└── DesafioDevNet8.Presentation/    # Camada de Apresentação
    ├── Program.cs                  # Configuração e inicialização
    └── ConsoleApplication.cs       # Interface do usuário
```

### Camadas

- **Domain**: Contém as entidades de negócio e interfaces de repositórios
- **Application**: Implementa a lógica de negócio através de serviços
- **Infrastructure**: Implementa a persistência de dados (em memória)
- **Presentation**: Interface com o usuário (Console Application)

## Tecnologias Utilizadas

- **.NET 8**: Framework principal
- **C#**: Linguagem de programação
- **Docker**: Containerização da aplicação
- **Docker Compose**: Orquestração de containers
- **Dependency Injection**: Injeção de dependências nativa do .NET
- **Repository Pattern**: Padrão de projeto para acesso a dados
- **Clean Architecture**: Arquitetura em camadas com separação de responsabilidades

## Pré-requisitos

### Para Execução Local

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) instalado

### Para Execução com Docker

- [Docker Desktop](https://www.docker.com/products/docker-desktop/) instalado
- Docker Compose (incluído no Docker Desktop)

## Como Executar

### Opção 1: Docker Compose (Recomendado) 🐳

1. **Clone o repositório:**

```bash
git clone https://github.com/nevesmarcos42/Desafio-Dev-em-.NET-8.git
cd Desafio-Dev-em-.NET-8
```

2. **Build e execute com Docker Compose:**

```bash
docker-compose up --build
```

3. **Para modo interativo (permite interação com o menu):**

```bash
docker-compose run --rm desafio-dev-net8
```

4. **Para parar os containers:**

```bash
docker-compose down
```

### Opção 2: Docker Manual

1. **Build da imagem:**

```bash
docker build -t desafio-dev-net8:latest .
```

2. **Execute o container:**

```bash
docker run -it --rm desafio-dev-net8:latest
```

### Opção 3: Execução Local

1. **Clone o repositório (ou baixe o código):**

```bash
cd Desafio-Dev-em-.NET-8
```

2. **Restaure as dependências:**

```bash
dotnet restore
```

3. **Compile o projeto:**

```bash
dotnet build
```

4. **Execute a aplicação:**

```bash
dotnet run --project src/DesafioDevNet8.Presentation/DesafioDevNet8.Presentation.csproj
```

## Funcionalidades

### Menu Principal

Ao executar a aplicação, você verá o seguinte menu:

```
=== MENU PRINCIPAL ===
1 - Cadastrar Venda e Calcular Comissao
2 - Processar Vendas via JSON
3 - Movimentar Estoque (Entrada/Saida)
4 - Consultar Estoque
5 - Calcular Juros de Atraso
6 - Exibir Relatorios
0 - Sair
```

### 1. Cadastrar Venda

- Selecione um vendedor
- Informe o valor da venda
- O sistema calcula automaticamente a comissão

### 2. Processar Vendas via JSON

Formato JSON esperado:

```json
[
  { "id": 101, "vendedorId": 1, "valor": 89.0 },
  { "id": 102, "vendedorId": 1, "valor": 250.0 },
  { "id": 103, "vendedorId": 2, "valor": 750.0 }
]
```

### 3. Movimentar Estoque

- Selecione um produto
- Escolha o tipo (Entrada ou Saída)
- Informe a quantidade e descrição
- O sistema atualiza o estoque automaticamente

### 4. Consultar Estoque

Exibe todos os produtos com suas quantidades atuais.

### 5. Calcular Juros de Atraso

- Informe o valor original
- Informe a data de vencimento (dd/MM/yyyy)
- O sistema calcula:
  - Dias de atraso
  - Valor dos juros (2,5% ao dia)
  - Valor total a pagar

### 6. Relatórios

- **Relatório de Comissões**: Total de comissões por vendedor
- **Relatório de Estoque**: Quantidade atual de todos os produtos
- **Histórico de Movimentações**: Todas as movimentações de estoque registradas

## Exemplos de Uso

### Exemplo 1: Cadastrar Venda

```
Escolha uma opcao: 1
Digite o ID do vendedor: 1
Digite o valor da venda (R$): 550.00

Resultado:
- Comissao calculada: R$ 27.50 (5% de R$ 550,00)
```

### Exemplo 2: Processar JSON

```json
[
  { "id": 1, "vendedorId": 1, "valor": 80.0 },
  { "id": 2, "vendedorId": 1, "valor": 300.0 },
  { "id": 3, "vendedorId": 2, "valor": 600.0 }
]
```

Resultado:

- Venda 1: R$ 0,00 de comissão (abaixo de R$ 100,00)
- Venda 2: R$ 3,00 de comissão (1% de R$ 300,00)
- Venda 3: R$ 30,00 de comissão (5% de R$ 600,00)

### Exemplo 3: Calcular Juros

```
Valor: R$ 1000,00
Vencimento: 20/11/2025
Data Atual: 26/11/2025

Resultado:
- Dias de atraso: 6
- Juros: R$ 150,00 (2,5% x 6 dias = 15%)
- Total: R$ 1150,00
```

## Boas Práticas Implementadas

### Dependency Injection

- Todos os serviços são injetados via construtor
- Configuração centralizada no `Program.cs`
- Facilita testes e manutenção

### Repository Pattern

- Abstração do acesso a dados
- Facilita mudança de persistência (banco de dados, arquivos, etc.)
- Separação de responsabilidades

### Clean Architecture

- Dependências fluem de fora para dentro
- Domain não depende de nenhuma outra camada
- Facilita testes unitários e manutenção

### Comentários Detalhados

- Cada classe, método e propriedade possui documentação XML
- Explicação clara da lógica de negócio
- Facilita compreensão e manutenção do código

## Estrutura de Dados

### Entidades Principais

#### Vendedor

```csharp
{
    Id: int,
    Nome: string,
    ComissaoTotal: decimal
}
```

#### Venda

```csharp
{
    Id: int,
    VendedorId: int,
    Valor: decimal,
    Comissao: decimal,
    DataVenda: DateTime
}
```

#### Produto

```csharp
{
    Id: int,
    Nome: string,
    Descricao: string,
    QuantidadeEstoque: int
}
```

#### MovimentacaoEstoque

```csharp
{
    Id: Guid,
    ProdutoId: int,
    TipoMovimentacao: string,
    Quantidade: int,
    Descricao: string,
    DataMovimentacao: DateTime,
    QuantidadeAposMovimentacao: int
}
```

#### Pagamento

```csharp
{
    Id: int,
    Descricao: string,
    ValorOriginal: decimal,
    DataVencimento: DateTime,
    ValorJuros: decimal,
    ValorTotal: decimal,
    DiasAtraso: int
}
```

## Dados de Exemplo

O sistema inicializa com dados de exemplo:

**Vendedores:**

- João Silva (ID: 1)
- Maria Santos (ID: 2)
- Pedro Oliveira (ID: 3)

**Produtos:**

- Notebook Dell (ID: 1) - Estoque: 10
- Mouse Logitech (ID: 2) - Estoque: 50
- Teclado Mecânico (ID: 3) - Estoque: 25

## Observações

- Os dados são armazenados em memória e serão perdidos ao fechar a aplicação
- Para persistência real, seria necessário implementar repositórios com banco de dados
- O sistema valida entradas e exibe mensagens de erro apropriadas
- Todas as datas seguem o formato brasileiro (dd/MM/yyyy)
- Valores monetários são exibidos com 2 casas decimais

## Autor

Desenvolvido como desafio técnico de .NET 8 com Clean Architecture.

## Licença

Este projeto é de código aberto e pode ser utilizado livremente para fins educacionais.
