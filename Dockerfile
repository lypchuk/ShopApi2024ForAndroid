# Learn about building .NET container images:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY ["ShopApi2024/ShopApi2024.csproj", "ShopApi2024/"]
RUN dotnet restore "ShopApi2024/ShopApi2024.csproj"

# copy everything else and build app
COPY . .
WORKDIR /source/ShopApi2024
RUN dotnet publish -o /app


# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ShopApi2024.dll"]
