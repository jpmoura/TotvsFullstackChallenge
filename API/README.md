# TOTVS Fullstack Challenge - Backend

Como parte do desafio fullstack, para o backend foi criada uma API REST em C# usando .NET Core 3.1 e ASP.NET Core. Todos os comandos descritos a seguir considera-se que o usuário está já na pasta do projeto.

## Build

Para construir os artefatos é necessário executar o comando `dotnet build`

## Testes

Para executar os testes é necessário executar o comando `dotnet test`

## Execução

Para executar a API é necessário executar o comando `dotnet run --project TOTVS.Fullstack.Challenge.AuctionHouse.RestApi\TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.csproj`.

É necessário observar a URL informada pois ela é necessária para a configuração do frontend

## Configurações

É possível editar a string usada para criação e leitura dos JWT criados e utilizados na autenticação. Para isso basta editar o arquivo `appsettings.json`