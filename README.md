# Backend-Lab2-RESTAPI

**.NET SDK**: Якщо ви хочете запустити проект локально, вам знадобиться встановлений .NET SDK. Ви можете завантажити його з [офіційного сайту .NET](https://dotnet.microsoft.com/download).

## Клонуйте репозиторій

```sh
git clone https://github.com/D0wnfal1/Backend-Lab2-ProjectTemplate
cd Backend-Lab2-ProjectTemplate
```

## Відновіть залежності

```sh
dotnet restore
```

## Налаштуйте середовище

Перед запуском проекту ви можете встановити змінну середовища `ASPNETCORE_ENVIRONMENT` на `Development`, щоб активувати режим розробника:

### Windows

```sh
set ASPNETCORE_ENVIRONMENT=Development
```

### Linux / macOS

```sh
export ASPNETCORE_ENVIRONMENT=Development
```

## Запустіть проект

```sh
dotnet run
```

## Перевірте API

Ви можете перевірити API через Swagger, відкривши [http://localhost:5066/swagger](http://localhost:5066/swagger) у браузері. Swagger надає зручний інтерфейс для тестування ендпоінтів.
