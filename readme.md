# EstoqueApi - API de Gerenciamento de Estoque

Este projeto é uma API RESTful para o gerenciamento de um sistema de estoque, desenvolvida com **ASP.NET Core 8**. A API segue os padrões de segurança e arquitetura mais recomendados, como a Arquitetura em Camadas, Injeção de Dependência e autenticação via JWT.

-----

### Funcionalidades

A API fornece os seguintes endpoints:

#### **Autenticação**

  * `POST /api/Auth/login`: Realiza o login de um usuário e retorna um JWT (JSON Web Token) válido.
  * `POST /api/Auth/register`: Cadastra um novo usuário, com senha segura (BCrypt).

#### **Gerenciamento de Estoque (CRUD)**

  * `GET /api/Estoque`: Retorna a lista de todos os produtos cadastrados.
  * `GET /api/Estoque/{uuid}`: Retorna um produto específico pelo seu `uuid`.
  * `POST /api/Estoque`: Adiciona um novo produto ao estoque.
  * `PUT /api/Estoque/{uuid}`: Atualiza um produto existente.
  * `DELETE /api/Estoque/{uuid}`: Remove um produto do estoque.

-----

### Tecnologias

  * **Framework:** ASP.NET Core 8
  * **Autenticação:** JSON Web Tokens (JWT)
  * **Hashing de Senha:** BCrypt
  * **Banco de Dados:** SQL Server
  * **ORM:** Entity Framework Core
  * **Arquitetura:** Injeção de Dependência e Repositório

-----

### Instalação e Execução

Para rodar o projeto, siga estes passos:

#### **1. Pré-requisitos**

  * .NET 8 SDK
  * Uma instância local do SQL Server (ou qualquer outro banco de dados compatível com o Entity Framework Core).

#### **2. Clone o Repositório**

```bash
git clone https://github.com/seu-usuario/seu-repositorio.git
cd seu-repositorio/EstoqueApi
```

#### **3. Restaure as Dependências**

Rode o comando a seguir para instalar todas as bibliotecas necessárias:

```bash
dotnet restore
```

#### **4. Crie e Atualize o Banco de Dados**

O projeto usa o Entity Framework Core para gerenciar o banco de dados. Execute os comandos abaixo no terminal para criar o banco de dados e a tabela de produtos:

```bash
dotnet ef database update
```

O método `SeedUsers` no `AppDbContext` irá criar automaticamente um usuário de teste com e-mail `teste@teste.com` e senha `123`.

#### **5. Configure o `appsettings.json`**

Verifique a seção `ConnectionStrings` no arquivo `appsettings.json` e ajuste a string de conexão para corresponder ao seu ambiente local. Certifique-se de que a `Jwt:Key` está definida.

#### **6. Execute a API**

```bash
dotnet run
```

A API estará disponível em `https://localhost:7039` (a porta pode variar).

-----

### Como Usar

Você pode testar todos os endpoints da API usando o **Swagger**. A interface de usuário do Swagger estará disponível em `https://localhost:7039/swagger`.

1.  **Login:** Envie uma requisição `POST` para `/api/Auth/login` com as credenciais de teste para obter um token JWT.
2.  **Autorização:** Copie o token e clique no botão **"Authorize"** no Swagger para usá-lo em todas as outras requisições.