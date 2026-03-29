# CartaoCreditoValido

Projeto .NET 8 para validação de cartões de crédito, utilizando arquitetura em camadas.

## Estrutura do Projeto

- **CartaoCreditoValido.Domain** - Camada de domínio com entidades e regras de negócio
- **CartaoCreditoValido.Application** - Camada de aplicação com serviços e lógica de negócio
- **CartaoCreditoValido.Infra** - Camada de infraestrutura com repositórios e acesso a dados
- **CartaoCreditoValido.WebAPI** - API Web ASP.NET Core com endpoints
- **CartaoCreditoValido.Tests** - Testes automatizados (NUnit)

## Requisitos

- .NET 8.0 ou superior instalado

## Como executar

### Compilar a solução

```bash
# Compilar todos os projetos
dotnet build
```

### Executar a aplicação

```bash
# Executar a WebAPI
dotnet run --project CartaoCreditoValido.WebAPI
```

A API estará disponível em:
- HTTP: `http://localhost:5002`
- HTTPS: `https://localhost:5001`
- Swagger: `https://localhost:5001/swagger` (abre automaticamente no navegador)

### Executar os testes

```bash
# Executar todos os testes
dotnet test
```

### Comandos úteis

```bash
# Limpar e reconstruir
dotnet clean && dotnet build

# Executar em modo watch (reinicia automaticamente)
dotnet watch run --project CartaoCreditoValido.WebAPI

# Executar testes em modo watch
dotnet watch test
```
