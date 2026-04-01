FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["CartaoCreditoValido.WebAPI/CartaoCreditoValido.WebAPI.csproj", "CartaoCreditoValido.WebAPI/"]
COPY ["CartaoCreditoValido.Application/CartaoCreditoValido.Application.csproj", "CartaoCreditoValido.Application/"]
COPY ["CartaoCreditoValido.Domain/CartaoCreditoValido.Domain.csproj", "CartaoCreditoValido.Domain/"]
COPY ["CartaoCreditoValido.Infra/CartaoCreditoValido.Infra.csproj", "CartaoCreditoValido.Infra/"]

RUN dotnet restore "CartaoCreditoValido.WebAPI/CartaoCreditoValido.WebAPI.csproj"

COPY . .
WORKDIR "/src/CartaoCreditoValido.WebAPI"
RUN dotnet publish "CartaoCreditoValido.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CartaoCreditoValido.WebAPI.dll"]

