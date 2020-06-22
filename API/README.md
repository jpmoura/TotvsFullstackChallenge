# TOTVS Fullstack Challenge - Backend

[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=jpmoura_TotvsFullstackChalleng&metric=bugs)](https://sonarcloud.io/dashboard?id=jpmoura_TotvsFullstackChalleng)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=jpmoura_TotvsFullstackChalleng&metric=code_smells)](https://sonarcloud.io/dashboard?id=jpmoura_TotvsFullstackChalleng)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=jpmoura_TotvsFullstackChalleng&metric=coverage)](https://sonarcloud.io/dashboard?id=jpmoura_TotvsFullstackChalleng)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=jpmoura_TotvsFullstackChalleng&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=jpmoura_TotvsFullstackChalleng)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=jpmoura_TotvsFullstackChalleng&metric=alert_status)](https://sonarcloud.io/dashboard?id=jpmoura_TotvsFullstackChalleng)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=jpmoura_TotvsFullstackChalleng&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=jpmoura_TotvsFullstackChalleng)
[![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=jpmoura_TotvsFullstackChalleng&metric=sqale_index)](https://sonarcloud.io/dashboard?id=jpmoura_TotvsFullstackChalleng)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=jpmoura_TotvsFullstackChalleng&metric=vulnerabilities)](https://sonarcloud.io/dashboard?id=jpmoura_TotvsFullstackChalleng)

Como parte do desafio _fullstack_, para o _backend_ foi criada uma API REST em C# usando [.NET Core 3.1](https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-core-3-1) e [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1) persistindo dados com [MongoDB](https://www.mongodb.com/).

Essa API também conta com uma rota de documentação via [Swagger](https://swagger.io/) que pode ser acessada no endpoint `\swagger` no endereço que a API está hospedada.

## 1. Configurações

A _string_ de conexão e o nome da base de dados podem ser modificados no arquivo [appsettings.json](TOTVS.Fullstack.Challenge.AuctionHouse.RestApi\appsettings.json). Por padrão esses parâmetros estão configurados para usar uma instância local do [MongoDB](https://www.mongodb.com/) (i.e. `mongodb://localhost:27017`) e base de dados chamada `auctionHouse`.

Recomenda-se a criação pelo menos da coleção `users` com dados de um usuário para que seja possível utlizar a API, visto que ela conta com uma autenticação via JWT. Existe uma rota de autenticação que utiliza os dados do usuário para retornar o token e para que a autenticação seja feita, é necessário existir o usuário no banco. O modelo do documento de usuário é:

```json
{
    "name":"Primeiro usuário",
    "username":"teste",
    "password":"teste",
    "isActive":true
}
```

**Não existe criptografia ou hash de senha implementados nessa versão**, por isso as senhas podem ser armazenadas em texto plano no banco.

É possível editar a chave usada para criação e leitura dos JWT utilizados na autenticação. Para isso basta editar o valor da chave `Secret` na seção de `Security` arquivo [appsettings.json](TOTVS.Fullstack.Challenge.AuctionHouse.RestApi\appsettings.json).

É necessário que o servidor tenha um certificado SSL válido visto que a aplicação conta com redireção para HTTPS por padrão. Esse comportamento também pode ser alterado editando o arquivo [appsettings.json](TOTVS.Fullstack.Challenge.AuctionHouse.RestApi\appsettings.json) e modificando o valor da chave `UseHttps` na seção de `Network`.

## 2. Instruções

Todos os comandos descritos a seguir considera-se que o usuário está já na pasta do projeto.

Também foi utilizado o [Cake](https://cakebuild.net/) para facilitar a criação de _scripts_ que envolvem tarefas comuns de desenvolvimento como realizar um _build_, executar testes, gerar relatórios de cobertura, etc.

### 2.1 Build

Para construir os artefatos é necessário executar o comando `dotnet build` ou executar a tarefa `Build` via [Cake](https://cakebuild.net/).

### 2.2 Testes

Para executar os testes é necessário executar o comando `dotnet test` ou executar a tarefa `Test` via [Cake](https://cakebuild.net/).

Para visualizar o relatório de cobertura de código basta executar a tarefa `Report` via [Cake](https://cakebuild.net/) e abrir o arquivo gerado na pasta `Coverage` chamado `index.html` ou `index.htm`.

### 2.3 Execução

Para executar a API é necessário executar o comando `dotnet run --project TOTVS.Fullstack.Challenge.AuctionHouse.RestApi\TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.csproj` ou executar a tarefa `Run` via [Cake](https://cakebuild.net/).

É necessário observar a URL informada pois ela é necessária para a configuração do _frontend_.

## 3. TODO

1. Criptografia de senhas
2. Parametrização do TTL do JWT via arquivo de configuração
3. Testes da camada de infraestrutura
4. Testes de filtro de exceções customizadas na camada de apresentação (API REST)
5. Ajuste nos scrips de relatório de cobertura e Sonar para ignorar certos arquivos-fonte
6. Ajuste no script de execução de testes para gerar o relatório de resultados de testes juntamente com a cobertura para que seja possível adicionar os resultados dos testes no Sonar
