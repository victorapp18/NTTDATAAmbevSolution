# NTTDATAmbevSolution - API de Vendas

##  Sobre o Projeto
A **NTTDATAmbevSolution** é uma API para gerenciamento de vendas, aplicando automaticamente descontos baseados na quantidade de itens comprados. O projeto segue uma **arquitetura em camadas**, garantindo organização e escalabilidade.

## 🛠️ Tecnologias Utilizadas
- **.NET 8** (C#)
- **ASP.NET Core** (Web API)
- **Dependency Injection**
- **Arquitetura em Camadas** (Domain, Application, Infrastructure, API)
- **Testes Unitários** (opcional)

##  Estrutura do Projeto

```
NTTDATAmbevSolution/
│── NTTDATAmbevSolution.Domain/       # Entidades e Interfaces
│── NTTDATAmbevSolution.Application/  # Lógica de Negócio (Services e DTOs)
│── NTTDATAmbevSolution.Infrastructure/ # Repositórios (armazenamento em memória)
│── NTTDATAmbevSolution.API/          # Endpoints da API
│── NTTDATAmbevSolution.Tests/        # Testes unitários
└── README.md                         # Documentação
```

## Funcionalidades
 Criar uma venda
 Buscar uma venda por ID
 Listar todas as vendas
 Atualizar uma venda
 Deletar uma venda
 Aplicar descontos automáticos com base na quantidade

##  Regras de Desconto
A API aplica descontos conforme a quantidade comprada:

| Quantidade | Desconto Aplicado |
|------------|------------------|
| 1 a 3      | **0%** |
| 4 a 9      | **10%** |
| 10 a 20    | **20%** |
| 21 ou mais | ❌ **Erro (venda não permitida)** |

##  Endpoints da API

### Criar uma Venda
**POST /api/sales**
```json
{
  "customer": "Cliente X",
  "date": "2025-03-19T12:00:00",
  "items": [
    { "productId": 3, "quantity": 15, "unitPrice": 50 }
  ]
}
```
**Resposta:**
```json
{
  "id": 3,
  "date": "2025-03-19T12:00:00",
  "customer": "Cliente X",
  "totalAmount": 600,
  "items": [
    { "productId": 3, "quantity": 15, "unitPrice": 50, "discount": 150 }
  ]
}
```

### Listar Todas as Vendas
**GET /api/sales**

### Buscar Venda por ID
**GET /api/sales/{id}**

### Atualizar uma Venda
**PUT /api/sales/{id}**

### Deletar uma Venda
**DELETE /api/sales/{id}**

## 🏗️ Como Executar o Projeto
1. **Clone o repositório:**
   ```sh
   git clone https://github.com/seu-usuario/NTTDATAmbevSolution.git
   ```
2. **Acesse a pasta do projeto:**
   ```sh
   cd NTTDATAmbevSolution
   ```
3. **Rode o projeto:**
   ```sh
   dotnet run --project NTTDATAmbevSolution.API
   ```
4. **Acesse a API no navegador ou no Postman:**
   ```sh
   http://localhost:5000/swagger
   ```

##  Testando a API
Para testar os endpoints, utilize o **Postman** ou a interface **Swagger** disponível no endereço acima.

---
📌 **Dúvidas?** Sinta-se à vontade para abrir uma **issue** no repositório! 🚀

