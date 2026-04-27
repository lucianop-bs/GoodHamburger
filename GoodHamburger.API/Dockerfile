FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["GoodHamburger.API/GoodHamburger.API.csproj", "GoodHamburger.API/"]
COPY ["GoodHamburger.Application/GoodHamburger.Application.csproj", "GoodHamburger.Application/"]
COPY ["GoodHamburger.Domain/GoodHamburger.Domain.csproj", "GoodHamburger.Domain/"]
COPY ["GoodHamburger.Infrastructure/GoodHamburger.Infrastructure.csproj", "GoodHamburger.Infrastructure/"]
RUN dotnet restore "GoodHamburger.API/GoodHamburger.API.csproj"

COPY . .
WORKDIR "/src/GoodHamburger.API"
RUN dotnet build "GoodHamburger.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoodHamburger.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoodHamburger.API.dll"]
