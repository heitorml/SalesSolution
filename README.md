# SalesSolution

O projeto tem como objetivo disponibilizar as funcionalidades de receber pedidos de clientes e enviar pedidos para fornecedores

Logo, crei duas API's e um Worker
- Resales.Api : Responsavel pelo cadastro de novos revendendores, possui um banco de dados dedicado.
- Orders.Api : Recebe pedidos dos cliente e os envia para o fornecedor atraves, possui um banco de dados dedicado.
- Orders.Worker : Processa os pedidos que estão prontos para serem enviados para o fornecedor

Eventos dos Pedidos
- ReceivedOrder - Este evento é publicado sempre um cliente envia um pedido para a revenda.
- ReadyForShippingOrder - O pedido esta pronto para envio quando a revenda cria um pedido para o fonecedor seguindo critérios.  
- OrderSentToSupplier - Marca que o pedido foi enviado para a revenda




  
---

## 🛠️ Tecnologias e Padrões Utilizados

### 🧼 Clean Architecture
- Separação de responsabilidades em 4 camadas principais
- Independência da infraestrutura
- Inversão de dependência via interfaces
- Facilidade para testes unitários e mocks

### 🟣 .NET 8
- Última versão LTS da plataforma .NET
- Alto desempenho e suporte a APIs modernas

### 🌐 ASP.NET Core
- Framework para construção de APIs RESTful robustas
- Usado nos projetos `Orders.Api` e `Resales.Api`

### ⚙️ Worker Service
- Utilizado para processamentos em background com o `Orders.Worker`
- Ideal para filas, cron jobs ou mensageria

### 📦 MassTransit + RabbitMQ
- **MassTransit**: biblioteca de mensageria para comunicação entre microsserviços
- **RabbitMQ**: broker de mensagens usado como transporte

### 🧪 xUnit + Moq
- **xUnit**: framework de testes unitários
- **Moq**: criação de mocks de dependências para testes

### ⚙️ Monitoramento Jeager
- Utilizado para obter logs e traces
- Utiliza openTelemetry com exportação OTL 


### 📊 Coverlet + Cobertura + Azure Pipelines
- **Coverlet**: biblioteca para análise de cobertura de código
- Geração de relatórios no formato `cobertura.xml` (compatível com CI)

### 📈 GitHub Actions (Em desenvolvimento)
- CI/CD automatizado
- Build, testes, publicação e upload de artefatos
- Geração de resultados de testes e cobertura de código

### 📦 Docker (opcional)
- Possível conteinerização da API e Worker
- Facilita testes locais e deployment

---

## 📁 Projetos

| Projeto                       | Camada        | Descrição                                                        |
|-------------------------------|---------------|------------------------------------------------------------------|
| `Orders.Api`                  | Presentation  | API de pedidos para receber pedidos e enviar para o fornecedor   |
| `Resales.Api`                 | Presentation  | API para CRUD básico des revendas                                |
| `Orders.Worker`               | Presentation  | Worker Service para consumo de mensagens                         |
| `Application.*`               | Application   | Casos de uso, interfaces                                         |
| `Domain.*`                    | Domain        | Entidades, enums, regras puras                                   |
| `Infrastructure.*`            | Infrastructure| Repositórios, contextos, MongDb                                  |
| `Infrastructure.*`            | CrossCutting  | DTOs e extensões                                                 |
| `*.Tests`                     | Tests         | Testes unitários com xUnit e Moq                                 |

---

## ✅ Pré-requisitos

Certifique-se de que os seguintes softwares estejam instalados em sua máquina:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/) (Infraestrutura local
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/)



## 🚀 Executando Localmente

1. Tenha o [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) instalado
2. Restaure os pacotes:

```bash
dotnet restore
