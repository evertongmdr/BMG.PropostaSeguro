#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["./services/BMG.Propostas.API/BMG.Propostas.API.csproj", "services/BMG.Propostas.API/"]
COPY ["./building blocks/BMG.Core/BMG.Core.csproj", "building blocks/BMG.WebAPI.Core/"]
COPY ["./building blocks/BMG.WebAPI.Core/BMG.WebAPI.Core.csproj", "building blocks/BMG.WebAPI.Core/"]

RUN dotnet restore "services/BMG.Propostas.API/BMG.Propostas.API.csproj"
COPY . .
WORKDIR "services/BMG.Propostas.API"
RUN dotnet build "BMG.Propostas.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BMG.Propostas.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BMG.Propostas.API.dll"]