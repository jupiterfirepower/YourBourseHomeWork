#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/YB.Todo/YB.Todo.csproj", "src/YB.Todo/"]
RUN dotnet restore "src/YB.Todo/YB.Todo.csproj"
COPY . .
WORKDIR "/src/src/YB.Todo"
RUN dotnet build "YB.Todo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "YB.Todo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YB.Todo.dll"]