# e-auditoria Processo Seletivo FullStack

O repositório é divido em 3 pastas, 1 que contem a solução e aplicação do backend feita em .Net 6.0, 1 pasta consta o frontend desenvolvido utilizando ReactJS e Typescript e por fim a ultima pasta contém scripts para criação e população do banco de dados que podem ser utilizados de forma opcional.

## Stack utilizada

**Front-end:** React, Typescript

**Back-end:** .Net 6.0, Entity Framework

**Base de dados:** MySQL

## Instalação Backend

Abra o projeto "SistemaLocacao" e baixe as dependencias do projeto.

```bash
    cd backend/SistemaLocacao
    dotnet restore
```

Por padrão a aplicação está configurada para acessar a base de dados mysql na porta `3306`, acesse o `appsettings.json` e/ou `appsettings.Development.json` e adicione o seu usuario no lugar do `root` e sua senha no lugar do `1234`.

Para execução dos recursos, será necessário a criação da base de dados, podendo ser via script mysql, ou rodando o update database.

CLI do .NET Core

```bash
    dotnet ef database update
```

Visual Studio

```bash
    Update-Database
```

Por fim, pode-se executar o seguinte comando para rodar a aplicação.

```bash
    dotnet run
```

## Instalação Frontend

Abra a pasta base do frontend e baixe as dependencias do projeto.

```bash
    cd frontend
    npm install
```

Para a execução da aplicação no modo de desenvolvimento é necessário rodar o seguinte comando:

```bash
    npm start
```

Acesse [http://localhost:3000](http://localhost:3000) para visualizar no seu navegador.

## Banco de dados

Na pasta `Database` contém 2 scripts, 1 contém a script para criação da base de dados, para caso não queira rodar o `update-database` e 1 script com um lote de inserts para popular a base de dados para melhor visualização dos componentes.
