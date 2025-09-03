FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["product-management.csproj", "./"]
RUN dotnet restore "product-management.csproj"

COPY . .
WORKDIR "/src/."
RUN dotnet build "product-management.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "product-management.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "product-management.dll"]