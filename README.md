# Good Hamburger API

Esta é a API para a hamburgueria Good Hamburger, utilizando Arquitetura Limpa, C# e .NET 10.
O projeto utiliza banco de dados PostgreSQL.

## Pré-requisitos

- [Docker](https://www.docker.com/products/docker-desktop) instalado e rodando.
- [Docker Compose](https://docs.docker.com/compose/install/) instalado.

## Como rodar o projeto

1. Clone o repositório ou navegue até a raiz do projeto.
2. Na raiz do projeto (onde se encontra o arquivo `docker-compose.yml`), abra um terminal.
3. Execute o comando abaixo para construir a imagem da API e subir os containers (Banco de Dados e API):

```bash
docker-compose up --build -d
```

4. A API será iniciada na porta `8080`.
   - Você pode acessar a documentação (Scalar) através do navegador: `http://localhost:8080/scalar/v1` ou verificar as rotas pela ferramenta de requisição de sua preferência.
   - O banco de dados PostgreSQL ficará disponível na porta `5432` com as credenciais padrão do container (Usuário: `postgres`, Senha: `password`, Database: `GoodHamburgerDB`).
   - As migrations do Entity Framework Core são rodadas **automaticamente** ao inicializar o ambiente da API, portanto ao subir o projeto o banco já estará criado e pronto para uso.

## Testes

Para rodar os testes da aplicação, você pode utilizar o comando na raiz do seu projeto:

```bash
dotnet test
```

## Como encerrar a aplicação

Para parar e remover os containers e a infraestrutura que o compose criou, na raiz do projeto digite:

```bash
docker-compose down
```
