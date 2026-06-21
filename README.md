@'
# Gerenciador de Endereços - Teste Desenvolvedor C#

Aplicação web desenvolvida em C# com ASP.NET Core MVC para gerenciamento de endereços.

O sistema permite que o usuário realize login, cadastre endereços manualmente ou busque os dados do endereço através do CEP utilizando a API ViaCEP. Também é possível visualizar, editar, excluir e exportar os endereços cadastrados para um arquivo CSV.

## Funcionalidades

- Login de usuário
- Validação de credenciais
- Cadastro de endereços
- Listagem de endereços cadastrados
- Edição de endereços
- Exclusão de endereços
- Busca automática de endereço por CEP usando a API ViaCEP
- Exportação dos endereços para arquivo CSV
- Script de criação das tabelas do banco de dados

## Tecnologias utilizadas

- C#
- ASP.NET Core MVC
- Entity Framework Core
- SQLite
- HTML
- CSS
- JavaScript
- Bootstrap
- ViaCEP

## Estrutura do projeto

AEC/
- Controllers/
  - LoginController.cs
  - EnderecosController.cs

- Data/
  - AppDbContext.cs

- Models/
  - Usuario.cs
  - Endereco.cs
  - ViaCepResposta.cs

- Views/
  - Login/
    - Index.cshtml
  - Enderecos/
    - Index.cshtml
    - Criar.cshtml
    - Editar.cshtml
    - Excluir.cshtml

- Scripts/
  - banco.sql

- wwwroot/
- Program.cs
- appsettings.json
- README.md

## Banco de dados

O projeto utiliza SQLite para simplificar a execução local.

O banco de dados é criado automaticamente ao executar a aplicação, através do Entity Framework Core.

Também foi incluído o script de criação das tabelas na pasta:

Scripts/banco.sql

## Tabelas

### Usuarios

- Id
- Nome
- UsuarioLogin
- Senha

### Enderecos

- Id
- Cep
- Logradouro
- Complemento
- Bairro
- Cidade
- Uf
- Numero
- UsuarioId

## Usuário padrão

Para acessar o sistema, utilize:

Usuário: admin  
Senha: 123456

## Como executar o projeto

### 1. Clonar o repositório

git clone https://github.com/Yanna01santos/teste-dev-sharp-aec.git

### 2. Acessar a pasta do projeto

cd teste-dev-sharp-aec

### 3. Restaurar os pacotes

dotnet restore

### 4. Compilar o projeto

dotnet build

### 5. Executar o projeto

dotnet run

Após executar, acesse no navegador o endereço exibido no terminal.

Exemplo:

https://localhost:7000

ou

http://localhost:5000

## Como usar o sistema

1. Acesse a tela inicial de login.
2. Informe o usuário e senha padrão.
3. Após o login, será exibida a tela de endereços.
4. Clique em "Novo Endereço" para cadastrar um endereço.
5. Informe o CEP e clique em "Buscar CEP" para preencher automaticamente os dados retornados pela API ViaCEP.
6. Preencha o número do endereço manualmente.
7. Salve o endereço.
8. Na tela principal, é possível editar, excluir ou exportar os endereços cadastrados.
9. Clique em "Exportar CSV" para baixar o arquivo com os endereços.

## Scripts do banco

O script de criação das tabelas está disponível em:

Scripts/banco.sql

Esse arquivo contém a estrutura das tabelas Usuarios e Enderecos, além da inserção de um usuário padrão para teste.

## Observações

- O campo complemento é opcional.
- O número do endereço é preenchido manualmente, pois a API ViaCEP não retorna essa informação.
- A aplicação controla os endereços por usuário logado.
- O arquivo CSV é gerado com os endereços cadastrados pelo usuário autenticado.
- O arquivo de banco SQLite local não foi versionado no GitHub, pois ele é gerado automaticamente ao executar a aplicação.

## API utilizada

A integração de CEP foi feita utilizando a API pública ViaCEP:

https://viacep.com.br/

## Repositório

https://github.com/Yanna01santos/teste-dev-sharp-aec

## Autor

Yanna Aparecida

Projeto desenvolvido para teste prático de Desenvolvedor C#.

'@ | Set-Content -Path README.md -Encoding UTF8