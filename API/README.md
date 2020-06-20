# TOTVS Fullstack Challenge - Backend

[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=jpmoura_TotvsFullstackChalleng&metric=bugs)](https://sonarcloud.io/dashboard?id=jpmoura_TotvsFullstackChalleng)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=jpmoura_TotvsFullstackChalleng&metric=code_smells)](https://sonarcloud.io/dashboard?id=jpmoura_TotvsFullstackChalleng)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=jpmoura_TotvsFullstackChalleng&metric=coverage)](https://sonarcloud.io/dashboard?id=jpmoura_TotvsFullstackChalleng)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=jpmoura_TotvsFullstackChalleng&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=jpmoura_TotvsFullstackChalleng)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=jpmoura_TotvsFullstackChalleng&metric=alert_status)](https://sonarcloud.io/dashboard?id=jpmoura_TotvsFullstackChalleng)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=jpmoura_TotvsFullstackChalleng&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=jpmoura_TotvsFullstackChalleng)
[![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=jpmoura_TotvsFullstackChalleng&metric=sqale_index)](https://sonarcloud.io/dashboard?id=jpmoura_TotvsFullstackChalleng)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=jpmoura_TotvsFullstackChalleng&metric=vulnerabilities)](https://sonarcloud.io/dashboard?id=jpmoura_TotvsFullstackChalleng)

Como parte do desafio _fullstack_, para o _backend_ foi criada uma API REST em C# usando .NET Core 3.1 e ASP.NET Core.

Essa API também conta com uma rota de documentação via [Swagger](https://swagger.io/) que pode ser acessada no endpoint `\swagger` no endereço que a API está hospedada.

## 1. Configurações

É possível editar a string usada para criação e leitura dos JWT criados e utilizados na autenticação. Para isso basta editar o arquivo [appsettings.json](TOTVS.Fullstack.Challenge.AuctionHouse.RestApi\appsettings.json). Além disso é necessário que o servidor tenha um certificado SSL válido visto que a aplicação conta com redireção para HTTPS. É possível editar esse comportamento comentando ou removendo a linha 74 do arquivo [Startup.cs](TOTVS.Fullstack.Challenge.AuctionHouse.RestApi\Startup.cs).

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
