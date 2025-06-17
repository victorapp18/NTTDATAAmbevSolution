# NTTDATAAmbevSolution – API de Vendas

Este projeto é uma **API para gerenciamento de vendas**, desenvolvida com **.NET 8** utilizando arquitetura em camadas, princípios **SOLID**, **DDD** e boas práticas modernas. O objetivo é simular o registro e controle de vendas com regras reais de negócio.

---

## Objetivo do Projeto

Implementar uma API RESTful capaz de registrar vendas contendo:

- Número da venda  
- Data  
- Cliente (ID e nome - simulado)  
- Filial (ID e nome - simulado)  
- Lista de produtos (com quantidade, preço unitário e desconto)  
- Cálculo automático do total  
- Cancelamento de venda e itens  
- Aplicação de regras de desconto por quantidade  

Além disso, eventos como `SaleCreated`, `SaleModified`, `SaleCancelled` e `ItemCancelled` são **simulados via logs**.

---

## Tecnologias Utilizadas

- .NET 8  
- C#  
- ASP.NET Core Web API  
- Docker e Docker Hub  
- Swagger (Swashbuckle)  
- Arquitetura em Camadas com DDD  
- Repositório In-Memory  

---

## Regras de Negócio Atendidas

| Quantidade | Desconto Aplicado | Observações |
|------------|--------------------|-------------|
| < 4 itens  | **0%**             | Desconto não permitido |
| 4 a 9      | **10%**            | Aplicado automaticamente |
| 10 a 20    | **20%**            | Aplicado automaticamente |
| > 20       | **Rejeitado**      | Venda não permitida |

---

## Como Executar o Projeto

### Usando Docker (recomendado)

> A imagem já está disponível no Docker Hub: [`victorapp18/nttambev`](https://hub.docker.com/r/victorapp18/nttambev)

1. Clone o repositório:

```bash
git clone https://github.com/victorapp18/NTTDATAAmbevSolution.git
cd NTTDATAAmbevSolution
```

2. Construa e execute com Docker Compose:

```bash
docker-compose up --build
```

3. Acesse a API em:

```
http://localhost:5000/swagger
```

---

### Executando localmente via .NET CLI

1. Navegue até o projeto da API:

```bash
cd NTTDATAAmbevSolution.API
```

2. Execute o projeto:

```bash
dotnet run
```

3. Acesse:

```
http://localhost:5129/swagger
```

---

## Endpoints Principais

| Método | Rota | Descrição |
|--------|------|-----------|
| GET    | /api/sales           | Lista todas as vendas |
| GET    | /api/sales/{id}      | Consulta uma venda por ID |
| POST   | /api/sales           | Cria uma nova venda |
| PUT    | /api/sales/{id}/cancel | Cancela uma venda e seus itens |

---

## Estrutura do Projeto

```
NTTDATAAmbevSolution
├── API                 // Camada de apresentação
├── Application         // Serviços de aplicação, DTOs, interfaces
├── Domain              // Entidades de domínio e regras de negócio
├── Infrastructure      // Repositórios e acesso a dados (InMemory)
```

---

## Observações

- Os dados de cliente e filial estão representados por IDs e nomes estáticos para simular o padrão de identidade externa.
- A lógica de desconto é processada internamente no momento da criação.
- Eventos são apenas **logados no console**, simulando uma publicação de mensagens.

---

## Links Importantes

- **GitHub**: [https://github.com/victorapp18/NTTDATAAmbevSolution](https://github.com/victorapp18/NTTDATAAmbevSolution)  
- **Docker Hub**: [https://hub.docker.com/r/victorapp18/nttambev](https://hub.docker.com/r/victorapp18/nttambev)