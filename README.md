# 🍔 Projeto Good Hamburger API

**Documentação Técnica**

Este repositório contém o código-fonte do **Good Hamburger**, uma API RESTful desenvolvida em C# utilizando a plataforma .NET 10. O projeto foi concebido para simular o domínio de uma lanchonete, aplicando conceitos de arquitetura de software, padrões de projeto e boas práticas de desenvolvimento orientadas a objetos.

---

## 🚀 Tecnologias e Ferramentas Utilizadas

No desenvolvimento desta aplicação, foram adotadas as seguintes tecnologias:

* **.NET 10 (C# 14)**: Framework base utilizado para o desenvolvimento da API.
* **Entity Framework Core (EF Core 10)**: Mapeador Objeto-Relacional (ORM) escolhido para a abstração e manipulação dos dados.
* **PostgreSQL**: Sistema de gerenciamento de banco de dados relacional utilizado para a persistência.
* **MediatR**: Biblioteca empregada para implementar o padrão comportamental Mediator, facilitando o uso do padrão CQRS.
* **xUnit & FluentAssertions**: Ferramentas adotadas para a construção e asserção dos testes unitários de domínio.
* **Docker & Docker Compose**: Utilizados para a conteinerização da aplicação e do banco de dados, visando a padronização do ambiente de execução.

---

## 🏗️ Fundamentação Arquitetural

A estrutura do projeto baseia-se nos princípios da **Clean Architecture** (Arquitetura Limpa) e nas diretrizes dos princípios **SOLID**, com foco na separação de responsabilidades (Separation of Concerns). O emprego do SOLID sustenta a manutenibilidade do código das seguintes formas:

- **(S) Single Responsibility Principle**: Classes com responsabilidades únicas. 
  *Exemplo no código:* O `PedidosController` atua estritamente como porta de entrada HTTP, enquanto toda a lógica interna ou de orquestração reside de forma isolada nos *Handlers* do MediatR (como o `AtualizarPedidoCommandHandler`).
- **(O) Open/Closed Principle**: A arquitetura permite o acréscimo de novas funcionalidades por extensão, sem necessidade de alterações em códigos estabilizados. 
  *Exemplo no código:* A implementação do `ValidationBehavior` atua como um *Pipeline* transparente que valida as requisições (usando FluentValidation) do CQRS antes de chegarem ao *Handler*, sem que este saiba da existência da validação.
- **(L) Liskov Substitution Principle**: Derivações e classes implementadoras respeitam o contrato das abstrações incondicionalmente. 
  *Exemplo no código:* O padrão de retorno usando herança entre `Result` e `Result<T>`, permitindo polimorfismo limpo e seguro para representar sucessos e falhas em toda intersecção de comunicação sem lançar exceções indesejadas que invalidem o contrato.
- **(I) Interface Segregation Principle**: Criação de interfaces coesas e com escopos limitados. 
  *Exemplo no código:* O contrato do repositório de domínio (como `IPedidoRepository`) possui métodos exclusivamente granulares operando sobre a raiz de agregação Pedido, garantindo que clientes não dependam de métodos que não utilizam.
- **(D) Dependency Inversion Principle**: Módulos de alto nível (Domínio/Aplicação) ditam as interfaces e independem das implementações de baixo nível (Banco de Dados).
  *Exemplo no código:* A camada de Application trabalha as lógicas precisando apenas do `IPedidoRepository`. A classe concreta que de fato utiliza o *Entity Framework Core* (`PedidoRepository`) está mapeada dentro do projeto Infrastructure e inserida nela apenas sob o container de injeção de dependências.

### 1. Separação em Camadas

O projeto está dividido nas seguintes camadas principais:
* **Camada de Domínio (Domain)**: Representa o núcleo do sistema, não possuindo dependências de bibliotecas externas. Contém as Entidades (como `Pedido` e `Produto`), Enums (`TipoProduto`), Interfaces de repositório e Exceções customizadas.
  *Exemplo no código:* O projeto `GoodHamburger.Domain` concentra toda a estrutura principal (`Entities/Pedido.cs`, `Enums/Categoria.cs`) e é a camada base que não referencia nenhuma outra na *Solution*.
* **Camada de Aplicação (Application)**: Responsável pela orquestração do fluxo de dados e casos de uso, utilizando o padrão **CQRS** (Command Query Responsibility Segregation) em conjunto com a biblioteca MediatR.
  *Exemplo no código:* A separação entre intenções de escrita (ex.: `CriarPedidoCommand`) e intenções de leitura (ex.: `ObterProdutosQuery`) localizados no namespace `GoodHamburger.Application`.
* **Camada de Infraestrutura (Infrastructure)**: Responsável pela implementação das interfaces definidas no Domínio e pela integração com ferramentas externas, como o banco de dados via Entity Framework e a configuração de *Migrations*.
  *Exemplo no código:* O mapeamento relacional das classes de Domínio é feito em `Mappings/ProdutoConfiguration.cs` e a materialização do repositório em `Repositories/PedidoRepository.cs`.
* **Camada de Apresentação (API)**: Fornece os endpoints HTTP (Controllers). Esta camada foi projetada para ser enxuta, servindo apenas como interface de comunicação, repassando os dados para a camada de Aplicação.
  *Exemplo no código:* O `PedidosController.cs` atua de forma transparente limitando-se a converter uma requisição de entrada para a chamada abstrata `await mediator.Send(command);`.

### 2. Modelagem de Domínio Rico
Em contraposição ao modelo anêmico de domínio, buscou-se concentrar as regras de negócio nas próprias entidades.
* **Validações Internas e Regras de Negócio**: Lógicas e restrições, como o cálculo de descontos, são encapsuladas nos métodos da entidade `Pedido`, assegurando que o objeto mantenha sempre um estado válido.
* **Centralização de Erros**: O mapeamento de falhas de domínios é estruturado em constantes para evitar o uso de *magic strings*, favorecendo a manutenibilidade.

### 3. Tratamento Sistematizado de Exceções
Foi projetado um mecanismo de dupla camada para o tratamento de falhas:
1.  **Validações de Negócio**: Adoção do *Result Pattern* associado a um Action Filter customizado (`ResultActionFilter`), que intercepta o retorno da camada de aplicação e converte falhas conhecidas de domínio nos respectivos Status Codes HTTP (como 400 Bad Request ou 404 Not Found).
2.  **Exceções não Tratadas**: O tratamento de eventos inesperados ou erros de infraestrutura utiliza o recurso nativo `IExceptionHandler` do .NET, padronizando a resposta de erro através da especificação **ProblemDetails** (RFC 7807).

### 4. Boas Práticas Adicionais
* **Otimização de Consultas**: Acesso aos dados desenhado visando mitigar problemas de performance (como o problema N+1), utilizando técnicas de *Eager Loading* (`Include`) no EF Core.
* **Semântica HTTP**: Uso adequado de verbos HTTP e códigos de retorno compatíveis com a especificação RESTful.
* **Testes Automatizados**: A camada de Domínio foi submetida a testes unitários (utilizando o *framework* xUnit) para homologar o funcionamento da lógica de descontos aplicadas às diferentes combinações de categorias (Sanduíches, Bebidas e Acompanhamentos).

---

## ⚙️ Instruções de Execução

Para homologar a execução do projeto, propõe-se duas abordagens: ambiente conteinerizado via Docker ou execução local.

### Opção 1: Execução via Docker Compose (Recomendado)

Esta abordagem instanciará simultaneamente a API e o banco de dados PostgreSQL.

**Pré-requisitos**:
* [Docker Desktop](https://www.docker.com/products/docker-desktop) instalado e em execução.

**Passos**:
1. Através de um terminal, acesse a pasta raiz do repositório, onde encontra-se o arquivo `docker-compose.yml`.
2. Rode o comando de provisionamento:
   ```bash
   docker compose up --build -d
   ```
3. A inicialização aplicará as *migrations* automaticamente no banco de dados. 
4. A documentação da API (via Scalar) estará disponível em: `http://localhost:8080/scalar/v1`

*Nota: Para destruir os contêineres, execute `docker compose down`.*

### Opção 2: Execução Local (.NET CLI / IDE)

Neste cenário, a API é executada direto na máquina anfitriã, exigindo que o banco de dados já esteja disponível.

**Pré-requisitos**:
* [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) instalado.
* Instância de servidor PostgreSQL ativa.

**Passos**:
1. Verifique se a variável `DefaultConnection` no arquivo de configuração (`GoodHamburger.API/appsettings.json`) possui as credenciais referentes à instância local do PostgreSQL.
2. Com um terminal aberto na raiz do projeto, inicialize a aplicação:
   ```bash
   dotnet run --project GoodHamburger.API/GoodHamburger.API.csproj
   ```
   *(Alternativamente, é possível iniciar o projeto abrindo a solução `GoodHamburger.sln` com o Visual Studio).*
3. Ao iniciar, a classe `Program.cs` se comunicará com o banco garantindo a atualização do esquema de tabelas antes de prosseguir. A rota de documentação será indicada no registro do terminal.

---

## 🧪 Execução da Suíte de Testes

Os testes unitários validados no projeto encontram-se encapsulados no laboratório de testes do diretório `GoodHamburger.Tests`.
Para acioná-los publicamente via linha de comando, certifique-se de estar na raiz do projeto e execute:

```bash
dotnet test
```

---

## 🌱 Considerações Finais e Trabalhos Futuros

O desenvolvimento da "Good Hamburger API" foi uma oportunidade prática de consolidar conceitos de integração de subsistemas visando alto nível de coesão, flexibilidade e baixo acoplamento.

Para desdobramentos futuros em trabalhos e pesquisas sobre o projeto, propõe-se:
* **Evolução da Suíte de Testes**: Aprofundamento das práticas de *Quality Assurance* através da implementação de testes *End-to-End* (E2E), garantindo a confiabilidade de todo o fluxo informacional, desde a recepção HTTP pela API até a persistência no banco de dados.
* **Desenvolvimento de Interface Gráfica**: Consumo funcional e sistemático desta API através da criação de uma aplicação Web (SPA) *Front-End* utilizando a tecnologia **Blazor WebAssembly**, demonstrando a robustez dos contratos e o tempo de resposta da arquitetura construída.
