FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY ["src/TheDevSpaceWebApp/TheDevSpaceWebApp.csproj", "src/TheDevSpaceWebApp/"]
COPY ["src/TheDevSpace.Repository/TheDevSpace.Repository.csproj", "src/TheDevSpace.Repository/"]
COPY ["src/TheDevSpace.Domain/TheDevSpace.Domain.csproj", "src/TheDevSpace.Domain/"]
COPY ["src/TheDevSpace.Application/TheDevSpace.Application.csproj", "src/TheDevSpace.Application/"]
RUN dotnet restore "src/TheDevSpaceWebApp/TheDevSpaceWebApp.csproj"
COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "TheDevSpaceWebApp.dll"]