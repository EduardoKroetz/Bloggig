# Bloggig - Aplicação de Blog

Uma plataforma completa de blog que permite aos usuários autenticarem-se via OAuth 2.0 ou email/senha, criar e gerenciar posts, e interagir com outros usuários. O projeto foi desenvolvido com **ASP.NET Core** e **Angular**, com o intuito de permitir que os usuários compartilhem suas ideias.

## 🚀 Funcionalidades

- **Autenticação de Usuários**: OAuth 2.0 com Google ou login com email/senha.
- **Feed de Posts**: Algoritmo de recomendação baseado em tags para feeds personalizados.
- **Perfis de Usuário**: Visualizar e editar perfis, incluindo posts e detalhes pessoais.
- **Operações CRUD**: Criar, visualizar, editar e deletar posts e comentários.
- **Pesquisar**: Buscar usuários e posts por palavras-chave.
- **Editar Conta**: Alterar detalhes da conta como nome, email e imagem de perfil.
- **Comentários**: Adicionar, editar e deletar comentários em posts.
- **Cache de Posts**: Utilização de Redis para cachear o feed de posts, melhorando a performance.

## 🛠️ Stack Tecnológico

- **Backend**: .NET 8, ASP.NET Core, C#
- **Frontend**: Angular, Tailwind CSS
- **Banco de Dados**: SQL Server
- **Cache**: Redis para cachear o feed de posts
- **Autenticação**: OAuth 2.0, Cookies para gerenciamento de sessões
- **Armazenamento de Imagens**: Azure Blob Storage para imagens de perfil e postagens
- **Deploy**:
  - **Backend**: Docker + Azure DevOps
  - **Banco de Dados**: Azure SQL Database
  - **Frontend**: Vercel

## 🖥️ Configuração & Instalação

Para configurar o projeto localmente, siga os passos abaixo:

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (Para o Angular)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
- [Redis](https://redis.io/download)
- [Docker](https://www.docker.com/)

### Configuração do Backend

1. Clone o repositório:
    ```bash
    git clone https://github.com/EduardoKroetz/Bloggig.git
    ```

2. Navegue até o diretório `backend`:
    ```bash
    cd backend
    ```

3. Configure a string de conexão do banco de dados no arquivo `appsettings.json`:
    ```json
    "ConnectionStrings": {
        "DefaultConnection": "Sua string de conexão do SQL Server aqui"
    },
    "Google": {
      "ClientId": "Seu ClientId do Google para autenticação OAuth",
      "ClientSecret": "Seu ClientSecret do Google para autenticação OAuth"
    },
    "FrontendUrl": "http://localhost:4200",
    "Azure": {
      "BlobStorageConnectionString": "Sua string de conexão do Azure Blob Storage",
      "BlobProfileImagesContainerName": "bloggig-profile-images-container",
      "BlobPostsThumbnailContainerName": "bloggig-posts-thumbnail-container",
      "BlobFileServiceUrl": "sua-conta-de-armazenamento-na-azure"
    }
    ```

4. Execute o projeto:
    ```bash
    dotnet run
    ```

### Configuração do Frontend

1. Navegue até o diretório `frontend`:
    ```bash
    cd frontend
    ```

2. Instale as dependências:
    ```bash
    npm install
    ```

3. Execute o servidor de desenvolvimento:
    ```bash
    npm run start
    ```
    
## 🔧 Como Usar

1. Acesse a página de login: [Bloggig](http://localhost:4200/auth/login)
2. Crie uma conta usando OAuth do Google ou faça o cadastro com email e senha.
3. Comece a criar, editar e deletar posts e comentários.
4. Pesquise por outros usuários e explore seus perfis.

## 🚀 Demonstração ao Vivo

Confira a versão ao vivo da aplicação: [Bloggig Deploy](https://bloggig.vercel.app/auth/login)

## 👨‍💻 Autor

- **Eduardo Kroetz** - [LinkedIn](https://www.linkedin.com/in/eduardokroetz) | [GitHub](https://github.com/EduardoKroetz)
