# SalesSolution

O projeto tem como objetivo disponibilizar as funcionalidades de receber pedidos de clientes e enviar pedidos para fornecedores


### Sobre os Eventos

Eventos dos Pedidos
- ReceivedOrder - Este evento é publicado sempre um cliente envia um pedido para a revenda.
- ReadyForShippingOrder - O pedido esta pronto para envio quando a revenda cria um pedido para o fonecedor seguindo critérios.  
- OrderSentToSupplier - Marca que o pedido foi enviado para a revenda.
- OrderCancelled - Ocorre quando falha sistemica no envio do pedido ao fornecedor.

Eventos da Revenda
- ResaleCreated - Ocorre quando uma nova revenda é cadastrada.
- ResaleUpdated - Ocorre quando dados da revenda são atualizadas. 

Cada evento possui um consumidor que reage ao fato processando as ações. 
Os eventos são transmitos através do messageBroker RabbitMq, a implmentação utiliza o MasstransitV8 (https://masstransit.io/quick-starts)

### Sobre as entidades

Foram criadas as entidades: 
- Resales 
- Orders
- ItemsOrder
- Address

obs: As entidades são armazenadas de forma não relacional usando um repositorio MongoDb

### Sobre a

Foram criadas as entidades: 
- Resales 
- Orders
- ItemsOrder
- Address

obs: As entidades são armazenadas de forma não relacional usando um repositorio MongoDb

### Sobre a mensageria 

Utiliza-se os componentes:
- **MassTransitV8**: biblioteca de mensageria para comunicação entre microsserviços
- **RabbitMQ**: broker de mensagens usado como transporte


### Sobre a aborgem arquitetural

Para esta solução foi escolhida a Clean Architecture que traz os benefícios

- Separação de responsabilidades em 4 camadas principais
- Independência da infraestrutura
- Inversão de dependência via interfaces
- Facilidade para testes unitários e mocks

## 📁 Projetos

| Projeto                       | Camada          | Descrição                                                        |
|-------------------------------|-----------------|------------------------------------------------------------------|
| `Orders.Api`                  | 1-Presentation  | API de pedidos para receber pedidos e enviar para o fornecedor   |
| `Resales.Api`                 | 1-Presentation  | API para CRUD básico des revendas                                |
| `Orders.Worker`               | 1-Presentation  | Worker Service para consumo de mensagens                         |
| `Application.*`               | 2-Application   | Casos de uso, interfaces                                         |
| `Domain.*`                    | 3-Domain        | Entidades, enums, regras puras                                   |
| `Infrastructure.*`            | 4-Infrastructure| Repositórios, contextos, MongDb                                  |
| `CrossCutting.*`              | 5-CrossCutting  | DTOs e extensões                                                 |
| `*.Tests`                     | Tests           | Testes unitários com xUnit e Moq                                 |

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

### 🧼 Resilience - Polly
- Politicas de retentativas exponencial
- CircuitBreaker
- Polly - Biblioteca de politicas de resiliencia em chamadas Http

### 📦 Refit 
- Para comunicação entre a `Orders.Api` e `Resales.Api`
- Abstrai toda a implementação do HttpClient 

### 📈 Fail Fast Validation 
- Utiliza a biblioteca FluentValidation para efetuar validações de requisições

### 🗂️ Repositorio NoSQL 
- Utilização do MongoDb para armazenamento não relacional dos documentos  

### 📦 MassTransit + RabbitMQ
- **MassTransit**: biblioteca de mensageria para comunicação entre microsserviços
- **RabbitMQ**: broker de mensagens usado como transporte

### 🧪 xUnit + Moq
- **xUnit**: framework de testes unitários
- **Moq**: criação de mocks de dependências para testes

### 📈 Monitoramento Jeager
- Utilizado para obter logs e traces
- Utiliza openTelemetry com exportação OTL 

### 📊 Coverlet + Cobertura + Azure Pipelines
- **Coverlet**: biblioteca para análise de cobertura de código
- Geração de relatórios no formato `cobertura.xml` (compatível com CI)

### 📈 GitHub Actions (Em desenvolvimento)
- CI/CD automatizado
- Build, testes, publicação e upload de artefatos
- Geração de resultados de testes e cobertura de código


flowchart TD
    A[Apresentação<br>API / Worker] --> B[Application Layer<br>UseCases]
    B --> C[Domain Layer<br>Entities, Interfaces]

    subgraph External Services
        E[HTTP APIs via Refit]
        F[MongoDB]
        G[Message Bus (RabbitMQ)]
    end

    C --> D[Infrastructure<br>Repositories, Adapters]
    D -->|Chama| E
    D -->|Consulta| F
    D -->|Publica / Consome| G
    B -->|Chama| D


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
