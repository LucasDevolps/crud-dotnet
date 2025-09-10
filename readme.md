Ótima ideia\! Adicionar a arquitetura ao `README.md` é o que diferencia um projeto simples de um projeto profissional.

A nova arquitetura em múltiplas bibliotecas de classes é um grande passo. Vamos atualizar o `README.md` para refletir essa estrutura e explicar o papel de cada camada.

-----

### README.md

# EstoqueApi - API de Gerenciamento de Estoque

Este projeto é uma API RESTful para o gerenciamento de um sistema de estoque, desenvolvida com **ASP.NET Core 8**. A arquitetura do projeto segue um padrão de **Arquitetura Limpa**, com separação de responsabilidades em múltiplas bibliotecas de classes para garantir escalabilidade e manutenibilidade.

-----

### Arquitetura do Projeto

O projeto é dividido em quatro camadas lógicas:

1.  **Estoque.Domain:** Contém as entidades de negócio (classes `Product` e `User`) e as interfaces de contrato (`IProductRepository`). É a camada que define o que a sua aplicação faz.
2.  **Estoque.Application:** Contém a lógica de negócio (`ProductService`, `AuthService`) e as interfaces que definem as funcionalidades. É a camada que orquestra as operações.
3.  **Estoque.Infrastructure:** Lida com a comunicação com o mundo externo, como o banco de dados (`AppDbContext`, `ProductRepository`) e o serviço de mensageria (`RabbitMQProducer`).
4.  **EstoqueApi:** É a camada de apresentação. Contém os controladores e os DTOs e é o ponto de entrada da sua aplicação.

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
  * **Arquitetura:** Arquitetura Limpa com Injeção de Dependência
  * **Autenticação:** JSON Web Tokens (JWT)
  * **Hashing de Senha:** BCrypt
  * **Comunicação Assíncrona:** RabbitMQ
  * **Contêinerização:** Docker e Docker Compose
  * **Banco de Dados:** SQL Server
  * **ORM:** Entity Framework Core

-----

### Instalação e Execução

A maneira recomendada de rodar a aplicação é usando o Docker Compose.

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