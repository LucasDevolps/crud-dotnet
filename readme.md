Ótima ideia\! Manter o `README.md` atualizado é uma prática profissional essencial.

Desde a última versão, adicionamos funcionalidades importantes como o **Docker** e o **RabbitMQ**, além de refinar os endpoints. Aqui está um `README.md` completo e atualizado que reflete o estado atual do seu projeto.

-----

### README.md

# EstoqueApi - API de Gerenciamento de Estoque

Este projeto é uma API RESTful para o gerenciamento de um sistema de estoque, desenvolvida com **ASP.NET Core 8**. A API segue os padrões de segurança e arquitetura mais recomendados, como a Arquitetura em Camadas, Injeção de Dependência, autenticação via JWT e comunicação assíncrona via RabbitMQ.

-----

### Funcionalidades

A API fornece os seguintes endpoints:

#### **Autenticação e Autorização**

  * `POST /api/Auth/login`: Realiza o login de um usuário e retorna um token JWT (JSON Web Token) válido.
  * `POST /api/Auth/register`: Cadastra um novo usuário com senha segura (BCrypt).
  * **Proteção de Rotas:** Todos os endpoints de gerenciamento de estoque são protegidos pelo atributo `[Authorize]`, exigindo um JWT válido.

#### **Gerenciamento de Estoque (CRUD)**

  * `GET /api/Estoque`: Retorna a lista de todos os produtos cadastrados.
  * `GET /api/Estoque/{uuid}`: Retorna um produto específico pelo seu `uuid`.
  * `POST /api/Estoque`: Adiciona um novo produto ao estoque. Uma mensagem de evento é enviada para o RabbitMQ.
  * `PUT /api/Estoque/{uuid}`: Atualiza um produto existente. Uma mensagem de evento é enviada para o RabbitMQ.
  * `DELETE /api/Estoque/{uuid}`: Remove um produto do estoque.

-----

### Tecnologias

  * **Framework:** ASP.NET Core 8
  * **Autenticação:** JSON Web Tokens (JWT)
  * **Hashing de Senha:** BCrypt
  * **Comunicação Assíncrona:** RabbitMQ
  * **Contêinerização:** Docker e Docker Compose
  * **Banco de Dados:** SQL Server
  * **ORM:** Entity Framework Core
  * **Arquitetura:** Injeção de Dependência e Padrão de Repositório

-----

### Instalação e Execução com Docker

A maneira mais fácil e recomendada de rodar a aplicação é usando o Docker Compose.

#### **1. Pré-requisitos**

  * .NET 8 SDK
  * Docker Desktop (ou equivalente)

#### **2. Crie e Atualize o Banco de Dados**

Antes de rodar com Docker, você precisa garantir que o banco de dados e as tabelas estão criadas.

1.  Navegue até a pasta **`EstoqueApi`**.
2.  Execute o comando a seguir no terminal para aplicar as migrações e criar o banco de dados:
    ```bash
    dotnet ef database update
    ```

O método `SeedUsers` no `AppDbContext` irá criar automaticamente um usuário de teste com e-mail `teste@teste.com` e senha `123`.

#### **3. Execute a Aplicação com Docker Compose**

Na pasta raiz do seu projeto (onde está o `docker-compose.yml`), execute o seguinte comando:

```bash
docker-compose up --build -d
```

Este comando irá:

  * Construir a imagem Docker do seu back-end.
  * Iniciar os contêineres do **RabbitMQ** e da sua **EstoqueApi**.

-----

### Como Usar e Testar

  * **API:** A sua API estará disponível em `https://localhost:7039`. Você pode testar os endpoints através da interface do Swagger, disponível em `https://localhost:7039/swagger`.
  * **RabbitMQ:** O painel de gerenciamento do RabbitMQ estará disponível em `http://localhost:15672`. As credenciais padrão são **`admin`** e **`admin`**.

Com este `README.md`, qualquer pessoa conseguirá entender e rodar o seu projeto rapidamente.