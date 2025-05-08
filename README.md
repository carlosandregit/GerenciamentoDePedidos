# 🛒 API de Gerenciamento de Pedidos

## 📄 Descrição

Esta API foi desenvolvida para gerenciar de forma simples e eficiente pedidos de compra, incluindo o cadastro de clientes, produtos e o controle de pedidos. 

O projeto foi estruturado com foco na organização do código, segurança e facilidade de manutenção, utilizando boas práticas como Clean Architecture e autenticação via JWT.

---

## 🧰 Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Entity Framework Core
- EF Core Tools & Design
- PostgreSQL
- JWT (JSON Web Token) Authentication

---

## 🏗️ Estrutura do Projeto

A arquitetura foi organizada em camadas para garantir desacoplamento e manutenibilidade:

- **Domain**: Contém as entidades, interfaces e regras de negócio do domínio.
- **Application**: Inclui os DTOs, interfaces de serviço e lógica de aplicação.
- **Infrastructure**: Implementação dos repositórios, contextos e serviços externos.
- **Presentation (API)**: Camada de apresentação com os controllers e a configuração da API (program.cs, DI, middlewares, etc.).

---

## ✅ Pré-requisitos

- [.NET SDK 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Visual Studio 2022 ou VS Code
- PostgreSQL instalado localmente **(porta: 5433 | senha: `devpass`)**
- NuGet CLI ou acesso ao Console do Gerenciador de Pacotes

---

## ⚙️ Configuração do Ambiente

1. **Instale o PostgreSQL** localmente ou via Docker.
2. Crie um banco chamado `pedidosdb`.
3. Configure o usuário `devuser` com a senha `devpass`.
4. Certifique-se de que o PostgreSQL está escutando na porta **5433**.
5. No projeto, verifique a string de conexão no `appsettings.json`:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5433;Database=pedidosdb;Username=devuser;Password=devpass"
   }
   ```

6. Aplique as migrations com:

   ```bash
   dotnet ef database update
   ```

---

## 🔐 Autenticação

- A autenticação da API é feita via **JWT**.
- Um endpoint de cadastro de usuário foi criado para gerar credenciais de acesso.
- Após o login, o token deve ser enviado no cabeçalho `Authorization: Bearer {token}` para acessar os endpoints protegidos.

---

## 🔁 Observações de Implementação

1. **Endpoints usando somente POST**

   Todos os endpoints da API foram implementados utilizando exclusivamente o verbo **HTTP POST**, por uma decisão técnica voltada à **segurança e controle**.

   Isso evita que dados sensíveis sejam expostos em URLs (como ocorre em requisições GET), além de reduzir riscos com cache indesejado ou histórico do navegador contendo dados críticos.

2. **Cadastro de usuário habilitado**

   Para fins de testes e desenvolvimento, a API disponibiliza um endpoint para cadastrar novos usuários que poderão ser utilizados para autenticação e geração de tokens.

---

## 🚀 Como Executar

1. Clone este repositório:
   ```bash
   git clone https://github.com/seu-usuario/seu-repositorio.git
   cd seu-repositorio
   ```

2. Instale os pacotes EF Core (se necessário):
   ```powershell
   Install-Package Microsoft.EntityFrameworkCore.Design
   Install-Package Microsoft.EntityFrameworkCore.Tools
   ```

3. Crie o banco (se ainda não existir):
   ```bash
   dotnet ef database update
   ```

4. Execute o projeto:
   ```bash
   dotnet run
   ```

---

## 📌 Branch Principal

- Desenvolvimento contínuo na branch: **`develop`**
