# Guia de Execução com Docker

Este guia fornece instruções detalhadas para executar o projeto usando Docker e Docker Compose.

## Requisitos

- Docker Desktop instalado e em execução
- Docker Compose (incluído no Docker Desktop)

## Estrutura Docker

O projeto inclui:

- **Dockerfile**: Imagem multi-stage otimizada para .NET 8
- **docker-compose.yml**: Orquestração do container
- **.dockerignore**: Otimização do build excluindo arquivos desnecessários

## Comandos Principais

### Build e Execução

```bash
# Build e inicia o container
docker-compose up --build

# Build sem iniciar
docker-compose build

# Inicia sem rebuild
docker-compose up
```

### Modo Interativo

Para interagir com o menu da aplicação:

```bash
docker-compose run --rm desafio-dev-net8
```

### Gerenciamento

```bash
# Para os containers
docker-compose down

# Para e remove volumes
docker-compose down -v

# Ver logs
docker-compose logs

# Ver logs em tempo real
docker-compose logs -f
```

### Comandos Docker Manual

```bash
# Build da imagem
docker build -t desafio-dev-net8:latest .

# Executar container
docker run -it --rm desafio-dev-net8:latest

# Executar com volume para dados
docker run -it --rm -v $(pwd)/data:/app/data desafio-dev-net8:latest

# Listar imagens
docker images | grep desafio

# Remover imagem
docker rmi desafio-dev-net8:latest
```

## Estrutura do Dockerfile

O Dockerfile utiliza multi-stage build:

1. **Build Stage**: Restaura dependências e compila o projeto
2. **Publish Stage**: Publica os artefatos otimizados
3. **Runtime Stage**: Imagem final mínima apenas com runtime

## Configurações

### Variáveis de Ambiente

No `docker-compose.yml`, você pode configurar:

```yaml
environment:
  - DOTNET_ENVIRONMENT=Production
```

### Volumes

Para persistir dados:

```yaml
volumes:
  - ./data:/app/data
```

### Rede

O projeto usa uma rede bridge customizada:

```yaml
networks:
  desafio-network:
    driver: bridge
```

## Solução de Problemas

### Container não inicia

```bash
# Verificar logs
docker-compose logs

# Rebuildar do zero
docker-compose down
docker-compose build --no-cache
docker-compose up
```

### Porta em uso

Se houver conflito de portas, modifique o `docker-compose.yml`:

```yaml
ports:
  - "8080:80"  # Ajuste conforme necessário
```

### Limpeza completa

```bash
# Remove containers, imagens e volumes
docker-compose down -v --rmi all

# Limpa sistema Docker
docker system prune -a
```

## Performance

### Otimizações Implementadas

- Multi-stage build reduz tamanho final da imagem
- .dockerignore evita copiar arquivos desnecessários
- Uso de imagem runtime mínima (sem SDK)
- Cache de layers otimizado

### Tamanhos Esperados

- Imagem build: ~500-700 MB
- Imagem final: ~200-250 MB

## Dicas

1. Use sempre `--build` após mudanças no código
2. Para desenvolvimento, considere volumes para hot-reload
3. Em produção, use imagens versionadas
4. Monitore logs com `docker-compose logs -f`

## Exemplos de Uso

### Desenvolvimento

```bash
# Rebuild rápido durante desenvolvimento
docker-compose up --build -d
docker-compose logs -f
```

### Testes

```bash
# Executa uma vez e remove container
docker-compose run --rm desafio-dev-net8
```

### Produção

```bash
# Build com tag de versão
docker build -t desafio-dev-net8:1.0.0 .
docker tag desafio-dev-net8:1.0.0 desafio-dev-net8:latest

# Deploy
docker-compose -f docker-compose.prod.yml up -d
```
