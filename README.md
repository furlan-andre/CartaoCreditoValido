# CartaoCreditoValido

Projeto .NET 8 para validação de cartões de crédito, utilizando arquitetura em camadas, RabbitMQ para comunicação assíncrona e XUnit para testes automatizados.


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

## Endpoints da API

### `POST /CartaoCredito`

Cria um cartão de crédito.

Parâmetros de entrada (body JSON):

```json
{
  "nomeCompletoTitular": "string",
  "nascimentoTitular": "yyyy-MM-dd",
  "numeroCartao": 1111111111111111
}
```

Possíveis retornos:

- `201 Created`
  - Header `Location`: `http://localhost:8080/CartaoCredito/{id}`
  - Body:

```json
{
  "id": 1,
  "nomeCompletoTitular": "Andre Teste",
  "nascimentoTitular": "1990-05-10",
  "numeroCartao": 1111111111111111
}
```

- `400 Bad Request` (validações de entrada ou regra de domínio)
  - Formato para erro de validação:

```json
{
  "messages": [
	"O número do cartão é obrigatório."
  ]
}
```

  - Formato para exceção de domínio:

```json
{
  "message": "Mensagem da regra de domínio"
}
```

- `500 Internal Server Error`

```json
{
  "message": "Mensagem do erro"
}
```

### `GET /CartaoCredito/{id}`

Obtém um cartão de crédito por identificador.

Parâmetros de entrada:

- `id` (path): número inteiro (`long`)

Possíveis retornos:

- `200 OK`

```json
{
  "id": 1,
  "nomeCompletoTitular": "Andre Teste",
  "nascimentoTitular": "1990-05-10",
  "numeroCartao": 4111111111111111
}
```

- `404 Not Found` (quando não existir registro para o `id` informado)
- `500 Internal Server Error`

```json
{
  "message": "Mensagem do erro"
}
```

