
# API de CRUD de Cadastro de Usuário

Este projeto é uma API desenvolvida em C# utilizando o ASP.NET Core Web API, que realiza operações de CRUD (Create, Read, Update, Delete) para o cadastro de usuários. A API interage com um banco de dados SQL Server rodando em um contêiner Docker.

## Funcionalidades

- **Criar Usuário:** Adiciona um novo usuário ao sistema via requisição POST.
- **Visualizar Usuários:** Retorna uma lista de todos os usuários cadastrados via requisição GET.
- **Visualizar Usuário por ID:** Retorna os detalhes de um usuário específico via requisição GET com ID.
- **Atualizar Usuário:** Atualiza as informações de um usuário existente via requisição PUT.
- **Deletar Usuário:** Remove um usuário do sistema via requisição DELETE.

## Tecnologias Utilizadas

- **Linguagem:** C#
- **Framework:** ASP.NET Core Web API
- **Banco de Dados:** SQL Server em contêiner Docker

## Requisitos

- [.NET 6.0 ou superior](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started)

## Como Executar

1. **Clone o repositório:**

   ```bash
   git clone https://github.com/babingthon/CrupDapper.git
   ```

2. **Configure o banco de dados:**

   - Certifique-se de que o Docker está instalado e em execução.
   - Use o seguinte comando para iniciar um contêiner com o SQL Server:

     ```bash
     docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=SuaSenha@123' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
     ```

3. **Configure a conexão com o banco de dados:**

   - No arquivo `appsettings.json`, configure a string de conexão com o SQL Server:

     ```json
     "ConnectionStrings": {
       "DefaultConnection": "server=localhost;database=CrudDapper; User Id=SA; Password=SUA_SENHA_FORTE; trusted_connection=false; trustservercertificate=true"
     }
     ```

4. **Execute a aplicação:**

   - Navegue até o diretório do projeto e execute:

     ```bash
     dotnet run
     ```

5. **Use a API:**

   - A API estará disponível em `https://localhost:7070/swagger/index.html` ou `https://localhost:7071/swagger/index.html`.

## Endpoints da API

- **POST /api/usuarios** - Cria um novo usuário.
- **GET /api/usuarios** - Retorna todos os usuários.
- **GET /api/usuarios/{id}** - Retorna um usuário específico.
- **PUT /api/usuarios/{id}** - Atualiza as informações de um usuário.
- **DELETE /api/usuarios/{id}** - Deleta um usuário.

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).
