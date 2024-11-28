
# FSBR Web API

Este é um projeto de **API** RESTful utilizando **ASP.NET Core**, implementando o **Repository Pattern** para o gerenciamento de **Produtos** e **Categorias**, utilizando o banco de dados **LocalDB** no **SQL Server**.

## Estrutura do Projeto

A estrutura do projeto segue uma organização baseada no **Repository Pattern**, separando as camadas de **acesso a dados**, **serviços** e **controllers**.

### Estrutura de Pastas

```
/FSBR-WebAPI
│
├── /Controllers               # Camada de controle, onde a API é exposta.
│   ├── ProductController.cs   # Controlador de Produtos
│   ├── CategoryController.cs  # Controlador de Categorias
│
├── /Entities                  # Contém os modelos de dados (Product, Category)
│   ├── Product.cs
│   ├── Category.cs
│
├── /Domain                    # Camada de serviço, contendo a lógica de negócios.
│   ├── /Interfaces            # Interfaces dos repositórios e serviços.
│   │   ├── IProductService.cs
│   │   ├── ICategoryService.cs
│   │
│   ├── /Services              # Implementações dos serviços.
│   │   ├── ProductService.cs
│   │   ├── CategoryService.cs
│
├── /Infrastructure            # Camada de acesso a dados e repositórios.
│   ├── /Configuration         # Configuração do DbContext e dependências.
│   │   ├── ContextBase.cs     # DbContext para o acesso ao banco de dados.
│   │
│   ├── /Repository            # Implementação dos repositórios.
│   │   ├── ProductRepository.cs
│   │   ├── CategoryRepository.cs
│
├── /Migrations                # Migrations do Entity Framework para criação do banco de dados.
│
├── /appsettings.json          # Arquivo de configuração com strings de conexão e outras variáveis.
├── /Program.cs                # Configuração da aplicação e serviços (DI).
├── /Startup.cs                # Configuração da aplicação e serviços (para versões anteriores).
```

## Funcionalidades

- **Cadastro de Produtos e Categorias**: A API permite adicionar, atualizar, excluir e listar produtos e categorias.
- **Relacionamento 1:N entre Produtos e Categorias**: Cada produto está associado a uma categoria e vice-versa.
- **Migrations**: O banco de dados é gerido via migrations do Entity Framework, permitindo a criação e atualização automática das tabelas no LocalDB.

## Como Rodar o Projeto

### **1. Instalar Dependências**

Certifique-se de que as dependências do projeto estejam instaladas. Você pode rodar o comando:

```bash
dotnet restore
```

### **2. Criar o Banco de Dados**

Se ainda não foi criado, você pode gerar as **migrations** e aplicar no **LocalDB** executando:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### **3. Rodar o Projeto**

Execute o projeto com o comando:

```bash
dotnet run
```

A API estará disponível em **http://localhost:5000** (por padrão, ou conforme configurado).

### **4. Testar a API**

Você pode testar a API usando o **Postman** ou **Swagger** (se configurado). Algumas das rotas disponíveis:

- **GET** `/api/Produtos/{id}`: Obter detalhes de um produto.
- **POST** `/api/Produtos`: Criar um novo produto.
- **PUT** `/api/Produtos/{id}`: Atualizar um produto.
- **DELETE** `/api/Produtos/{id}`: Deletar um produto.
- **GET** `/api/Produtos/ListarProdutos`: Listar todos os produtos.

## Contribuição

Se você quiser contribuir com este projeto, siga os seguintes passos:

1. Fork o repositório.
2. Crie uma branch (`git checkout -b minha-nova-feature`).
3. Faça as alterações e commit (`git commit -am 'Adicionando nova funcionalidade'`).
4. Envie para o repositório original (`git push origin minha-nova-feature`).
5. Abra um pull request.

### **Licença**

Este projeto está licenciado sob a **MIT License** - consulte o arquivo [LICENSE](LICENSE) para mais detalhes.
