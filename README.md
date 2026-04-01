# CartaoCreditoValido

Projeto .NET 8 para validação de cartões de crédito, utilizando arquitetura em camadas, DDD, SQL Server, RabbitMQ para comunicação assíncrona e XUnit para testes automatizados.


> A execução da API é suportada somente via Docker.

## Estrutura do Projeto

- **CartaoCreditoValido.Domain** - Camada de domínio com entidades e regras de negócio
- **CartaoCreditoValido.Application** - Camada de aplicação com serviços e lógica de negócio
- **CartaoCreditoValido.Infra** - Camada de infraestrutura com repositórios e acesso a dados
- **CartaoCreditoValido.WebAPI** - API Web ASP.NET Core com endpoints
- **CartaoCreditoValido.Tests** - Testes automatizados (NUnit)

## Requisitos

- Docker instalado e em execução

## Como executar

Execute os comandos abaixo a partir da raiz da solução.

### Subir a aplicação

```powershell
docker compose up --build
```

A API estará disponível em:
- HTTP: `http://localhost:8080`
- Swagger: `http://localhost:8080/swagger`

Serviços auxiliares disponíveis:
- RabbitMQ Management: `http://localhost:15672`
